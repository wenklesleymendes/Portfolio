namespace EMCatraca.Configuracao.ControlesDeUsuario
{
    partial class ControleDispositivo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnCatracaDados = new System.Windows.Forms.Panel();
            this.chkComunicacaoWebApi = new System.Windows.Forms.CheckBox();
            this.lblCatracaCodigo = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.chkGiroInvertido = new System.Windows.Forms.CheckBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.NumericUpDown();
            this.lblCatracaDescricao = new System.Windows.Forms.Label();
            this.lblCatracaIp = new System.Windows.Forms.Label();
            this.lblCatracaPorta = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.pnCatracaLoginSenha = new System.Windows.Forms.Panel();
            this.txtConfirmaSenha = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lbLogin = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnStatusSenha = new System.Windows.Forms.Panel();
            this.txtStatusSenha = new System.Windows.Forms.TextBox();
            this.lbStatusSenha = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridDispositivo = new EMCatraca.Configuracao.Controles.DataGridSelecao();
            this.pnConteudo = new System.Windows.Forms.Panel();
            this.flpControle = new System.Windows.Forms.FlowLayoutPanel();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.pnCatracaDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).BeginInit();
            this.pnCatracaLoginSenha.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            this.pnStatusSenha.SuspendLayout();
            this.pnConteudo.SuspendLayout();
            this.flpControle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnCatracaDados
            // 
            this.pnCatracaDados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnCatracaDados.Controls.Add(this.chkComunicacaoWebApi);
            this.pnCatracaDados.Controls.Add(this.lblCatracaCodigo);
            this.pnCatracaDados.Controls.Add(this.txtDescricao);
            this.pnCatracaDados.Controls.Add(this.chkGiroInvertido);
            this.pnCatracaDados.Controls.Add(this.txtIP);
            this.pnCatracaDados.Controls.Add(this.txtCodigo);
            this.pnCatracaDados.Controls.Add(this.lblCatracaDescricao);
            this.pnCatracaDados.Controls.Add(this.lblCatracaIp);
            this.pnCatracaDados.Controls.Add(this.lblCatracaPorta);
            this.pnCatracaDados.Controls.Add(this.txtPorta);
            this.pnCatracaDados.Location = new System.Drawing.Point(0, 0);
            this.pnCatracaDados.Margin = new System.Windows.Forms.Padding(0);
            this.pnCatracaDados.Name = "pnCatracaDados";
            this.pnCatracaDados.Size = new System.Drawing.Size(396, 137);
            this.pnCatracaDados.TabIndex = 14;
            // 
            // chkComunicacaoWebApi
            // 
            this.chkComunicacaoWebApi.AutoSize = true;
            this.chkComunicacaoWebApi.Location = new System.Drawing.Point(204, 108);
            this.chkComunicacaoWebApi.Name = "chkComunicacaoWebApi";
            this.chkComunicacaoWebApi.Size = new System.Drawing.Size(111, 17);
            this.chkComunicacaoWebApi.TabIndex = 6;
            this.chkComunicacaoWebApi.Text = "Comunicação API";
            this.chkComunicacaoWebApi.UseVisualStyleBackColor = true;
            this.chkComunicacaoWebApi.CheckedChanged += new System.EventHandler(this.chkComunicacaoWebApi_CheckedChanged);
            // 
            // lblCatracaCodigo
            // 
            this.lblCatracaCodigo.AutoSize = true;
            this.lblCatracaCodigo.Location = new System.Drawing.Point(4, 5);
            this.lblCatracaCodigo.Name = "lblCatracaCodigo";
            this.lblCatracaCodigo.Size = new System.Drawing.Size(98, 13);
            this.lblCatracaCodigo.TabIndex = 0;
            this.lblCatracaCodigo.Text = "Código da Catraca:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(110, 29);
            this.txtDescricao.MaxLength = 20;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(271, 20);
            this.txtDescricao.TabIndex = 2;
            this.txtDescricao.TextChanged += new System.EventHandler(this.txtDescricao_TextChanged);
            this.txtDescricao.Leave += new System.EventHandler(this.txtDescricao_Leave);
            // 
            // chkGiroInvertido
            // 
            this.chkGiroInvertido.AutoSize = true;
            this.chkGiroInvertido.Location = new System.Drawing.Point(110, 108);
            this.chkGiroInvertido.Name = "chkGiroInvertido";
            this.chkGiroInvertido.Size = new System.Drawing.Size(88, 17);
            this.chkGiroInvertido.TabIndex = 5;
            this.chkGiroInvertido.Text = "Giro invertido";
            this.chkGiroInvertido.UseVisualStyleBackColor = true;
            this.chkGiroInvertido.CheckedChanged += new System.EventHandler(this.chkGiroInvertido_CheckedChanged);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(110, 54);
            this.txtIP.MaxLength = 15;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(271, 20);
            this.txtIP.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(110, 3);
            this.txtCodigo.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(60, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCatracaDescricao
            // 
            this.lblCatracaDescricao.AutoSize = true;
            this.lblCatracaDescricao.Location = new System.Drawing.Point(4, 29);
            this.lblCatracaDescricao.Name = "lblCatracaDescricao";
            this.lblCatracaDescricao.Size = new System.Drawing.Size(58, 13);
            this.lblCatracaDescricao.TabIndex = 2;
            this.lblCatracaDescricao.Text = "Descrição:";
            // 
            // lblCatracaIp
            // 
            this.lblCatracaIp.AutoSize = true;
            this.lblCatracaIp.Location = new System.Drawing.Point(4, 54);
            this.lblCatracaIp.Name = "lblCatracaIp";
            this.lblCatracaIp.Size = new System.Drawing.Size(20, 13);
            this.lblCatracaIp.TabIndex = 4;
            this.lblCatracaIp.Text = "IP:";
            // 
            // lblCatracaPorta
            // 
            this.lblCatracaPorta.AutoSize = true;
            this.lblCatracaPorta.Location = new System.Drawing.Point(3, 80);
            this.lblCatracaPorta.Name = "lblCatracaPorta";
            this.lblCatracaPorta.Size = new System.Drawing.Size(35, 13);
            this.lblCatracaPorta.TabIndex = 6;
            this.lblCatracaPorta.Text = "Porta:";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(110, 80);
            this.txtPorta.MaxLength = 5;
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(271, 20);
            this.txtPorta.TabIndex = 4;
            // 
            // pnCatracaLoginSenha
            // 
            this.pnCatracaLoginSenha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnCatracaLoginSenha.Controls.Add(this.txtConfirmaSenha);
            this.pnCatracaLoginSenha.Controls.Add(this.txtLogin);
            this.pnCatracaLoginSenha.Controls.Add(this.lbLogin);
            this.pnCatracaLoginSenha.Controls.Add(this.lbPassword);
            this.pnCatracaLoginSenha.Controls.Add(this.txtSenha);
            this.pnCatracaLoginSenha.Location = new System.Drawing.Point(0, 137);
            this.pnCatracaLoginSenha.Margin = new System.Windows.Forms.Padding(0);
            this.pnCatracaLoginSenha.Name = "pnCatracaLoginSenha";
            this.pnCatracaLoginSenha.Size = new System.Drawing.Size(396, 58);
            this.pnCatracaLoginSenha.TabIndex = 15;
            this.pnCatracaLoginSenha.Visible = false;
            // 
            // txtConfirmaSenha
            // 
            this.txtConfirmaSenha.Location = new System.Drawing.Point(250, 32);
            this.txtConfirmaSenha.MaxLength = 5;
            this.txtConfirmaSenha.Name = "txtConfirmaSenha";
            this.txtConfirmaSenha.PasswordChar = '*';
            this.txtConfirmaSenha.Size = new System.Drawing.Size(131, 20);
            this.txtConfirmaSenha.TabIndex = 9;
            this.txtConfirmaSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConfirmaSenha.Leave += new System.EventHandler(this.txtConfimaSenha_Leave);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(109, 5);
            this.txtLogin.MaxLength = 15;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(272, 20);
            this.txtLogin.TabIndex = 7;
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Location = new System.Drawing.Point(4, 8);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(36, 13);
            this.lbLogin.TabIndex = 8;
            this.lbLogin.Text = "Login:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(4, 34);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(41, 13);
            this.lbPassword.TabIndex = 10;
            this.lbPassword.Text = "Senha:";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(109, 32);
            this.txtSenha.MaxLength = 5;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(131, 20);
            this.txtSenha.TabIndex = 8;
            this.txtSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flpFiltros
            // 
            this.flpFiltros.Controls.Add(this.pnCatracaDados);
            this.flpFiltros.Controls.Add(this.pnCatracaLoginSenha);
            this.flpFiltros.Controls.Add(this.pnStatusSenha);
            this.flpFiltros.Controls.Add(this.gridDispositivo);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpFiltros.Location = new System.Drawing.Point(0, 0);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.Size = new System.Drawing.Size(492, 628);
            this.flpFiltros.TabIndex = 58;
            // 
            // pnStatusSenha
            // 
            this.pnStatusSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnStatusSenha.Controls.Add(this.txtStatusSenha);
            this.pnStatusSenha.Controls.Add(this.lbStatusSenha);
            this.pnStatusSenha.Controls.Add(this.panel1);
            this.pnStatusSenha.Location = new System.Drawing.Point(0, 195);
            this.pnStatusSenha.Margin = new System.Windows.Forms.Padding(0);
            this.pnStatusSenha.Name = "pnStatusSenha";
            this.pnStatusSenha.Size = new System.Drawing.Size(396, 27);
            this.pnStatusSenha.TabIndex = 16;
            this.pnStatusSenha.Visible = false;
            // 
            // txtStatusSenha
            // 
            this.txtStatusSenha.BackColor = System.Drawing.SystemColors.Control;
            this.txtStatusSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatusSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatusSenha.Location = new System.Drawing.Point(109, 5);
            this.txtStatusSenha.MaxLength = 15;
            this.txtStatusSenha.Multiline = true;
            this.txtStatusSenha.Name = "txtStatusSenha";
            this.txtStatusSenha.ReadOnly = true;
            this.txtStatusSenha.Size = new System.Drawing.Size(272, 16);
            this.txtStatusSenha.TabIndex = 9;
            this.txtStatusSenha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbStatusSenha
            // 
            this.lbStatusSenha.AutoSize = true;
            this.lbStatusSenha.Location = new System.Drawing.Point(4, 7);
            this.lbStatusSenha.Name = "lbStatusSenha";
            this.lbStatusSenha.Size = new System.Drawing.Size(86, 13);
            this.lbStatusSenha.TabIndex = 8;
            this.lbStatusSenha.Text = "Status da Senha";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 619);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 100);
            this.panel1.TabIndex = 17;
            // 
            // gridDispositivo
            // 
            this.gridDispositivo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gridDispositivo.Location = new System.Drawing.Point(3, 225);
            this.gridDispositivo.MultiSelect = true;
            this.gridDispositivo.Name = "gridDispositivo";
            this.gridDispositivo.ReadOnly = true;
            this.gridDispositivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDispositivo.Size = new System.Drawing.Size(393, 386);
            this.gridDispositivo.TabIndex = 0;
            this.gridDispositivo.Click += new System.EventHandler(this.gridDispositivo_Click);
            // 
            // pnConteudo
            // 
            this.pnConteudo.Controls.Add(this.flpFiltros);
            this.pnConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnConteudo.Location = new System.Drawing.Point(0, 0);
            this.pnConteudo.Name = "pnConteudo";
            this.pnConteudo.Size = new System.Drawing.Size(492, 628);
            this.pnConteudo.TabIndex = 59;
            // 
            // flpControle
            // 
            this.flpControle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpControle.Controls.Add(this.btnIncluir);
            this.flpControle.Controls.Add(this.btnExcluir);
            this.flpControle.Controls.Add(this.btnCancelar);
            this.flpControle.Controls.Add(this.btnSalvar);
            this.flpControle.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpControle.Location = new System.Drawing.Point(396, 3);
            this.flpControle.Name = "flpControle";
            this.flpControle.Size = new System.Drawing.Size(90, 192);
            this.flpControle.TabIndex = 60;
            // 
            // btnIncluir
            // 
            this.btnIncluir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnIncluir.BackColor = System.Drawing.Color.White;
            this.btnIncluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnIncluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnIncluir.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnIncluir.ForeColor = System.Drawing.Color.Black;
            this.btnIncluir.Location = new System.Drawing.Point(13, 4);
            this.btnIncluir.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(75, 30);
            this.btnIncluir.TabIndex = 10;
            this.btnIncluir.Text = "Incluir";
            this.btnIncluir.UseVisualStyleBackColor = false;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnExcluir.ForeColor = System.Drawing.Color.Black;
            this.btnExcluir.Location = new System.Drawing.Point(13, 38);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 30);
            this.btnExcluir.TabIndex = 12;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(13, 72);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSalvar.BackColor = System.Drawing.Color.White;
            this.btnSalvar.Enabled = false;
            this.btnSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnSalvar.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnSalvar.ForeColor = System.Drawing.Color.Black;
            this.btnSalvar.Location = new System.Drawing.Point(13, 106);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 30);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // ControleDispositivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpControle);
            this.Controls.Add(this.pnConteudo);
            this.Name = "ControleDispositivo";
            this.Size = new System.Drawing.Size(492, 628);
            this.Load += new System.EventHandler(this.ControleDispositivo_Load);
            this.pnCatracaDados.ResumeLayout(false);
            this.pnCatracaDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).EndInit();
            this.pnCatracaLoginSenha.ResumeLayout(false);
            this.pnCatracaLoginSenha.PerformLayout();
            this.flpFiltros.ResumeLayout(false);
            this.pnStatusSenha.ResumeLayout(false);
            this.pnStatusSenha.PerformLayout();
            this.pnConteudo.ResumeLayout(false);
            this.flpControle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controles.DataGridSelecao gridDispositivo;
        private System.Windows.Forms.Panel pnCatracaDados;
        private System.Windows.Forms.CheckBox chkComunicacaoWebApi;
        private System.Windows.Forms.Label lblCatracaCodigo;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.CheckBox chkGiroInvertido;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.NumericUpDown txtCodigo;
        private System.Windows.Forms.Label lblCatracaDescricao;
        private System.Windows.Forms.Label lblCatracaIp;
        private System.Windows.Forms.Label lblCatracaPorta;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Panel pnCatracaLoginSenha;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Panel pnConteudo;
        private System.Windows.Forms.TextBox txtConfirmaSenha;
        private System.Windows.Forms.Panel pnStatusSenha;
        private System.Windows.Forms.TextBox txtStatusSenha;
        private System.Windows.Forms.Label lbStatusSenha;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpControle;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalvar;
    }
}
