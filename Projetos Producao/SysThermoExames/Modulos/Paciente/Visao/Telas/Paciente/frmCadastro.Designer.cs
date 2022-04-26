using System.Windows.Forms;

namespace MdPaciente.Visao
{
    partial class frmCadastro
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
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.lbPacientesTitulo = new System.Windows.Forms.Label();
            this.BtnCerrar = new System.Windows.Forms.Button();
            this.btnPacienteCancelar = new System.Windows.Forms.Button();
            this.btnPacienteSalvar = new System.Windows.Forms.Button();
            this.lbWhatsapp = new System.Windows.Forms.Label();
            this.lbNome = new System.Windows.Forms.Label();
            this.pbPacienteFoto = new System.Windows.Forms.PictureBox();
            this.txtWhatshapp = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.dtpDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.lblDataNascimento = new System.Windows.Forms.Label();
            this.lbSobrenome = new System.Windows.Forms.Label();
            this.txtSobrenome = new System.Windows.Forms.TextBox();
            this.lbNomeMeio = new System.Windows.Forms.Label();
            this.txtNomeMeio = new System.Windows.Forms.TextBox();
            this.lbSexo = new System.Windows.Forms.Label();
            this.cbSexo = new System.Windows.Forms.ComboBox();
            this.cbEtnia = new System.Windows.Forms.ComboBox();
            this.lbEtnia = new System.Windows.Forms.Label();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPacienteFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.BarraTitulo.Controls.Add(this.lbPacientesTitulo);
            this.BarraTitulo.Controls.Add(this.BtnCerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(550, 38);
            this.BarraTitulo.TabIndex = 16;
            // 
            // lbPacientesTitulo
            // 
            this.lbPacientesTitulo.AutoSize = true;
            this.lbPacientesTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPacientesTitulo.ForeColor = System.Drawing.Color.White;
            this.lbPacientesTitulo.Location = new System.Drawing.Point(189, 11);
            this.lbPacientesTitulo.Name = "lbPacientesTitulo";
            this.lbPacientesTitulo.Size = new System.Drawing.Size(144, 17);
            this.lbPacientesTitulo.TabIndex = 15;
            this.lbPacientesTitulo.Text = "Cadastro de Paciente";
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCerrar.FlatAppearance.BorderSize = 0;
            this.BtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCerrar.Location = new System.Drawing.Point(511, 0);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(38, 38);
            this.BtnCerrar.TabIndex = 4;
            this.BtnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnPacienteCancelar
            // 
            this.btnPacienteCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnPacienteCancelar.FlatAppearance.BorderSize = 0;
            this.btnPacienteCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPacienteCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPacienteCancelar.ForeColor = System.Drawing.Color.White;
            this.btnPacienteCancelar.Location = new System.Drawing.Point(437, 257);
            this.btnPacienteCancelar.Name = "btnPacienteCancelar";
            this.btnPacienteCancelar.Size = new System.Drawing.Size(100, 46);
            this.btnPacienteCancelar.TabIndex = 26;
            this.btnPacienteCancelar.Text = "Cancelar";
            this.btnPacienteCancelar.UseVisualStyleBackColor = false;
            this.btnPacienteCancelar.Click += new System.EventHandler(this.btnOperadorCancelar_Click);
            // 
            // btnPacienteSalvar
            // 
            this.btnPacienteSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnPacienteSalvar.FlatAppearance.BorderSize = 0;
            this.btnPacienteSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPacienteSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPacienteSalvar.ForeColor = System.Drawing.Color.White;
            this.btnPacienteSalvar.Location = new System.Drawing.Point(12, 257);
            this.btnPacienteSalvar.Name = "btnPacienteSalvar";
            this.btnPacienteSalvar.Size = new System.Drawing.Size(100, 46);
            this.btnPacienteSalvar.TabIndex = 25;
            this.btnPacienteSalvar.Text = "Salvar";
            this.btnPacienteSalvar.UseVisualStyleBackColor = false;
            this.btnPacienteSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lbWhatsapp
            // 
            this.lbWhatsapp.AutoSize = true;
            this.lbWhatsapp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWhatsapp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbWhatsapp.Location = new System.Drawing.Point(165, 220);
            this.lbWhatsapp.Name = "lbWhatsapp";
            this.lbWhatsapp.Size = new System.Drawing.Size(72, 17);
            this.lbWhatsapp.TabIndex = 23;
            this.lbWhatsapp.Text = "whatsapp:";
            // 
            // lbNome
            // 
            this.lbNome.AutoSize = true;
            this.lbNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbNome.Location = new System.Drawing.Point(162, 56);
            this.lbNome.Name = "lbNome";
            this.lbNome.Size = new System.Drawing.Size(49, 17);
            this.lbNome.TabIndex = 21;
            this.lbNome.Text = "Nome:";
            // 
            // pbPacienteFoto
            // 
            this.pbPacienteFoto.Image = global::MdPaciente.Properties.Resources.semImagem;
            this.pbPacienteFoto.Location = new System.Drawing.Point(12, 54);
            this.pbPacienteFoto.Name = "pbPacienteFoto";
            this.pbPacienteFoto.Size = new System.Drawing.Size(138, 186);
            this.pbPacienteFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPacienteFoto.TabIndex = 29;
            this.pbPacienteFoto.TabStop = false;
            // 
            // txtWhatshapp
            // 
            this.txtWhatshapp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWhatshapp.Location = new System.Drawing.Point(290, 217);
            this.txtWhatshapp.Name = "txtWhatshapp";
            this.txtWhatshapp.Size = new System.Drawing.Size(247, 23);
            this.txtWhatshapp.TabIndex = 19;
            // 
            // txtNome
            // 
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.Location = new System.Drawing.Point(290, 56);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(247, 23);
            this.txtNome.TabIndex = 17;
            // 
            // dtpDataNascimento
            // 
            this.dtpDataNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataNascimento.Location = new System.Drawing.Point(290, 192);
            this.dtpDataNascimento.Name = "dtpDataNascimento";
            this.dtpDataNascimento.Size = new System.Drawing.Size(247, 20);
            this.dtpDataNascimento.TabIndex = 35;
            // 
            // lblDataNascimento
            // 
            this.lblDataNascimento.AutoSize = true;
            this.lblDataNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataNascimento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDataNascimento.Location = new System.Drawing.Point(164, 190);
            this.lblDataNascimento.Name = "lblDataNascimento";
            this.lblDataNascimento.Size = new System.Drawing.Size(120, 17);
            this.lblDataNascimento.TabIndex = 36;
            this.lblDataNascimento.Text = "Data Nascimento:";
            // 
            // lbSobrenome
            // 
            this.lbSobrenome.AutoSize = true;
            this.lbSobrenome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSobrenome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbSobrenome.Location = new System.Drawing.Point(162, 112);
            this.lbSobrenome.Name = "lbSobrenome";
            this.lbSobrenome.Size = new System.Drawing.Size(85, 17);
            this.lbSobrenome.TabIndex = 41;
            this.lbSobrenome.Text = "Sobrenome:";
            // 
            // txtSobreNome
            // 
            this.txtSobrenome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSobrenome.Location = new System.Drawing.Point(290, 112);
            this.txtSobrenome.Name = "txtSobreNome";
            this.txtSobrenome.Size = new System.Drawing.Size(247, 23);
            this.txtSobrenome.TabIndex = 40;
            // 
            // lbNomeMeio
            // 
            this.lbNomeMeio.AutoSize = true;
            this.lbNomeMeio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNomeMeio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbNomeMeio.Location = new System.Drawing.Point(162, 84);
            this.lbNomeMeio.Name = "lbNomeMeio";
            this.lbNomeMeio.Size = new System.Drawing.Size(103, 17);
            this.lbNomeMeio.TabIndex = 43;
            this.lbNomeMeio.Text = "Nome do Meio:";
            // 
            // txtNomeMeio
            // 
            this.txtNomeMeio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeMeio.Location = new System.Drawing.Point(290, 84);
            this.txtNomeMeio.Name = "txtNomeMeio";
            this.txtNomeMeio.Size = new System.Drawing.Size(247, 23);
            this.txtNomeMeio.TabIndex = 42;
            // 
            // lbSexo
            // 
            this.lbSexo.AutoSize = true;
            this.lbSexo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbSexo.Location = new System.Drawing.Point(162, 140);
            this.lbSexo.Name = "lbSexo";
            this.lbSexo.Size = new System.Drawing.Size(43, 17);
            this.lbSexo.TabIndex = 45;
            this.lbSexo.Text = "Sexo:";
            // 
            // cbSexo
            // 
            this.cbSexo.FormattingEnabled = true;
            this.cbSexo.Items.AddRange(new object[] {
            "Homem",
            "Homem Trans",
            "Mulher",
            "Mulher Trans"});
            this.cbSexo.Location = new System.Drawing.Point(290, 140);
            this.cbSexo.Name = "cbSexo";
            this.cbSexo.Size = new System.Drawing.Size(247, 21);
            this.cbSexo.TabIndex = 46;
            // 
            // cbEtnia
            // 
            this.cbEtnia.FormattingEnabled = true;
            this.cbEtnia.Items.AddRange(new object[] {
            "Asiático",
            "Caucasiano"});
            this.cbEtnia.Location = new System.Drawing.Point(290, 166);
            this.cbEtnia.Name = "cbEtnia";
            this.cbEtnia.Size = new System.Drawing.Size(247, 21);
            this.cbEtnia.TabIndex = 48;
            // 
            // lbEtnia
            // 
            this.lbEtnia.AutoSize = true;
            this.lbEtnia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEtnia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbEtnia.Location = new System.Drawing.Point(164, 166);
            this.lbEtnia.Name = "lbEtnia";
            this.lbEtnia.Size = new System.Drawing.Size(60, 17);
            this.lbEtnia.TabIndex = 47;
            this.lbEtnia.Text = "Ethenia:";
            // 
            // frmCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 320);
            this.Controls.Add(this.cbEtnia);
            this.Controls.Add(this.lbEtnia);
            this.Controls.Add(this.cbSexo);
            this.Controls.Add(this.lbSexo);
            this.Controls.Add(this.lbNomeMeio);
            this.Controls.Add(this.txtNomeMeio);
            this.Controls.Add(this.lbSobrenome);
            this.Controls.Add(this.txtSobrenome);
            this.Controls.Add(this.lblDataNascimento);
            this.Controls.Add(this.dtpDataNascimento);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.pbPacienteFoto);
            this.Controls.Add(this.btnPacienteCancelar);
            this.Controls.Add(this.btnPacienteSalvar);
            this.Controls.Add(this.lbWhatsapp);
            this.Controls.Add(this.lbNome);
            this.Controls.Add(this.txtWhatshapp);
            this.Controls.Add(this.txtNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmUsuario";
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPacienteFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.Label lbPacientesTitulo;
        private System.Windows.Forms.Button BtnCerrar;
        private System.Windows.Forms.PictureBox pbPacienteFoto;
        private System.Windows.Forms.Button btnPacienteCancelar;
        private System.Windows.Forms.Button btnPacienteSalvar;
        private System.Windows.Forms.Label lbWhatsapp;
        private System.Windows.Forms.Label lbNome;
        public System.Windows.Forms.TextBox txtWhatshapp;
        public System.Windows.Forms.TextBox txtNome;
        public System.Windows.Forms.DateTimePicker dtpDataNascimento;
        protected System.Windows.Forms.Label lblDataNascimento;
        private Label lbSobrenome;
        public TextBox txtSobrenome;
        private Label lbNomeMeio;
        public TextBox txtNomeMeio;
        private Label lbSexo;
        private ComboBox cbSexo;
        private ComboBox cbEtnia;
        private Label lbEtnia;
    }
}