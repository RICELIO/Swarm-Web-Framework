using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Swarm.Core.Web.ControledeAcesso;
using Swarm.Utilitarios.Library.Seguranca.Criptografia;

namespace Swarm.Ferramentas
{
    public partial class wPrincipal : Form
    {
        public wPrincipal()
        {
            InitializeComponent();
        }

        #region Eventos disparados pelo usuário

        private void wPrincipal_Shown(object sender, EventArgs e)
        {
            this.DefinirItensWindow();
        }

        #endregion

        #region Métodos

        protected void DefinirItensWindow()
        {
            this.OperacaoLimparGUIDGerado();
            this.OperacaoLimparCenarioCriptografia(true);
        }

        #endregion

        #region Aba - Controle de Acesso

        #region Eventos disparados pelo usuário
        private void btnCopiarGUID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OperacaoCopiarGUIDParaAreadeTransferencia();
        }

        private void btnLimparGUID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OperacaoLimparGUIDGerado();
        }

        private void btnGerarGUID_Click(object sender, EventArgs e)
        {
            this.OperacaoGerarGUIDSolicitado();
        }
        #endregion

        #region Métodos
        protected void OperacaoCopiarGUIDParaAreadeTransferencia()
        {
            Clipboard.SetDataObject(this.txtGUID.Text, true);
        }

        protected void OperacaoLimparGUIDGerado()
        {
            this.txtGUID.Text = string.Empty;
        }

        protected void OperacaoGerarGUIDSolicitado()
        {
            this.txtGUID.Text = AcessoController.GerarGUID(null);
        }
        #endregion

        #endregion

        #region Aba - Criptografia

        #region Propriedades
        protected Enumeradores.TipoCriptografia TipoCriptogragia { get; set; }
        #endregion

        #region Eventos disparados pelo usuário
        private void rbTipoMD5_Click(object sender, EventArgs e)
        {
            this.OperacaoSelecionarTipodeCriptografiaEnvolvida(Enumeradores.TipoCriptografia.MD5);
        }

        private void rbTipoDES3_Click(object sender, EventArgs e)
        {
            this.OperacaoSelecionarTipodeCriptografiaEnvolvida(Enumeradores.TipoCriptografia.DES3);
        }

        private void rbTipoWEB_Click(object sender, EventArgs e)
        {
            this.OperacaoSelecionarTipodeCriptografiaEnvolvida(Enumeradores.TipoCriptografia.WEB);
        }

        private void btnCriptografar_Click(object sender, EventArgs e)
        {
            this.OperacaoCriptografarStringEnvolvida();
        }

        private void btnDescriptografar_Click(object sender, EventArgs e)
        {
            this.OperacaoDescriptografarStringEnvolvida();
        }

        private void btnLimparCriptografia_Click(object sender, EventArgs e)
        {
            this.OperacaoLimparCenarioCriptografia(false);
        }

        private void btnCopiarEnvolvidoCriptografia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OperacaoCopiarCriptografiaParaAreadeTransferencia(Enumeradores.CampoCriptografiaEnvolvido.Origem);
        }

        private void btnCopiarResultadoCriptografia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OperacaoCopiarCriptografiaParaAreadeTransferencia(Enumeradores.CampoCriptografiaEnvolvido.Destino);
        }
        #endregion

        #region Métodos
        protected void OperacaoSelecionarTipodeCriptografiaEnvolvida(Enumeradores.TipoCriptografia tipo)
        {
            this.TipoCriptogragia = tipo;
            this.rbTipoMD5.Checked = this.rbTipoDES3.Checked = this.rbTipoWEB.Checked = false; // Reiniciando controles

            switch (this.TipoCriptogragia)
            {
                case Enumeradores.TipoCriptografia.MD5:
                    this.rbTipoMD5.Checked = true;
                    break;
                case Enumeradores.TipoCriptografia.DES3:
                    this.rbTipoDES3.Checked = true;
                    break;
                case Enumeradores.TipoCriptografia.WEB:
                    this.rbTipoWEB.Checked = true;
                    break;
            }
        }

        protected void OperacaoCriptografarStringEnvolvida()
        {
            try
            {
                switch (this.TipoCriptogragia)
                {
                    case Enumeradores.TipoCriptografia.MD5:
                        this.txtResultadoCriptografia.Text = new CriptografiaMD5().Criptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                    case Enumeradores.TipoCriptografia.DES3:
                        this.txtResultadoCriptografia.Text = new CriptografiaDES3().Criptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                    case Enumeradores.TipoCriptografia.WEB:
                        this.txtResultadoCriptografia.Text = new CriptografiaWEB().Criptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                }
            }
            catch { MessageBox.Show("Não foi possível criptografar a string informada."); }
        }

        protected void OperacaoDescriptografarStringEnvolvida()
        {
            try
            {
                switch (this.TipoCriptogragia)
                {
                    case Enumeradores.TipoCriptografia.MD5:
                        this.txtResultadoCriptografia.Text = new CriptografiaMD5().Descriptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                    case Enumeradores.TipoCriptografia.DES3:
                        this.txtResultadoCriptografia.Text = new CriptografiaDES3().Descriptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                    case Enumeradores.TipoCriptografia.WEB:
                        this.txtResultadoCriptografia.Text = new CriptografiaWEB().Descriptografar(this.txtEnvolvidoCriptografia.Text);
                        break;
                }
            }
            catch { MessageBox.Show("Não foi possível descriptografar a string informada."); }
        }

        protected void OperacaoLimparCenarioCriptografia(bool reiniciarCenario)
        {
            if (reiniciarCenario) this.OperacaoSelecionarTipodeCriptografiaEnvolvida(Enumeradores.TipoCriptografia.MD5);
            this.txtEnvolvidoCriptografia.Text = this.txtResultadoCriptografia.Text = string.Empty;
        }

        protected void OperacaoCopiarCriptografiaParaAreadeTransferencia(Enumeradores.CampoCriptografiaEnvolvido campo)
        {
            switch (campo)
            {
                case Enumeradores.CampoCriptografiaEnvolvido.Origem:
                    Clipboard.SetDataObject(this.txtEnvolvidoCriptografia.Text, true);
                    break;
                case Enumeradores.CampoCriptografiaEnvolvido.Destino:
                    Clipboard.SetDataObject(this.txtResultadoCriptografia.Text, true);
                    break;
            }
        }
        #endregion

        #endregion        
    }
}
