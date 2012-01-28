using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Utilitarios.Helpers.Web;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController;
using Swarm.Core.Web.FrontController.Common;
using Swarm.Web.Code.Portal.Controles;
using Swarm.Utilitarios;

namespace Swarm.Web.Controles
{
    public partial class Autenticador : ControlBase
    {
        #region Propriedades

        private bool IsSessaoExpirada
        {
            get { return Conversoes.ToBoolean(this.Page.Session[PageFacade.URI_SET_EXPIRED]); }
            set { this.Page.Session[PageFacade.URI_SET_EXPIRED] = value; }
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
        }

        protected void btnAutenticar_Click(object sender, EventArgs e)
        {
            this.Operacao_EfetuarLoginNoSistema();
        }

        #endregion

        #region Métodos Internos

        protected void ValidarItensControl()
        {
            if (Checar.IsCampoVazio(this.LogindoUsuario))
                throw new Exception(Erros.CampoVazio("Usuário"));

            if (Checar.IsCampoVazio(this.SenhadoUsuario))
                throw new Exception(Erros.CampoVazio("Senha"));
        }

        protected void Operacao_EfetuarLoginNoSistema()
        {
            try
            {
                this.ValidarItensControl();
                base.Autenticar(this.LogindoUsuario, this.SenhadoUsuario);

                if (this.IsSessaoExpirada)
                    Javascript.ReloadPage(this.Page, Janela.Sender.Self, Valor.Inativo);
                else
                    Navigation.GoToByKEY(Map.Seguranca.Login);
            }
            catch (Exception erro) { Javascript.Alert(this.Page, erro.Message); }
        }

        #endregion

        #region Métodos Externos

        public void Operacao_PrepararItensControl(bool isSessaoExpirada)
        {
            this.Operacao_PrepararItensControl(isSessaoExpirada, Valor.MenosUm, Valor.Vazio);
        }
        public void Operacao_PrepararItensControl(bool isSessaoExpirada, int urlID, string parametros)
        {
            Controle.SetVisible(!base.UsuarioLogado.Autenticado, this.blocoAutenticacao);
            Controle.SetVisible(base.UsuarioLogado.Autenticado, this.blocoSelecaoAmbiente);
            if (this.blocoSelecaoAmbiente.Visible) this.ltrSelecaodeAmbiente.Text = new SelecaodeAmbiente(urlID, parametros).Render();

            this.IsSessaoExpirada = isSessaoExpirada;
        }

        public void Operacao_DefinirAmbiente(string ambienteEnvolvido)
        {
            this.Operacao_DefinirAmbiente(ambienteEnvolvido, Valor.MenosUm, Valor.Vazio);
        }
        public void Operacao_DefinirAmbiente(string ambienteEnvolvido, int urlID, string parametros)
        {
            base.DefinirAmbiente(ambienteEnvolvido);
            if (Checar.MenorQue(urlID))
                Navigation.GoToByKEY(Map.Portal.Home);
            else
                Navigation.GoToByID(urlID, parametros);
        }

        #endregion
    }
}