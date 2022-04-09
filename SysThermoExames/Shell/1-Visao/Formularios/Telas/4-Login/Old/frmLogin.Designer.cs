//namespace Formularios.Telas._4_Login
//{
//    partial class frmLogin
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
//            this.txtboxLogin = new MaterialSkin.Controls.MaterialTextBox();
//            this.txtboxSenha = new MaterialSkin.Controls.MaterialTextBox();
//            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
//            this.btnLogin = new MaterialSkin.Controls.MaterialButton();
//            this.SuspendLayout();
//            // 
//            // materialLabel1
//            // 
//            this.materialLabel1.AutoSize = true;
//            this.materialLabel1.Depth = 0;
//            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.materialLabel1.Location = new System.Drawing.Point(11, 64);
//            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
//            this.materialLabel1.Name = "materialLabel1";
//            this.materialLabel1.Size = new System.Drawing.Size(41, 19);
//            this.materialLabel1.TabIndex = 0;
//            this.materialLabel1.Text = "Login";
//            // 
//            // txtboxLogin
//            // 
//            this.txtboxLogin.AnimateReadOnly = false;
//            this.txtboxLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.txtboxLogin.Depth = 0;
//            this.txtboxLogin.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.txtboxLogin.LeadingIcon = null;
//            this.txtboxLogin.Location = new System.Drawing.Point(6, 86);
//            this.txtboxLogin.MaxLength = 20;
//            this.txtboxLogin.MouseState = MaterialSkin.MouseState.OUT;
//            this.txtboxLogin.Multiline = false;
//            this.txtboxLogin.Name = "txtboxLogin";
//            this.txtboxLogin.Size = new System.Drawing.Size(204, 50);
//            this.txtboxLogin.TabIndex = 1;
//            this.txtboxLogin.Text = "";
//            this.txtboxLogin.TrailingIcon = null;
//            // 
//            // txtboxSenha
//            // 
//            this.txtboxSenha.AnimateReadOnly = false;
//            this.txtboxSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.txtboxSenha.Depth = 0;
//            this.txtboxSenha.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.txtboxSenha.LeadingIcon = null;
//            this.txtboxSenha.Location = new System.Drawing.Point(6, 161);
//            this.txtboxSenha.MaxLength = 20;
//            this.txtboxSenha.MouseState = MaterialSkin.MouseState.OUT;
//            this.txtboxSenha.Multiline = false;
//            this.txtboxSenha.Name = "txtboxSenha";
//            this.txtboxSenha.Password = true;
//            this.txtboxSenha.Size = new System.Drawing.Size(204, 50);
//            this.txtboxSenha.TabIndex = 2;
//            this.txtboxSenha.Text = "";
//            this.txtboxSenha.TrailingIcon = null;
//            // 
//            // materialLabel2
//            // 
//            this.materialLabel2.AutoSize = true;
//            this.materialLabel2.Depth = 0;
//            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.materialLabel2.Location = new System.Drawing.Point(11, 139);
//            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
//            this.materialLabel2.Name = "materialLabel2";
//            this.materialLabel2.Size = new System.Drawing.Size(46, 19);
//            this.materialLabel2.TabIndex = 3;
//            this.materialLabel2.Text = "Senha";
//            // 
//            // btnLogin
//            // 
//            this.btnLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.btnLogin.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
//            this.btnLogin.Depth = 0;
//            this.btnLogin.HighEmphasis = true;
//            this.btnLogin.Icon = null;
//            this.btnLogin.Location = new System.Drawing.Point(71, 220);
//            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
//            this.btnLogin.MouseState = MaterialSkin.MouseState.HOVER;
//            this.btnLogin.Name = "btnLogin";
//            this.btnLogin.NoAccentTextColor = System.Drawing.Color.Empty;
//            this.btnLogin.Size = new System.Drawing.Size(77, 36);
//            this.btnLogin.TabIndex = 4;
//            this.btnLogin.Text = "Entrar";
//            this.btnLogin.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
//            this.btnLogin.UseAccentColor = false;
//            this.btnLogin.UseVisualStyleBackColor = true;
//            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
//            // 
//            // frmLogin
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(217, 270);
//            this.Controls.Add(this.btnLogin);
//            this.Controls.Add(this.materialLabel2);
//            this.Controls.Add(this.txtboxSenha);
//            this.Controls.Add(this.txtboxLogin);
//            this.Controls.Add(this.materialLabel1);
//            this.Name = "frmLogin";
//            this.Text = "Login";
//            this.Load += new System.EventHandler(this.frmLogin_Load);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        //private MaterialSkin.Controls.MaterialLabel materialLabel1;
//        public MaterialSkin.Controls.MaterialTextBox txtboxLogin;
//        private MaterialSkin.Controls.MaterialTextBox txtboxSenha;
//        private MaterialSkin.Controls.MaterialLabel materialLabel2;
//        private MaterialSkin.Controls.MaterialButton btnLogin;
//    }
//}