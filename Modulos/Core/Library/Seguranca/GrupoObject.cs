using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Core.Library.Seguranca
{
    public class Grupo : ModeloObjetoBase
    {
        public Grupo()
            : base("Grupo", "IdGrupo")
        {
        }

        public Grupo(long id)
            : this()
        {
            this.Materializar(id);
        }

        #region Propriedades

        public string Descricao { get; set; }

        #endregion

        #region Métodos

        protected override void Reset()
        {
            this.ID = Valor.Zero;
            this.Descricao = Valor.Vazio;
            base.Log.Reset();
            this.Materializado = Valor.Inativo;
        }

        protected override bool Validar()
        {
            if (Checar.IsCampoVazio(this.Descricao))
                throw new Exception(Erros.ValorInvalido("Grupo de Acesso", "Descrição do grupo"));

            base.Log.Validar();

            return true;
        }

        protected override void PreencherItensPersistencia()
        {
            this.ItensPersistencia.Add("Descricao", this.Descricao);
            base.Log.PreencherItensPersistencia(this.ItensPersistencia);
        }

        public override void Materializar(long id, bool materializarClasses)
        {
            this.ID = id;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT Descricao, {3}
            FROM {0}
            WHERE {1} = {2}
            ", this.Tabela, this.ChavePrimaria, this.ID, base.Log.GetColunasSQL());

            LeitorFacade leitor = new LeitorFacade(sql, this.TransacaoEnvolvida);
            if (leitor.LerLinha())
            {
                this.Descricao = leitor.GetValor("Descricao").ToString();
                base.Log.Materializar(leitor);

                this.Materializado = Valor.Ativo;
            }
            leitor.Fechar();
        }

        #endregion

        #region Métodos de ligação

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            LeitorFacade leitor = GrupoUsuarioController.GetAll(this.ID, Valor.Zero);
            while (leitor.LerLinha())
            {
                Usuario objUsuario = UsuarioController.Create();
                objUsuario.ID = Conversoes.ToInt64(leitor.GetValor("IdUsuario"));
                usuarios.Add(objUsuario);
            }
            leitor.Fechar();

            return usuarios;
        }

        #endregion
    }
}
