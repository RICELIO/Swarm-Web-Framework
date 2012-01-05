using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController.Common;
using Swarm.Utilitarios;

namespace Swarm.Web
{
    public partial class Expired : PageBase
    {
        #region Propriedades

        public int ProximaPagina_ID
        {
            get { return Conversoes.ToInt32(base.GetParametro(PageFacade.URI_NEXT_ID)); }
        }

        public string ProximaPagina_Parametros
        {
            get { return base.GetParametro(PageFacade.URI_NEXT_Parametros); }
        }

        public string AmbienteSelecionado
        {
            get { return base.GetParametro("AmbienteToken"); }
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
                this.ctAutenticador.Operacao_DefinirAmbiente(this.AmbienteSelecionado, this.ProximaPagina_ID, this.ProximaPagina_Parametros);
            else
                this.ctAutenticador.Operacao_PrepararItensControl(Valor.Ativo);
        }

        #endregion
    }
}