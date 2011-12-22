using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swarm.Utilitarios;
using Swarm.Persistencia;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Core.Library.Seguranca.Criptografia;

namespace Swarm.Core.Web
{
    public abstract class UsuarioCorrenteFacade
    {
        public static Usuario Instance
        {
            get { return UsuarioCorrenteSingleton.Instance.Usuario; }
            private set { UsuarioCorrenteSingleton.Instance.Usuario = value; }
        }

        /// <summary>
        /// Deve ser armazenado o GUID do Ambiente que o usuário estiver trabalhando.
        /// </summary>
        public static string Environment
        {
            get { return EnvironmentSingleton.Instance.Token; }
            set { EnvironmentSingleton.Instance.Token = value; }
        }

        #region  Métodos

        public static void Autenticar(string login, string senha)
        {
            try
            {
                Usuario objUsuario = UsuarioController.Get(login);
                objUsuario.Autenticar(senha);

                // Definindo usuário corrente (logado)
                UsuarioCorrenteFacade.Instance = objUsuario;
            }
            catch
            {
                UsuarioCorrenteFacade.Desconectar();
                throw new SenhaInvalidaException();
            }
        }

        public static void Desconectar()
        {
            UsuarioCorrenteSingleton.Destroy();
            EnvironmentSingleton.Destroy();
        }       

        public static void AtualizarSenha(string senhaAtual, string novaSenha, ColecaoPersistencia colecao)
        {
            ColecaoPersistencia colecaoPersistencia = Checar.IsNull(colecao) ? new ColecaoPersistencia() : colecao;

            if (Checar.IsCampoVazio(senhaAtual) || !Checar.IsCampoVazio(novaSenha))
                throw new AlterarSenhaDadosIncompletosException();

            Criptografia objCriptografia = UsuarioController.GetModelodeCriptografia();
            string senhaAtualCriptografada = objCriptografia.Criptografar(senhaAtual);

            if (senhaAtualCriptografada.Equals(UsuarioCorrenteFacade.Instance.Senha))
            {
                novaSenha = objCriptografia.Criptografar(novaSenha);
                UsuarioCorrenteFacade.Instance.Senha = novaSenha;
                colecao.AdicionarItem(UsuarioCorrenteFacade.Instance, EnumPersistencia.Operacao.Alterar);
            }
            else
                throw new SenhaInvalidaException();

            if (!Checar.IsNull(colecao)) return;
            colecaoPersistencia.Persistir();
        }

        public static void AtualizarAvatar(string avatar, ColecaoPersistencia colecao)
        {
            ColecaoPersistencia colecaoPersistencia = Checar.IsNull(colecao) ? new ColecaoPersistencia() : colecao;

            if (Checar.IsCampoVazio(avatar))
                throw new Exception(Erros.ValorInvalido("Usuário", "Avatar / Foto"));

            UsuarioCorrenteFacade.Instance.Avatar = avatar;
            colecao.AdicionarItem(UsuarioCorrenteFacade.Instance, EnumPersistencia.Operacao.Alterar);

            if (!Checar.IsNull(colecao)) return;
            colecaoPersistencia.Persistir();
        }

        #endregion
    }
}
