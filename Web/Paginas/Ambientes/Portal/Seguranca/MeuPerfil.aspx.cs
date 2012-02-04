using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Swarm.Utilitarios;
using Swarm.Utilitarios.Helpers.Web;
using Swarm.Core;
using Swarm.Core.Web;
using Swarm.Core.Web.FrontController;
using Swarm.Core.Library.Seguranca.Autenticacao;

namespace Swarm.Web.Portal.Seguranca
{
    public partial class MeuPerfil : PageBase
    {
        #region Propriedades

        protected string SenhaAtual
        {
            get { return this.txtSenhaAtual.Text.Trim(); }
        }

        protected string NovaSenha
        {
            get { return this.txtNovaSenha.Text.Trim(); }
        }

        protected string NovaSenha_Confirmacao
        {
            get { return this.txtConfirmarNovaSenha.Text.Trim(); }
        }

        #endregion

        #region Eventos disparados pelo usuário

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
            this.DefinirItensView();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            this.Operacao_AlterarSenhadoUsuarioEnvolvido();
        }

        protected void btnEnviarAvatar_Click(object sender, EventArgs e)
        {
            this.Operacao_AssociarAvatarAoUsuarioEnvolvido();
        }

        #endregion

        #region Métodos

        protected void ValidarItensView_Autenticacao()
        {
            if (Checar.IsCampoVazio(this.SenhaAtual))
                throw new Exception(Erros.CampoVazio("Senha atual"));

            if (Checar.IsCampoVazio(this.NovaSenha))
                throw new Exception(Erros.CampoVazio("Nova senha"));

            if (Checar.IsCampoVazio(this.NovaSenha_Confirmacao))
                throw new Exception(Erros.CampoVazio("Confirmar nova senha"));

            if (this.NovaSenha != this.NovaSenha_Confirmacao)
                throw new Exception("A nova senha não confere com a confirmação informada.");
        }

        protected void DefinirItensView()
        {
            this.DefinirItensView_Autenticacao();
            this.DefinirItensView_ImagemdeExibicao();
        }
        private void DefinirItensView_Autenticacao()
        {
            this.lblLogin.Text = this.UsuarioLogado.Login;
            this.lblNivelAcesso.Text = this.UsuarioLogado.GetDescricaodoNiveldeAcesso();
            Controle.ReiniciarControles(this.txtSenhaAtual, this.txtNovaSenha, this.txtConfirmarNovaSenha);
        }
        private void DefinirItensView_ImagemdeExibicao()
        {
            string parametrosAvatar = string.Format("Imagem={0}&Largura=100&Altura=112", base.UsuarioLogado.GetAvatar(Diretorio.Tipo.Web));
            this.imgAvatar.ImageUrl = string.Format("~/{0}", Navigation.GetURI(Map.Callback.ObterImagemThumbnail, parametrosAvatar));
        }

        private void Operacao_AlterarSenhadoUsuarioEnvolvido()
        {
            try
            {
                this.ValidarItensView_Autenticacao();
                UsuarioController.AtualizarSenha(base.UsuarioLogado, this.SenhaAtual, this.NovaSenha, null);
                
                Javascript.Alert(this, "Sua senha de acesso foi alterada com sucesso!");
                this.DefinirItensView_Autenticacao();
            }
            catch (Exception erro) { Javascript.Alert(this, erro.Message); }
        }

        private void Operacao_AssociarAvatarAoUsuarioEnvolvido()
        {
            try
            {
                UploadImagem obj = new UploadImagem(this.upAvatar, Configuracoes.Avatar_FilePath, base.UsuarioLogado.Login, 300, Valor.Zero, Valor.Zero, null);
                
                bool arquivoEnviado = obj.Enviar();
                if (!arquivoEnviado) throw new Exception("Não foi possível concluir o envio da imagem para o servidor. Tente novamente!");
                
                bool avatarAtualizado = UsuarioController.AtualizarAvatar(base.UsuarioLogado, obj.GetNomedoArquivo(), null);
                if (!avatarAtualizado) throw new Exception("Não foi possível atualizar a sua imagem de exibição. Tente novamente!");

                Javascript.Alert(this, "Imagem de exibição enviada com sucesso!");
                this.DefinirItensView_ImagemdeExibicao();
            }
            catch (Exception erro) { Javascript.Alert(this, erro.Message); }
        }

        #endregion
    }
}