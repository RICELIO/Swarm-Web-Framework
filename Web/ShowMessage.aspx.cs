using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core.Web.FrontController;

namespace Swarm.Web
{
    public partial class ShowMessage : Page
    {
        #region Propriedades

        protected string Titulo
        {
            get { return Navigation.GetParameter("t"); }
        }

        protected string Mensagem
        {
            get { return Navigation.GetParameter("m"); }
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
            this.Title = this.Titulo;
            this.lblMsg.Text = this.Mensagem;
        }

        #endregion
    }
}