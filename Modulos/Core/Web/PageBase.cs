using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Swarm.Utilitarios;
using Swarm.Core.Library.Seguranca.Autenticacao;
using Swarm.Core.Web.FrontController;

namespace Swarm.Core.Web
{
    public class PageBase : Page
    {
        #region Propriedades

        protected Usuario UsuarioLogado
        {
            get { return UsuarioCorrenteFacade.Instance; }
        }

        #endregion

        #region Métodos - Gerais

        /// <summary>
        /// Este método irá tratar os campos do tipo TEXT para evitar duplicação e ataques ao sistema. Deve ser utilizado em todos os parâmetros do tipo STRING que forem ser armanezados na base de dados.
        /// </summary>
        public string GetTEXT(ITextControl controle)
        {
            string valor = controle.Text.Trim();
            return Tratar.SqlInjection(valor, Valor.Ativo);
        }

        public string GetParametro(string parametro)
        {
            return Navigation.GetParameter(parametro);
        }

        #endregion

        #region Métodos - Usuário

        /// <summary>
        /// Este método irá autenticar o usuário no sistema.
        /// </summary>
        public void Autenticar(string login, string senha)
        {
            UsuarioCorrenteFacade.Autenticar(login, senha);
        }

        /// <summary>
        /// Este método irá definir o ambiente que o usuário estiver logado no sistema. Caso seja necessário trocar o ambiente sugerimos desconectar o usuário envolvido.
        /// </summary>
        /// <param name="token">GUID do Ambiente (Habilitado, Restrito e com o mapeamento definido).</param>
        public void DefinirAmbiente(string token)
        {
            UsuarioCorrenteFacade.Environment = token;
        }

        /// <summary>
        /// Este método irá desconectar o usuário do sistema.
        /// </summary>
        public void Desconectar()
        {
            UsuarioCorrenteFacade.Desconectar();
        }        

        #endregion
    }
}
