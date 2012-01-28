using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core.Web;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Core.Web.FrontController;
using Swarm.Utilitarios;
using Swarm.Web.Code.Portal.Views;

namespace Swarm.Web.Portal
{
    public partial class VisualizarControlesdeAcesso : PageBase
    {
        #region Propriedades

        protected long UrlID
        {
            get { return Conversoes.ToInt64(base.GetParametro("UrlID")); }
        }

        protected EnumAcesso.TipodeAcesso TipodeAcesso
        {
            get { return (EnumAcesso.TipodeAcesso)Conversoes.ToInt32(base.GetParametro("TipoAcesso")); }
        }

        #endregion

        #region Eventos disparados pelo usuário

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
            this.DefinirItensView();
        }

        #endregion

        #region Métodos

        protected void DefinirItensView()
        {
            switch (this.TipodeAcesso)
            {
                default:
                case EnumAcesso.TipodeAcesso.Indefinido:
                case EnumAcesso.TipodeAcesso.Ambiente:
                case EnumAcesso.TipodeAcesso.Funcionalidade:
                    Navigation.GoToByKEY(Map.Portal.Home);
                    break;
                case EnumAcesso.TipodeAcesso.SuperGrupo:
                    {
                        VisualizarDetalhesItensSuperGrupo view = new VisualizarDetalhesItensSuperGrupo(this.UrlID);
                        this.ltrView.Text = view.Render();
                        break;
                    }
                case EnumAcesso.TipodeAcesso.Grupo:
                    {
                        VisualizarDetalhesItensGrupo view = new VisualizarDetalhesItensGrupo(this.UrlID);
                        this.ltrView.Text = view.Render();
                        break;
                    }
            }
        }

        #endregion
    }

}