namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.imgLogin = new System.Windows.Forms.PictureBox();
            this.cboOperadores = new System.Windows.Forms.ComboBox();
            this.btnOkLogin = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnFecharLogin = new System.Windows.Forms.Button();
            this.lblSenha = new System.Windows.Forms.Label();
            this.gbLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLogin
            // 
            this.gbLogin.BackColor = System.Drawing.Color.White;
            this.gbLogin.Controls.Add(this.txtSenha);
            this.gbLogin.Controls.Add(this.imgLogin);
            this.gbLogin.Controls.Add(this.cboOperadores);
            this.gbLogin.Controls.Add(this.btnOkLogin);
            this.gbLogin.Controls.Add(this.lblUsuario);
            this.gbLogin.Controls.Add(this.btnFecharLogin);
            this.gbLogin.Controls.Add(this.lblSenha);
            this.gbLogin.Location = new System.Drawing.Point(273, 114);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(274, 124);
            this.gbLogin.TabIndex = 2;
            this.gbLogin.TabStop = false;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(100, 67);
            this.txtSenha.MaxLength = 30;
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(156, 20);
            this.txtSenha.TabIndex = 3;
            this.txtSenha.UseSystemPasswordChar = true;
            this.txtSenha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSenha_KeyPress);
            // 
            // imgLogin
            // 
            this.imgLogin.Image = ((System.Drawing.Image)(resources.GetObject("imgLogin.Image")));
            this.imgLogin.Location = new System.Drawing.Point(6, 12);
            this.imgLogin.Name = "imgLogin";
            this.imgLogin.Size = new System.Drawing.Size(77, 78);
            this.imgLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogin.TabIndex = 11;
            this.imgLogin.TabStop = false;
            // 
            // cboOperadores
            // 
            this.cboOperadores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboOperadores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOperadores.FormattingEnabled = true;
            this.cboOperadores.Location = new System.Drawing.Point(101, 28);
            this.cboOperadores.Name = "cboOperadores";
            this.cboOperadores.Size = new System.Drawing.Size(156, 21);
            this.cboOperadores.TabIndex = 1;
            this.cboOperadores.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CboOperadores_KeyDown);
            this.cboOperadores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboOperadores_KeyPress);
            // 
            // btnOkLogin
            // 
            this.btnOkLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(190)))), ((int)(((byte)(49)))));
            this.btnOkLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOkLogin.ForeColor = System.Drawing.Color.Black;
            this.btnOkLogin.Location = new System.Drawing.Point(100, 94);
            this.btnOkLogin.Name = "btnOkLogin";
            this.btnOkLogin.Size = new System.Drawing.Size(75, 23);
            this.btnOkLogin.TabIndex = 6;
            this.btnOkLogin.Text = "&Ok";
            this.btnOkLogin.UseVisualStyleBackColor = false;
            this.btnOkLogin.Click += new System.EventHandler(this.BtnOkLogin_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(98, 12);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuário";
            // 
            // btnFecharLogin
            // 
            this.btnFecharLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(190)))), ((int)(((byte)(49)))));
            this.btnFecharLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharLogin.ForeColor = System.Drawing.Color.Black;
            this.btnFecharLogin.Location = new System.Drawing.Point(181, 94);
            this.btnFecharLogin.Name = "btnFecharLogin";
            this.btnFecharLogin.Size = new System.Drawing.Size(75, 23);
            this.btnFecharLogin.TabIndex = 7;
            this.btnFecharLogin.Text = "&Fechar";
            this.btnFecharLogin.UseVisualStyleBackColor = false;
            this.btnFecharLogin.Click += new System.EventHandler(this.BtnFecharLogin_Click);
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(98, 51);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(38, 13);
            this.lblSenha.TabIndex = 3;
            this.lblSenha.Text = "Senha";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(584, 334);
            this.ControlBox = false;
            this.Controls.Add(this.gbLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.PictureBox imgLogin;
        private System.Windows.Forms.ComboBox cboOperadores;
        private System.Windows.Forms.Button btnOkLogin;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnFecharLogin;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtSenha;
    }
}