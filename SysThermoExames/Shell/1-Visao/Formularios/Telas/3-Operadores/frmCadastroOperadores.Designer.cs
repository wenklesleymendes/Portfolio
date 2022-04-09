using System.Windows.Forms;

namespace Formularios.Telas._2_Operadores
{
    partial class frmCadastroOperador
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
            this.btnCancelar = new MaterialSkin.Controls.MaterialButton();
            this.btnSalvar = new MaterialSkin.Controls.MaterialButton();
            this.chkboxAdministrador = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtboxNome = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.mskboxCPF = new MaterialSkin.Controls.MaterialTextBox();
            this.materialDrawer1 = new MaterialSkin.Controls.MaterialDrawer();
            this.materialDrawer2 = new MaterialSkin.Controls.MaterialDrawer();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.txtboxSenha = new MaterialSkin.Controls.MaterialTextBox();
            this.groupBoxCadastroOperador = new System.Windows.Forms.GroupBox();
            this.txtboxLogin = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.groupBoxCadastroOperador.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCancelar.Depth = 0;
            this.btnCancelar.HighEmphasis = true;
            this.btnCancelar.Icon = null;
            this.btnCancelar.Location = new System.Drawing.Point(6, 436);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancelar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCancelar.Size = new System.Drawing.Size(96, 36);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCancelar.UseAccentColor = false;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSalvar.BackColor = System.Drawing.Color.Red;
            this.btnSalvar.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSalvar.Depth = 0;
            this.btnSalvar.HighEmphasis = true;
            this.btnSalvar.Icon = null;
            this.btnSalvar.Location = new System.Drawing.Point(289, 436);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSalvar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSalvar.Size = new System.Drawing.Size(76, 36);
            this.btnSalvar.TabIndex = 9;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSalvar.UseAccentColor = false;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // chkboxAdministrador
            // 
            this.chkboxAdministrador.AutoSize = true;
            this.chkboxAdministrador.Depth = 0;
            this.chkboxAdministrador.Location = new System.Drawing.Point(16, 314);
            this.chkboxAdministrador.Margin = new System.Windows.Forms.Padding(0);
            this.chkboxAdministrador.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkboxAdministrador.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkboxAdministrador.Name = "chkboxAdministrador";
            this.chkboxAdministrador.ReadOnly = false;
            this.chkboxAdministrador.Ripple = true;
            this.chkboxAdministrador.Size = new System.Drawing.Size(135, 37);
            this.chkboxAdministrador.TabIndex = 6;
            this.chkboxAdministrador.Text = "Administrador";
            this.chkboxAdministrador.UseVisualStyleBackColor = true;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(19, 16);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(43, 19);
            this.materialLabel1.TabIndex = 10;
            this.materialLabel1.Text = "Nome";
            // 
            // txtboxNome
            // 
            this.txtboxNome.AnimateReadOnly = false;
            this.txtboxNome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxNome.Depth = 0;
            this.txtboxNome.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtboxNome.LeadingIcon = null;
            this.txtboxNome.Location = new System.Drawing.Point(16, 36);
            this.txtboxNome.MaxLength = 50;
            this.txtboxNome.MouseState = MaterialSkin.MouseState.OUT;
            this.txtboxNome.Multiline = false;
            this.txtboxNome.Name = "txtboxNome";
            this.txtboxNome.Size = new System.Drawing.Size(326, 50);
            this.txtboxNome.TabIndex = 11;
            this.txtboxNome.Text = "";
            this.txtboxNome.TrailingIcon = null;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(19, 89);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(30, 19);
            this.materialLabel2.TabIndex = 12;
            this.materialLabel2.Text = "CPF";
            // 
            // mskboxCPF
            // 
            this.mskboxCPF.AnimateReadOnly = false;
            this.mskboxCPF.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mskboxCPF.Depth = 0;
            this.mskboxCPF.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mskboxCPF.LeadingIcon = null;
            this.mskboxCPF.Location = new System.Drawing.Point(16, 111);
            this.mskboxCPF.MaxLength = 11;
            this.mskboxCPF.MouseState = MaterialSkin.MouseState.OUT;
            this.mskboxCPF.Multiline = false;
            this.mskboxCPF.Name = "mskboxCPF";
            this.mskboxCPF.Size = new System.Drawing.Size(326, 50);
            this.mskboxCPF.TabIndex = 13;
            this.mskboxCPF.Text = "";
            this.mskboxCPF.TrailingIcon = null;
            // 
            // materialDrawer1
            // 
            this.materialDrawer1.AutoHide = false;
            this.materialDrawer1.AutoShow = false;
            this.materialDrawer1.BackgroundWithAccent = false;
            this.materialDrawer1.BaseTabControl = null;
            this.materialDrawer1.Depth = 0;
            this.materialDrawer1.HighlightWithAccent = true;
            this.materialDrawer1.IndicatorWidth = 0;
            this.materialDrawer1.IsOpen = false;
            this.materialDrawer1.Location = new System.Drawing.Point(-178, 101);
            this.materialDrawer1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDrawer1.Name = "materialDrawer1";
            this.materialDrawer1.ShowIconsWhenHidden = false;
            this.materialDrawer1.Size = new System.Drawing.Size(178, 43);
            this.materialDrawer1.TabIndex = 18;
            this.materialDrawer1.Text = "materialDrawer1";
            this.materialDrawer1.UseColors = false;
            // 
            // materialDrawer2
            // 
            this.materialDrawer2.AutoHide = false;
            this.materialDrawer2.AutoShow = false;
            this.materialDrawer2.BackgroundWithAccent = false;
            this.materialDrawer2.BaseTabControl = null;
            this.materialDrawer2.Depth = 0;
            this.materialDrawer2.HighlightWithAccent = true;
            this.materialDrawer2.IndicatorWidth = 0;
            this.materialDrawer2.IsOpen = false;
            this.materialDrawer2.Location = new System.Drawing.Point(-117, 100);
            this.materialDrawer2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDrawer2.Name = "materialDrawer2";
            this.materialDrawer2.ShowIconsWhenHidden = false;
            this.materialDrawer2.Size = new System.Drawing.Size(117, 44);
            this.materialDrawer2.TabIndex = 19;
            this.materialDrawer2.Text = "materialDrawer2";
            this.materialDrawer2.UseColors = false;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel6.Location = new System.Drawing.Point(19, 239);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(46, 19);
            this.materialLabel6.TabIndex = 26;
            this.materialLabel6.Text = "Senha";
            // 
            // txtboxSenha
            // 
            this.txtboxSenha.AnimateReadOnly = false;
            this.txtboxSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxSenha.Depth = 0;
            this.txtboxSenha.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtboxSenha.LeadingIcon = null;
            this.txtboxSenha.Location = new System.Drawing.Point(16, 261);
            this.txtboxSenha.MaxLength = 20;
            this.txtboxSenha.MouseState = MaterialSkin.MouseState.OUT;
            this.txtboxSenha.Multiline = false;
            this.txtboxSenha.Name = "txtboxSenha";
            this.txtboxSenha.Password = true;
            this.txtboxSenha.Size = new System.Drawing.Size(326, 50);
            this.txtboxSenha.TabIndex = 27;
            this.txtboxSenha.Text = "";
            this.txtboxSenha.TrailingIcon = null;
            // 
            // groupBoxCadastroOperador
            // 
            this.groupBoxCadastroOperador.Controls.Add(this.materialLabel3);
            this.groupBoxCadastroOperador.Controls.Add(this.txtboxLogin);
            this.groupBoxCadastroOperador.Controls.Add(this.txtboxSenha);
            this.groupBoxCadastroOperador.Controls.Add(this.materialLabel6);
            this.groupBoxCadastroOperador.Controls.Add(this.materialDrawer2);
            this.groupBoxCadastroOperador.Controls.Add(this.materialDrawer1);
            this.groupBoxCadastroOperador.Controls.Add(this.mskboxCPF);
            this.groupBoxCadastroOperador.Controls.Add(this.chkboxAdministrador);
            this.groupBoxCadastroOperador.Controls.Add(this.materialLabel2);
            this.groupBoxCadastroOperador.Controls.Add(this.txtboxNome);
            this.groupBoxCadastroOperador.Controls.Add(this.materialLabel1);
            this.groupBoxCadastroOperador.Location = new System.Drawing.Point(6, 67);
            this.groupBoxCadastroOperador.Name = "groupBoxCadastroOperador";
            this.groupBoxCadastroOperador.Size = new System.Drawing.Size(359, 361);
            this.groupBoxCadastroOperador.TabIndex = 10;
            this.groupBoxCadastroOperador.TabStop = false;
            this.groupBoxCadastroOperador.Text = "Cadastro";
            // 
            // txtboxLogin
            // 
            this.txtboxLogin.AnimateReadOnly = false;
            this.txtboxLogin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtboxLogin.Depth = 0;
            this.txtboxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtboxLogin.LeadingIcon = null;
            this.txtboxLogin.Location = new System.Drawing.Point(16, 186);
            this.txtboxLogin.MaxLength = 20;
            this.txtboxLogin.MouseState = MaterialSkin.MouseState.OUT;
            this.txtboxLogin.Multiline = false;
            this.txtboxLogin.Name = "txtboxLogin";
            this.txtboxLogin.Size = new System.Drawing.Size(326, 50);
            this.txtboxLogin.TabIndex = 28;
            this.txtboxLogin.Text = "";
            this.txtboxLogin.TrailingIcon = null;
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(19, 164);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(41, 19);
            this.materialLabel3.TabIndex = 29;
            this.materialLabel3.Text = "Login";
            // 
            // frmCadastroOperador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 481);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.groupBoxCadastroOperador);
            this.Controls.Add(this.btnCancelar);
            this.Name = "frmCadastroOperador";
            this.Padding = new System.Windows.Forms.Padding(3, 55, 3, 3);
            this.Text = "Operadores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCadastroOperador_FormClosing);
            this.groupBoxCadastroOperador.ResumeLayout(false);
            this.groupBoxCadastroOperador.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton btnCancelar;
        private MaterialSkin.Controls.MaterialButton btnSalvar;
        private MaterialSkin.Controls.MaterialCheckbox chkboxAdministrador;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox txtboxNome;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialTextBox mskboxCPF;
        private MaterialSkin.Controls.MaterialDrawer materialDrawer1;
        private MaterialSkin.Controls.MaterialDrawer materialDrawer2;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialTextBox txtboxSenha;
        private GroupBox groupBoxCadastroOperador;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox txtboxLogin;
    }
}