using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Library.Seguranca.Criptografia;
using Swarm.Core.Web.FrontController.Common;

namespace Swarm.Core.Web.FrontController
{
    public abstract class Navigation
    {
        #region Métodos Externos

        public static string GetTitle(string key)
        {
            return UrlMap.Find(key).Titulo;
        }
        public static string GetTitle(int id)
        {
            return UrlMap.Find(id).Titulo;
        }

        public static void GoToHandler()
        {
            Navigation.GoToByKEY(Map.FrontController.Default);
        }

        public static void ShowMessage(string mensagem)
        {
            string titulo = Navigation.GetTitle(Map.Seguranca.Login);
            Navigation.ShowMessage(titulo, mensagem);
        }
        public static void ShowMessage(string titulo, string mensagem)
        {
            string parametros = string.Format("t={0}&m={1}", titulo, mensagem);
            Navigation.GoToByKEY(Map.FrontController.ShowMessage, parametros);
        }

        public static string GetParameter(string parametro)
        {
            string parametrosRequested = HttpContext.Current.Request[PageFacade.URI_Parametros];
            if (Checar.IsCampoVazio(parametrosRequested)) return Valor.Vazio;

            parametrosRequested = new CriptografiaWEB().Descriptografar(parametrosRequested);
            List<string> listaParametros = Conversoes.ToList<string>(parametrosRequested.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            string parametroEnvolvido = listaParametros.Find(item => parametro == item.Split('=')[Valor.Zero]);
            
            return Checar.IsCampoVazio(parametroEnvolvido) ? Valor.Vazio : parametroEnvolvido.Split('=')[Valor.Um];
        }

        public static void GoToByKEY(string key)
        {
            Navigation.GoToByKEY(key, Valor.Vazio);
        }
        public static void GoToByKEY(string key, string parametros)
        {
            Navigation.GoToByKEY(key, parametros, Valor.Inativo);
        }
        public static void GoToByKEY(string key, string parametros, bool isCriptografado)
        {
            int id = UrlMap.Find(key).ID;
            Navigation.GoToByID(id, parametros, isCriptografado);
        }

        public static void GoToByID(int id)
        {
            Navigation.GoToByID(id, Valor.Vazio, Valor.Inativo);
        }
        public static void GoToByID(int id, string parametros)
        {
            Navigation.GoToByID(id, parametros, Valor.Inativo);
        }
        public static void GoToByID(int id, string parametros, bool isCriptografado)
        {
            string uri = string.Format("~/{0}.aspx?{1}={2}", Map.FrontController.Controller, PageFacade.URI_ID, id);
            if (!Checar.IsCampoVazio(parametros))
            {
                if (!isCriptografado) parametros = new CriptografiaWEB().Criptografar(parametros);
                uri += string.Format("&{0}={1}", PageFacade.URI_Parametros, parametros);
            }

            HttpContext.Current.Response.Redirect(uri);
        }

        public static string GetURI(string key)
        {
            return Navigation.GetURI(key, Valor.Vazio);
        }
        public static string GetURI(string key, string parametros)
        {
            return Navigation.GetURI(key, parametros, Valor.Inativo);
        }
        public static string GetURI(string key, string parametros, bool isCriptografado)
        {
            int id = UrlMap.Find(key).ID;
            return Navigation.GetURI(id, parametros, isCriptografado);
        }

        public static string GetURI(int id)
        {
            return Navigation.GetURI(id, Valor.Vazio);
        }
        public static string GetURI(int id, string parametros)
        {
            return Navigation.GetURI(id, parametros, Valor.Inativo);
        }
        public static string GetURI(int id, string parametros, bool isCriptografado)
        {
            string uri = string.Format("{0}.aspx?{1}={2}", Map.FrontController.Controller, PageFacade.URI_ID, id);
            if (!Checar.IsCampoVazio(parametros))
            {
                if (!isCriptografado) parametros = new CriptografiaWEB().Criptografar(parametros);
                uri += string.Format("&{0}={1}", PageFacade.URI_Parametros, parametros);
            }

            return uri;
        }

        internal static void Redirect(int id, string parametros)
        {
            string uri = string.Format("~/{0}.aspx?{1}={2}", Map.FrontController.Controller, PageFacade.URI_ID, id);
            if (!Checar.IsCampoVazio(parametros)) uri += string.Format("&{0}", parametros);

            HttpContext.Current.Response.Redirect(uri);
        }

        #endregion
    }
}
