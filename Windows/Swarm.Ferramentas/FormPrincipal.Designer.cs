namespace Swarm.Ferramentas
{
    partial class wPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblGUID;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wPrincipal));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControleAcesso = new System.Windows.Forms.TabPage();
            this.btnLimparGUID = new System.Windows.Forms.LinkLabel();
            this.btnCopiarGUID = new System.Windows.Forms.LinkLabel();
            this.btnGerarGUID = new System.Windows.Forms.Button();
            this.txtGUID = new System.Windows.Forms.TextBox();
            this.tabCriptografia = new System.Windows.Forms.TabPage();
            this.btnDescriptografar = new System.Windows.Forms.Button();
            this.btnLimparCriptografia = new System.Windows.Forms.Button();
            this.btnCriptografar = new System.Windows.Forms.Button();
            this.txtResultadoCriptografia = new System.Windows.Forms.TextBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.txtEnvolvidoCriptografia = new System.Windows.Forms.TextBox();
            this.lblComum = new System.Windows.Forms.Label();
            this.rbTipoWEB = new System.Windows.Forms.RadioButton();
            this.rbTipoDES3 = new System.Windows.Forms.RadioButton();
            this.lblTipo = new System.Windows.Forms.Label();
            this.rbTipoMD5 = new System.Windows.Forms.RadioButton();
            this.btnCopiarEnvolvidoCriptografia = new System.Windows.Forms.LinkLabel();
            this.btnCopiarResultadoCriptografia = new System.Windows.Forms.LinkLabel();
            lblGUID = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabControleAcesso.SuspendLayout();
            this.tabCriptografia.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGUID
            // 
            lblGUID.AutoSize = true;
            lblGUID.Location = new System.Drawing.Point(7, 15);
            lblGUID.Name = "lblGUID";
            lblGUID.Size = new System.Drawing.Size(34, 13);
            lblGUID.TabIndex = 5;
            lblGUID.Text = "GUID";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabControleAcesso);
            this.tabControl1.Controls.Add(this.tabCriptografia);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 265);
            this.tabControl1.TabIndex = 0;
            // 
            // tabControleAcesso
            // 
            this.tabControleAcesso.Controls.Add(lblGUID);
            this.tabControleAcesso.Controls.Add(this.btnLimparGUID);
            this.tabControleAcesso.Controls.Add(this.btnCopiarGUID);
            this.tabControleAcesso.Controls.Add(this.btnGerarGUID);
            this.tabControleAcesso.Controls.Add(this.txtGUID);
            this.tabControleAcesso.Location = new System.Drawing.Point(4, 22);
            this.tabControleAcesso.Name = "tabControleAcesso";
            this.tabControleAcesso.Padding = new System.Windows.Forms.Padding(3);
            this.tabControleAcesso.Size = new System.Drawing.Size(567, 224);
            this.tabControleAcesso.TabIndex = 0;
            this.tabControleAcesso.Text = "Controle de Acesso";
            this.tabControleAcesso.UseVisualStyleBackColor = true;
            // 
            // btnLimparGUID
            // 
            this.btnLimparGUID.AutoSize = true;
            this.btnLimparGUID.Location = new System.Drawing.Point(523, 37);
            this.btnLimparGUID.Name = "btnLimparGUID";
            this.btnLimparGUID.Size = new System.Drawing.Size(38, 13);
            this.btnLimparGUID.TabIndex = 4;
            this.btnLimparGUID.TabStop = true;
            this.btnLimparGUID.Text = "Limpar";
            this.btnLimparGUID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnLimparGUID_LinkClicked);
            // 
            // btnCopiarGUID
            // 
            this.btnCopiarGUID.AutoSize = true;
            this.btnCopiarGUID.Location = new System.Drawing.Point(480, 37);
            this.btnCopiarGUID.Name = "btnCopiarGUID";
            this.btnCopiarGUID.Size = new System.Drawing.Size(37, 13);
            this.btnCopiarGUID.TabIndex = 3;
            this.btnCopiarGUID.TabStop = true;
            this.btnCopiarGUID.Text = "Copiar";
            this.btnCopiarGUID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCopiarGUID_LinkClicked);
            // 
            // btnGerarGUID
            // 
            this.btnGerarGUID.Location = new System.Drawing.Point(363, 60);
            this.btnGerarGUID.Name = "btnGerarGUID";
            this.btnGerarGUID.Size = new System.Drawing.Size(111, 23);
            this.btnGerarGUID.TabIndex = 2;
            this.btnGerarGUID.Text = "Gerar";
            this.btnGerarGUID.UseVisualStyleBackColor = true;
            this.btnGerarGUID.Click += new System.EventHandler(this.btnGerarGUID_Click);
            // 
            // txtGUID
            // 
            this.txtGUID.Location = new System.Drawing.Point(6, 34);
            this.txtGUID.Name = "txtGUID";
            this.txtGUID.ReadOnly = true;
            this.txtGUID.ShortcutsEnabled = false;
            this.txtGUID.Size = new System.Drawing.Size(468, 20);
            this.txtGUID.TabIndex = 1;
            // 
            // tabCriptografia
            // 
            this.tabCriptografia.Controls.Add(this.btnCopiarResultadoCriptografia);
            this.tabCriptografia.Controls.Add(this.btnCopiarEnvolvidoCriptografia);
            this.tabCriptografia.Controls.Add(this.btnDescriptografar);
            this.tabCriptografia.Controls.Add(this.btnLimparCriptografia);
            this.tabCriptografia.Controls.Add(this.btnCriptografar);
            this.tabCriptografia.Controls.Add(this.txtResultadoCriptografia);
            this.tabCriptografia.Controls.Add(this.lblResultado);
            this.tabCriptografia.Controls.Add(this.txtEnvolvidoCriptografia);
            this.tabCriptografia.Controls.Add(this.lblComum);
            this.tabCriptografia.Controls.Add(this.rbTipoWEB);
            this.tabCriptografia.Controls.Add(this.rbTipoDES3);
            this.tabCriptografia.Controls.Add(this.lblTipo);
            this.tabCriptografia.Controls.Add(this.rbTipoMD5);
            this.tabCriptografia.Location = new System.Drawing.Point(4, 22);
            this.tabCriptografia.Name = "tabCriptografia";
            this.tabCriptografia.Padding = new System.Windows.Forms.Padding(3);
            this.tabCriptografia.Size = new System.Drawing.Size(567, 239);
            this.tabCriptografia.TabIndex = 1;
            this.tabCriptografia.Text = "Criptografia";
            this.tabCriptografia.UseVisualStyleBackColor = true;
            // 
            // btnDescriptografar
            // 
            this.btnDescriptografar.Location = new System.Drawing.Point(381, 22);
            this.btnDescriptografar.Name = "btnDescriptografar";
            this.btnDescriptografar.Size = new System.Drawing.Size(87, 23);
            this.btnDescriptografar.TabIndex = 6;
            this.btnDescriptografar.Text = "Descriptografar";
            this.btnDescriptografar.UseVisualStyleBackColor = true;
            this.btnDescriptografar.Click += new System.EventHandler(this.btnDescriptografar_Click);
            // 
            // btnLimparCriptografia
            // 
            this.btnLimparCriptografia.Location = new System.Drawing.Point(474, 22);
            this.btnLimparCriptografia.Name = "btnLimparCriptografia";
            this.btnLimparCriptografia.Size = new System.Drawing.Size(87, 23);
            this.btnLimparCriptografia.TabIndex = 7;
            this.btnLimparCriptografia.Text = "Limpar";
            this.btnLimparCriptografia.UseVisualStyleBackColor = true;
            this.btnLimparCriptografia.Click += new System.EventHandler(this.btnLimparCriptografia_Click);
            // 
            // btnCriptografar
            // 
            this.btnCriptografar.Location = new System.Drawing.Point(288, 22);
            this.btnCriptografar.Name = "btnCriptografar";
            this.btnCriptografar.Size = new System.Drawing.Size(87, 23);
            this.btnCriptografar.TabIndex = 5;
            this.btnCriptografar.Text = "Criptografar";
            this.btnCriptografar.UseVisualStyleBackColor = true;
            this.btnCriptografar.Click += new System.EventHandler(this.btnCriptografar_Click);
            // 
            // txtResultadoCriptografia
            // 
            this.txtResultadoCriptografia.Location = new System.Drawing.Point(10, 148);
            this.txtResultadoCriptografia.Multiline = true;
            this.txtResultadoCriptografia.Name = "txtResultadoCriptografia";
            this.txtResultadoCriptografia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultadoCriptografia.Size = new System.Drawing.Size(551, 64);
            this.txtResultadoCriptografia.TabIndex = 4;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(10, 132);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(58, 13);
            this.lblResultado.TabIndex = 0;
            this.lblResultado.Text = "Resultado:";
            // 
            // txtEnvolvidoCriptografia
            // 
            this.txtEnvolvidoCriptografia.Location = new System.Drawing.Point(10, 51);
            this.txtEnvolvidoCriptografia.Multiline = true;
            this.txtEnvolvidoCriptografia.Name = "txtEnvolvidoCriptografia";
            this.txtEnvolvidoCriptografia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEnvolvidoCriptografia.Size = new System.Drawing.Size(551, 64);
            this.txtEnvolvidoCriptografia.TabIndex = 3;
            // 
            // lblComum
            // 
            this.lblComum.AutoSize = true;
            this.lblComum.Location = new System.Drawing.Point(7, 35);
            this.lblComum.Name = "lblComum";
            this.lblComum.Size = new System.Drawing.Size(87, 13);
            this.lblComum.TabIndex = 0;
            this.lblComum.Text = "String Envolvida:";
            // 
            // rbTipoWEB
            // 
            this.rbTipoWEB.AutoSize = true;
            this.rbTipoWEB.Location = new System.Drawing.Point(228, 3);
            this.rbTipoWEB.Name = "rbTipoWEB";
            this.rbTipoWEB.Size = new System.Drawing.Size(48, 17);
            this.rbTipoWEB.TabIndex = 2;
            this.rbTipoWEB.Text = "Web";
            this.rbTipoWEB.UseVisualStyleBackColor = true;
            this.rbTipoWEB.Click += new System.EventHandler(this.rbTipoWEB_Click);
            // 
            // rbTipoDES3
            // 
            this.rbTipoDES3.AutoSize = true;
            this.rbTipoDES3.Location = new System.Drawing.Point(169, 3);
            this.rbTipoDES3.Name = "rbTipoDES3";
            this.rbTipoDES3.Size = new System.Drawing.Size(53, 17);
            this.rbTipoDES3.TabIndex = 1;
            this.rbTipoDES3.Text = "DES3";
            this.rbTipoDES3.UseVisualStyleBackColor = true;
            this.rbTipoDES3.Click += new System.EventHandler(this.rbTipoDES3_Click);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(7, 5);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(102, 13);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo de Criptografia:";
            // 
            // rbTipoMD5
            // 
            this.rbTipoMD5.AutoSize = true;
            this.rbTipoMD5.Checked = true;
            this.rbTipoMD5.Location = new System.Drawing.Point(115, 3);
            this.rbTipoMD5.Name = "rbTipoMD5";
            this.rbTipoMD5.Size = new System.Drawing.Size(48, 17);
            this.rbTipoMD5.TabIndex = 0;
            this.rbTipoMD5.TabStop = true;
            this.rbTipoMD5.Text = "MD5";
            this.rbTipoMD5.UseVisualStyleBackColor = true;
            this.rbTipoMD5.Click += new System.EventHandler(this.rbTipoMD5_Click);
            // 
            // btnCopiarEnvolvidoCriptografia
            // 
            this.btnCopiarEnvolvidoCriptografia.AutoSize = true;
            this.btnCopiarEnvolvidoCriptografia.Location = new System.Drawing.Point(524, 118);
            this.btnCopiarEnvolvidoCriptografia.Name = "btnCopiarEnvolvidoCriptografia";
            this.btnCopiarEnvolvidoCriptografia.Size = new System.Drawing.Size(37, 13);
            this.btnCopiarEnvolvidoCriptografia.TabIndex = 8;
            this.btnCopiarEnvolvidoCriptografia.TabStop = true;
            this.btnCopiarEnvolvidoCriptografia.Text = "Copiar";
            this.btnCopiarEnvolvidoCriptografia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCopiarEnvolvidoCriptografia_LinkClicked);
            // 
            // btnCopiarResultadoCriptografia
            // 
            this.btnCopiarResultadoCriptografia.AutoSize = true;
            this.btnCopiarResultadoCriptografia.Location = new System.Drawing.Point(524, 215);
            this.btnCopiarResultadoCriptografia.Name = "btnCopiarResultadoCriptografia";
            this.btnCopiarResultadoCriptografia.Size = new System.Drawing.Size(37, 13);
            this.btnCopiarResultadoCriptografia.TabIndex = 9;
            this.btnCopiarResultadoCriptografia.TabStop = true;
            this.btnCopiarResultadoCriptografia.Text = "Copiar";
            this.btnCopiarResultadoCriptografia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCopiarResultadoCriptografia_LinkClicked);
            // 
            // wPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 290);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "wPrincipal";
            this.Text = "Swarm Consultoria e Sistemas - Ferramentas";
            this.Shown += new System.EventHandler(this.wPrincipal_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabControleAcesso.ResumeLayout(false);
            this.tabControleAcesso.PerformLayout();
            this.tabCriptografia.ResumeLayout(false);
            this.tabCriptografia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabControleAcesso;
        private System.Windows.Forms.TabPage tabCriptografia;
        private System.Windows.Forms.LinkLabel btnLimparGUID;
        private System.Windows.Forms.LinkLabel btnCopiarGUID;
        private System.Windows.Forms.Button btnGerarGUID;
        private System.Windows.Forms.TextBox txtGUID;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.RadioButton rbTipoWEB;
        private System.Windows.Forms.RadioButton rbTipoDES3;
        private System.Windows.Forms.RadioButton rbTipoMD5;
        private System.Windows.Forms.TextBox txtResultadoCriptografia;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox txtEnvolvidoCriptografia;
        private System.Windows.Forms.Label lblComum;
        private System.Windows.Forms.Button btnLimparCriptografia;
        private System.Windows.Forms.Button btnCriptografar;
        private System.Windows.Forms.Button btnDescriptografar;
        private System.Windows.Forms.LinkLabel btnCopiarResultadoCriptografia;
        private System.Windows.Forms.LinkLabel btnCopiarEnvolvidoCriptografia;
    }
}

