using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Swarm.Core.Web.FrontController;
using Swarm.Utilitarios;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Core.Web
{
    /// <summary>
    /// Classe de Apoio as estruturas básicas da arquitetura Web.
    /// </summary>
    internal abstract class WebBase
    {
        #region Propriedades

        public static Usuario UsuarioLogado
        {
            get { return UsuarioCorrenteFacade.Instance; }
        }

        #endregion

        #region Métodos - Gerais

        /// <summary>
        /// Este método irá tratar os campos do tipo TEXT para evitar duplicação e ataques ao sistema. Deve ser utilizado em todos os parâmetros do tipo STRING que forem ser armanezados na base de dados.
        /// </summary>
        public static string GetTEXT(ITextControl controle)
        {
            string valor = controle.Text.Trim();
            return Tratar.SqlInjection(valor, Valor.Ativo);
        }

        public static string GetParametro(string parametro)
        {
            return Navigation.GetParameter(parametro);
        }

        #endregion

        #region Métodos - Usuário

        /// <summary>
        /// Este método irá autenticar o usuário no sistema.
        /// </summary>
        public static void Autenticar(string login, string senha)
        {
            UsuarioCorrenteFacade.Autenticar(login, senha);
        }

        /// <summary>
        /// Este método irá definir o ambiente que o usuário estiver logado no sistema. Caso seja necessário trocar o ambiente sugerimos desconectar o usuário envolvido.
        /// </summary>
        /// <param name="token">GUID do Ambiente (Habilitado, Restrito e com o mapeamento definido).</param>
        public static void DefinirAmbiente(string token)
        {
            UsuarioCorrenteFacade.Environment = token;
        }

        /// <summary>
        /// Este método irá desconectar o usuário do sistema.
        /// </summary>
        public static void Desconectar()
        {
            UsuarioCorrenteFacade.Desconectar();
        }

        #endregion
    }
}
