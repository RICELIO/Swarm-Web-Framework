using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Helpers.Web;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController;
using Swarm.Core.Web.FrontController.Common;

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

        public string LogindoUsuario
        {
            get { return base.GetTEXT(this.txtLogin); }
        }

        public string SenhadoUsuario
        {
            get { return base.GetTEXT(this.txtSenha); }
        }

        #endregion

        #region Eventos disparados pelo usuário

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
            this.DefinirItensView();
        }

        protected void btnAutenticar_Click(object sender, EventArgs e)
        {
            this.Operacao_EfetuarLoginNoSistema();
        }

        #endregion

        #region Métodos

        protected void ValidarItensView()
        {
            if (Checar.IsCampoVazio(this.LogindoUsuario))
                throw new Exception(Erros.CampoVazio("Usuário"));

            if (Checar.IsCampoVazio(this.SenhadoUsuario))
                throw new Exception(Erros.CampoVazio("Senha"));
        }

        protected void DefinirItensView()
        {
            Controle.SetBlank(this.txtLogin, this.txtSenha);
        }

        protected void Operacao_EfetuarLoginNoSistema()
        {
            try
            {
                this.ValidarItensView();
                base.Autenticar(this.LogindoUsuario, this.SenhadoUsuario);
                Navigation.GoToByID(this.ProximaPagina_ID, this.ProximaPagina_Parametros);
            }
            catch (Exception erro) { Javascript.Alert(this, erro.Message); }
        }

        #endregion
    }
}