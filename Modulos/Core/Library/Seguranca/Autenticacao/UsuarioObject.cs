using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Library.Seguranca.Criptografia;
using Swarm.Persistencia;
using Swarm.Core.Web;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    public class Usuario : ModeloObjetoBase
    {
        public Usuario()
            : base("Usuario", "IdUsuario")
        {
            this.Reset();
        }

        public Usuario(long id)
            : this()
        {
            this.Materializar(id);
        }

        #region Propriedades

        public EnumAutenticacao.TipodeUsuario Tipo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Avatar { get; set; }
        public bool Habilitado { get; set; }
        public bool Bloqueado { get; set; }
        public bool TrocarSenha { get; set; }

        public bool Autenticado { get; private set; }

        private List<Permissao> Permissoes { get; set; }

        public bool IsAdministrador
        {
            get { return this.Tipo == EnumAutenticacao.TipodeUsuario.Administrador; }
        }

        #endregion

        #region Métodos - Específicos

        internal void Autenticar(string senha)
        {
            try
            {
                Criptografia criptografia = UsuarioController.GetModelodeCriptografia();

                if (this.Bloqueado)
                    throw new UsuarioBloqueadoException();
                else if (!this.Habilitado)
                    throw new UsuarioDesabilitadoException();
                else if (!criptografia.Comparar(senha, this.Senha))
                    throw new SenhaInvalidaException();
                else
                    this.Autenticado = true;
            }
            catch(Exception erro)
            {
                this.Autenticado = false;
                throw erro;
            }
        }

        public string GetFullPathAvatar()
        {
            if (String.IsNullOrEmpty(this.Avatar))
                return Configuracoes.Avatar_Padrao;

            // Verificando se o arquivo existe fisicamente
            string fullPathAvatar = String.Concat(Configuracoes.Avatar_Path, this.Avatar);
            if (File.Exists(fullPathAvatar))
            {
                string webPathAvatar = String.Concat(Configuracoes.Avatar_WebPath, this.Avatar);
                return webPathAvatar;
            }
            else
                return Configuracoes.Avatar_Padrao;
        }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.Tipo = EnumAutenticacao.TipodeUsuario.Usuario;
            this.Login = Valor.Vazio;
            this.Senha = Valor.Vazio;
            this.Avatar = Valor.Vazio;
            this.Habilitado = Valor.Ativo;
            this.Bloqueado = Valor.Inativo;
            this.TrocarSenha = Valor.Inativo;

            this.Permissoes = new List<Permissao>();

            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (this.Tipo == EnumAutenticacao.TipodeUsuario.Indefinido)
                throw new Exception(Erros.ValorInvalido("Usuário", "Tipo de Usuário"));
            
            if (Checar.IsCampoVazio(this.Login))
                throw new Exception(Erros.ValorInvalido("Usuário", "Login"));

            if (Checar.IsCampoVazio(this.Senha))
                throw new Exception(Erros.ValorInvalido("Usuário", "Senha"));

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("Tipo", (int)this.Tipo);
            this.ItensPersistencia.Add("Login", this.Login);
            this.ItensPersistencia.Add("Senha", this.Senha);
            this.ItensPersistencia.Add("Avatar", this.Avatar, Checar.IsCampoVazio(this.Avatar));
            this.ItensPersistencia.Add("Habilitado", this.Habilitado);
            this.ItensPersistencia.Add("Bloqueado", this.Bloqueado);
            this.ItensPersistencia.Add("TrocarSenha", this.TrocarSenha);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT Tipo, Login, Senha, Avatar, Habilitado, Bloqueado, TrocarSenha
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID);

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.Tipo = (EnumAutenticacao.TipodeUsuario)Conversoes.ToInt32(leitor.GetValor("Tipo"));
                this.Login = leitor.GetValor("Login").ToString();
                this.Senha = leitor.GetValor("Senha").ToString();
                this.Avatar = leitor.GetValor("Avatar").ToString();
                this.Habilitado = Conversoes.ToBoolean(leitor.GetValor("Habilitado"));
                this.Bloqueado = Conversoes.ToBoolean(leitor.GetValor("Bloqueado"));
                this.TrocarSenha = Conversoes.ToBoolean(leitor.GetValor("TrocarSenha"));
                
                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de Ligação

        public List<Grupo> GetGrupos()
        {
            List<Grupo> grupos = new List<Grupo>();
            LeitorFacade leitor = GrupoUsuarioController.GetAll(Valor.Zero, this.ID);
            while (leitor.LerLinha())
            {
                Grupo objGrupo = GrupoController.Create();
                objGrupo.ID = Conversoes.ToInt64(leitor.GetValor("IdGrupo"));
                grupos.Add(objGrupo);
            }
            leitor.Fechar();

            return grupos;
        }

        public List<Permissao> GetPermissoes()
        {
            if (Checar.MaiorQue(this.Permissoes.Count))
                return this.Permissoes;

            LeitorFacade leitor = PermissaoController.GetAllForUser(this.ID);
            while (leitor.LerLinha())
            {
                Permissao obj = PermissaoController.Create();
                obj.ID = Conversoes.ToInt64(leitor.GetValor("IdPermissao"));
                obj.GUID = leitor.GetValor("GUID").ToString();
                this.Permissoes.Add(obj);
            }
            leitor.Fechar();

            return this.Permissoes;
        }

        #endregion
    }
}
