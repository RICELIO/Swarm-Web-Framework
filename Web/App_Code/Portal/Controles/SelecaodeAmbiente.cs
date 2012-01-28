using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Utilitarios;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController;
using Swarm.Core.Web.FrontController.Common;

namespace Swarm.Web.Code.Portal.Controles
{
    public class SelecaodeAmbiente : ViewBase
    {
        private int Url_ID { get; set; }
        private string Url_Parametros { get; set; }

        public SelecaodeAmbiente(int urlID, string parametros)
            : this()
        {
            this.Url_ID = urlID;
            this.Url_Parametros = parametros;
        }

        public SelecaodeAmbiente()
            : base("Portal/Controles/SelecaodeAmbiente.vm")
        {
        }

        protected override void SetDataContext()
        {
            base.Add("this", this);
        }

        #region Métodos

        public List<Ambiente> GetAmbientes()
        {
            return SecuritySettings.Ambientes.FindAll(obj => obj.Habilitado && obj.Restrito && Checar.MaiorQue(obj.GetItemBase().ID));
        }

        public string GetDefinirAmbienteURI(string guid)
        {
            if (Checar.MenorQue(this.Url_ID))
            {
                string parametros = string.Format("{0}={1}", PageFacade.URI_Ambiente_TOKEN, guid);
                return Navigation.GetURI(Map.Seguranca.Login, parametros);
            }
            else
            {
                string parametros = string.Format("{0}={1}&{2}={3}&{4}={5}", PageFacade.URI_NEXT_ID, this.Url_ID, PageFacade.URI_NEXT_Parametros, this.Url_Parametros, PageFacade.URI_Ambiente_TOKEN, guid);
                return Navigation.GetURI(Map.Seguranca.Expired, parametros);
            }
        }

        #endregion
    }
}