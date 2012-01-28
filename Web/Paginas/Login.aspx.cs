using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core.Web;
using Swarm.Core.Web.Configuracao;
using Swarm.Core.Web.FrontController.Common;
using Swarm.Web.Code.Portal.Controles;
using Swarm.Utilitarios;

namespace Swarm.Web
{
    public partial class Login : PageBase
    {
        #region Propriedades

        public string AmbienteSelecionado
        {
            get { return base.GetParametro(PageFacade.URI_Ambiente_TOKEN); }
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
            if (!Checar.IsCampoVazio(this.AmbienteSelecionado))
            {
                this.ctAutenticador.Operacao_DefinirAmbiente(this.AmbienteSelecionado);
            }
            else
            {
                this.blocodeDemonstracao.Visible = ConfiguracoesGeraisController.Get().Demonstracao_Habilitar;
                this.ltrBlocodeDemonstracao.Text = this.blocodeDemonstracao.Visible ? new BlocodeDemonstracao().Render() : Valor.Vazio;
                this.ltrBlocodeApresentacao.Text = new BlocodeApresentacao().Render();
                this.ltrBlocoUltimasAtualizacoes.Text = new BlocoUltimasAtualizacoes(Valor.Cinco).Render();

                this.ctAutenticador.Operacao_PrepararItensControl(Valor.Inativo);
            }
         }

        #endregion
    }
}