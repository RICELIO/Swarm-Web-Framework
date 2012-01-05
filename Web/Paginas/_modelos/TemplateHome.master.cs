using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core;
using Swarm.Core.Web.Configuracao;
using Swarm.Web.Code.Portal.Controles;

namespace Swarm.Web.Templates
{
    public partial class TemplateHome : MasterPage
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
            this.Page.Title = ConfiguracoesGeraisController.Get().Produto_Titulo;

            this.imgLogoMarca.ImageUrl = Configuracoes.Logomarca_WebPath;
            this.ltrMicroMenudoUsuario.Text = new MicroMenudoUsuario().Render();

            this.ltrMenudoUsuarioTopo.Text = new MenudoUsuarioTopo().Render();

            this.lblTituloProduto.Text = ConfiguracoesGeraisController.Get().Produto_Titulo;
            this.lblNumeroVersao.Text = ConfiguracoesGeraisController.Get().GetVersaoAtual();
        }

        #endregion
    }
}