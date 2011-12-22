using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Swarm.Utilitarios;
using System.Web.UI;

namespace Swarm.Core.Web.FrontController.Common
{
    public abstract class PageFacade
    {
        public PageFacade(HttpContext conteudo)
        {
            this.Conteudo = conteudo;
            this.ID = PageFacade.GetID(conteudo);
        }

        #region Constantes

        public const string URI_ID = "id"; // fluxo normal
        public const string URI_Parametros = "pm"; // fluxo normal
        public const string URI_NEXT_ID = "nid"; // fluxo dependente
        public const string URI_NEXT_Parametros = "npm"; // fluxo dependente

        #endregion

        #region Propriedades

        protected int ID { get; set; }
        protected HttpContext Conteudo { get; set; }

        #endregion

        public Page GetPageRequested()
        {
            if (!this.ValidarControledeAcesso())
                Navigation.ShowMessage("A funcionalidade solicitada não está disponível no momento.");

            if (!this.ValidarAutenticacao())
            {
                string parametros = Valor.Vazio;
                string queryPARAMETROS = this.GetParams().Replace(string.Format("{0}=", URI_Parametros), Valor.Vazio);

                if (Checar.MaiorQue(queryPARAMETROS.Length))
                    parametros = string.Format("{0}={1}&{2}={3}", URI_NEXT_ID, this.ID, URI_NEXT_Parametros, queryPARAMETROS);
                else
                    parametros = string.Format("{0}={1}", URI_NEXT_ID, this.ID);

                Navigation.GoToByKEY(Map.Seguranca.Expired, parametros);
            }

            if (!this.ValidarPermissoes())
                Navigation.ShowMessage("Você não possui permissão de acesso a esta funcionalidade.");

            try
            {
                string uri = this.GetURI();
                return this.OverwritePage(uri);
            }
            catch (Exception erro)
            { 
                Navigation.ShowMessage(erro.Message);
                return null;
            }
        }

        public abstract bool ValidarAutenticacao();
        public abstract bool ValidarPermissoes();

        public virtual bool ValidarControledeAcesso()
        {
            return Valor.Ativo;
        }

        public virtual string GetURI()
        {
            return UrlMap.Find(this.ID).Url;
        }

        #region Métodos Externos

        public static int GetID(HttpContext conteudo)
        {
            return Checar.IsCampoVazio(conteudo.Request.QueryString[URI_ID]) ?
                   UrlMap.Find(Map.FrontController.Default).ID :
                   Conversoes.ToInt32(conteudo.Request.QueryString[URI_ID]);
        }

        #endregion

        #region Métodos Internos

        private string GetParams()
        {
            int qtdParametros = Valor.Zero;
            string parametros = Valor.Vazio;

            foreach (string chave in this.Conteudo.Request.QueryString.Keys)
            {
                if (chave == URI_ID) continue;

                if (Checar.MaiorQue(++qtdParametros, Valor.Um))
                    parametros += "&";

                string separador = Checar.MaiorQue(++qtdParametros, Valor.Um) ? "&" : Valor.Vazio;
                parametros += string.Format("{0}{1}={2}", separador, chave, this.Conteudo.Request.QueryString[chave]);
            }

            return parametros;
        }

        private Page OverwritePage(string uriFULL)
        {
            int indexURI_MAX = uriFULL.IndexOf("?");
            string uri = Checar.MaiorQue(indexURI_MAX) ? uriFULL.Substring(Valor.Zero, indexURI_MAX) : uriFULL;

            this.Conteudo.RewritePath(this.Conteudo.Request.FilePath, Valor.Vazio, this.GetParams());
            return (Page)PageParser.GetCompiledPageInstance(uri, this.Conteudo.Server.MapPath(uri), this.Conteudo);
        }

        #endregion
    }
}
