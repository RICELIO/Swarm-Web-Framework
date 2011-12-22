using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Helpers.Web;
using Swarm.Core.Web;
using Swarm.Core.Web.Configuracao;
using Swarm.Core.Web.FrontController;
using Swarm.Web.Code.Portal.Controles;

namespace Swarm.Web
{
    public partial class Login : PageBase
    {
        #region Propriedades

        public string AmbienteSelecionado
        {
            get { return base.GetParametro("AmbienteToken"); }
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
            if (!Checar.IsCampoVazio(this.AmbienteSelecionado))
            {
                base.DefinirAmbiente(this.AmbienteSelecionado);
                Navigation.GoToByKEY(Map.Portal.Home);
            }
            else
            {
                this.blocodeDemonstracao.Visible = ConfiguracoesGeraisController.Get().Demonstracao_Habilitar;
                this.ltrBlocodeDemonstracao.Text = this.blocodeDemonstracao.Visible ? new BlocodeDemonstracao().Render() : Valor.Vazio;
                this.ltrBlocodeApresentacao.Text = new BlocodeApresentacao().Render();
                this.ltrBlocoUltimasAtualizacoes.Text = new BlocoUltimasAtualizacoes(Valor.Cinco).Render();

                Controle.SetVisible(!base.UsuarioLogado.Autenticado, this.blocoAutenticacao);
                Controle.SetVisible(base.UsuarioLogado.Autenticado, this.blocoSelecaoAmbiente);
                if (this.blocoSelecaoAmbiente.Visible) this.ltrSelecaodeAmbiente.Text = new SelecaodeAmbiente().Render();
            }
         }
        
        protected void Operacao_EfetuarLoginNoSistema()
        {
            try
            {
                this.ValidarItensView();
                base.Autenticar(this.LogindoUsuario, this.SenhadoUsuario);
                Navigation.GoToByKEY(Map.Seguranca.Login);
            }
            catch (Exception erro) { Javascript.Alert(this, erro.Message); }
        }
 
        #endregion
        
    }
}