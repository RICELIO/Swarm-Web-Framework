using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core.Web.FrontController;

namespace Swarm.Web.Templates
{
    public partial class TemplateJanela : MasterPage
    {
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
            this.Page.Title = Navigation.GetTitle();
            this.lblTituloPagina.Text = this.Page.Title;
        }

        #endregion
    }
}