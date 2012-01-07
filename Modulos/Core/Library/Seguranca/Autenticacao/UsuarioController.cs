using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Library.Seguranca.Criptografia;
using Swarm.Persistencia;

namespace Swarm.Core.Library.Seguranca.Autenticacao
{
    public abstract class UsuarioController
    {
        public static Usuario Manter(Usuario obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Manter(obj, colecao) as Usuario;
        }

        public static bool Excluir(Usuario obj, ColecaoPersistencia colecao)
        {
            return ModeloControladorBase.Excluir(obj, colecao);
        }

        public static Usuario Create()
        {
            return new Usuario();
        }

        public static Usuario Get(string login)
        {
            Usuario obj = UsuarioController.Create();

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT {0}
            FROM {1}
            WHERE Login = '{2}'
            ", obj.ChavePrimaria, obj.Tabela, login);

            LeitorFacade leitor = new LeitorFacade(sql);
            if (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                if (id > Valor.Zero) return new Usuario(id);
            }

            throw new Exception(Erros.NaoEncontrado("Usuário"));
        }

        public static LeitorFacade GetAll()
        {
            Usuario obj = UsuarioController.Create();
            
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
            SELECT DISTINCT *
            FROM {0}
            ", obj.Tabela);

            return new LeitorFacade(sql);
        }

        public static List<Usuario> GetList()
        {
            Usuario obj = UsuarioController.Create();
            List<Usuario> usuarios = new List<Usuario>();

            LeitorFacade leitor = UsuarioController.GetAll();
            while (leitor.LerLinha())
            {
                long id = Conversoes.ToInt64(leitor.GetValor(obj.ChavePrimaria));
                usuarios.Add(new Usuario(id));
            }
            leitor.Fechar();

            return usuarios;
        }

        #region Métodos - Úteis

        /// <summary>
        /// Método responsável por recuperar o modelo de criptografia utilizado.
        /// </summary>
        public static Criptografia GetModelodeCriptografia()
        {
            switch (Configuracoes.Usuario_TipoCriptografia)
            {
                default:
                case EnumCriptografia.Tipo.Indefinido:
                case EnumCriptografia.Tipo.MD5:
                    return new CriptografiaMD5();
                case EnumCriptografia.Tipo.DES3:
                    return new CriptografiaDES3();
            }
        }

        #endregion

        #region Métodos

        public static void AtualizarSenha(long id, string senhaAtual, string novaSenha, ColecaoPersistencia colecao)
        {
            ColecaoPersistencia colecaoPersistencia = Checar.IsNull(colecao) ? new ColecaoPersistencia() : colecao;

            if (Checar.IsCampoVazio(senhaAtual) || !Checar.IsCampoVazio(novaSenha))
                throw new AlterarSenhaDadosIncompletosException();

            Criptografia objCriptografia = UsuarioController.GetModelodeCriptografia();
            string senhaAtualCriptografada = objCriptografia.Criptografar(senhaAtual);

            Usuario objUsuario = new Usuario(id);
            if (senhaAtualCriptografada.Equals(objUsuario.Senha))
            {
                novaSenha = objCriptografia.Criptografar(novaSenha);
                objUsuario.Senha = novaSenha;
                colecao.AdicionarItem(objUsuario, EnumPersistencia.Operacao.Alterar);
            }
            else
                throw new SenhaInvalidaException();

            if (!Checar.IsNull(colecao)) return;
            colecaoPersistencia.Persistir();
        }

        public static void AtualizarAvatar(long id, string avatar, ColecaoPersistencia colecao)
        {
            ColecaoPersistencia colecaoPersistencia = Checar.IsNull(colecao) ? new ColecaoPersistencia() : colecao;

            if (Checar.IsCampoVazio(avatar))
                throw new Exception(Erros.ValorInvalido("Usuário", "Avatar / Foto"));

            Usuario objUsuario = new Usuario(id);
            objUsuario.Avatar = avatar;
            colecao.AdicionarItem(objUsuario, EnumPersistencia.Operacao.Alterar);

            if (!Checar.IsNull(colecao)) return;
            colecaoPersistencia.Persistir();
        }

        #endregion
    }
}
