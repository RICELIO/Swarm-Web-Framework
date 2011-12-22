using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleVelocity;
using Swarm.Utilitarios;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController;

namespace Swarm.Web.Code.Portal.Controles
{
    public class SelecaodeAmbiente : ViewBase
    {
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
            string parametros = string.Format("AmbienteToken={0}", guid);
            return Navigation.GetURI(Map.Seguranca.Login, parametros);
        }

        #endregion
    }
}