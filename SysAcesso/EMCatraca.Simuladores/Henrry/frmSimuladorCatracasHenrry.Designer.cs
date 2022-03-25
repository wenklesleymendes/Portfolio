namespace EMCatraca.Simuladores.Henrry
{
    partial class frmSimuladorCatracasHenrry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimuladorCatracasHenrry));
            this.pnCatraca = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbComunicacao = new System.Windows.Forms.Label();
            this.btnLimparComandos = new System.Windows.Forms.Button();
            this.btnNaoGiraCatraca = new System.Windows.Forms.Button();
            this.btnGiraCatraca = new System.Windows.Forms.Button();
            this.btnSolicita = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.txtComando = new System.Windows.Forms.TextBox();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlConfiguracaoCatraca = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.lblServidor = new System.Windows.Forms.Label();
            this.lblPorta = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.btnFinaliza = new System.Windows.Forms.Button();
            this.btnLigarCatraca = new System.Windows.Forms.Button();
            this.pnlConfiguracaoRand = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtIniRand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFinalizarRand = new System.Windows.Forms.Button();
            this.btnIniciarRand = new System.Windows.Forms.Button();
            this.txtPessoa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFimRand = new System.Windows.Forms.TextBox();
            this.txtDecricaoDaCatraca = new System.Windows.Forms.TextBox();
            this.pnCatraca.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlConfiguracaoCatraca.SuspendLayout();
            this.pnlConfiguracaoRand.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnCatraca
            // 
            this.pnCatraca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnCatraca.Controls.Add(this.panel1);
            this.pnCatraca.Controls.Add(this.pnlConfiguracaoCatraca);
            this.pnCatraca.Controls.Add(this.pnlConfiguracaoRand);
            this.pnCatraca.Controls.Add(this.txtDecricaoDaCatraca);
            this.pnCatraca.Location = new System.Drawing.Point(9, 10);
            this.pnCatraca.Name = "pnCatraca";
            this.pnCatraca.Size = new System.Drawing.Size(410, 440);
            this.pnCatraca.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbComunicacao);
            this.panel1.Controls.Add(this.btnLimparComandos);
            this.panel1.Controls.Add(this.btnNaoGiraCatraca);
            this.panel1.Controls.Add(this.btnGiraCatraca);
            this.panel1.Controls.Add(this.btnSolicita);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtResultado);
            this.panel1.Controls.Add(this.txtComando);
            this.panel1.Controls.Add(this.txtMatricula);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(6, 125);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 311);
            this.panel1.TabIndex = 86;
            // 
            // lbComunicacao
            // 
            this.lbComunicacao.AutoSize = true;
            this.lbComunicacao.Location = new System.Drawing.Point(183, 7);
            this.lbComunicacao.Name = "lbComunicacao";
            this.lbComunicacao.Size = new System.Drawing.Size(72, 13);
            this.lbComunicacao.TabIndex = 77;
            this.lbComunicacao.Text = "Comunicação";
            // 
            // btnLimparComandos
            // 
            this.btnLimparComandos.BackColor = System.Drawing.Color.White;
            this.btnLimparComandos.ForeColor = System.Drawing.Color.Black;
            this.btnLimparComandos.Location = new System.Drawing.Point(300, 260);
            this.btnLimparComandos.Name = "btnLimparComandos";
            this.btnLimparComandos.Size = new System.Drawing.Size(84, 41);
            this.btnLimparComandos.TabIndex = 75;
            this.btnLimparComandos.Text = "Limpar";
            this.btnLimparComandos.UseVisualStyleBackColor = false;
            this.btnLimparComandos.Click += new System.EventHandler(this.btnLimparComandos_Click);
            // 
            // btnNaoGiraCatraca
            // 
            this.btnNaoGiraCatraca.BackColor = System.Drawing.Color.White;
            this.btnNaoGiraCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnNaoGiraCatraca.Location = new System.Drawing.Point(300, 183);
            this.btnNaoGiraCatraca.Name = "btnNaoGiraCatraca";
            this.btnNaoGiraCatraca.Size = new System.Drawing.Size(84, 41);
            this.btnNaoGiraCatraca.TabIndex = 74;
            this.btnNaoGiraCatraca.Text = "Não Girar Dispositivo";
            this.btnNaoGiraCatraca.UseVisualStyleBackColor = false;
            this.btnNaoGiraCatraca.Click += new System.EventHandler(this.btnNaoGiraCatraca_Click);
            // 
            // btnGiraCatraca
            // 
            this.btnGiraCatraca.BackColor = System.Drawing.Color.White;
            this.btnGiraCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnGiraCatraca.Location = new System.Drawing.Point(300, 110);
            this.btnGiraCatraca.Name = "btnGiraCatraca";
            this.btnGiraCatraca.Size = new System.Drawing.Size(84, 41);
            this.btnGiraCatraca.TabIndex = 73;
            this.btnGiraCatraca.Text = "Gira Dispositivo";
            this.btnGiraCatraca.UseVisualStyleBackColor = false;
            this.btnGiraCatraca.Click += new System.EventHandler(this.btnGiraCatraca_Click);
            // 
            // btnSolicita
            // 
            this.btnSolicita.BackColor = System.Drawing.Color.White;
            this.btnSolicita.ForeColor = System.Drawing.Color.Black;
            this.btnSolicita.Location = new System.Drawing.Point(300, 29);
            this.btnSolicita.Name = "btnSolicita";
            this.btnSolicita.Size = new System.Drawing.Size(84, 41);
            this.btnSolicita.TabIndex = 72;
            this.btnSolicita.Text = "Passar Cartão";
            this.btnSolicita.UseVisualStyleBackColor = false;
            this.btnSolicita.Click += new System.EventHandler(this.btnSolicita_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "Log:";
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(3, 113);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResultado.Size = new System.Drawing.Size(288, 188);
            this.txtResultado.TabIndex = 70;
            // 
            // txtComando
            // 
            this.txtComando.Location = new System.Drawing.Point(3, 74);
            this.txtComando.Name = "txtComando";
            this.txtComando.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComando.Size = new System.Drawing.Size(287, 20);
            this.txtComando.TabIndex = 69;
            // 
            // txtMatricula
            // 
            this.txtMatricula.Location = new System.Drawing.Point(3, 47);
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMatricula.Size = new System.Drawing.Size(105, 20);
            this.txtMatricula.TabIndex = 68;
            this.txtMatricula.TextChanged += new System.EventHandler(this.txtMatricula_TextChanged);
            this.txtMatricula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatricula_KeyPress);
            this.txtMatricula.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMatricula_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "Matricula/Cartão:";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(89, 7);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(79, 13);
            this.lbStatus.TabIndex = 66;
            this.lbStatus.Text = "Ligar a Dispositivo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "Status Dispositivo:";
            // 
            // pnlConfiguracaoCatraca
            // 
            this.pnlConfiguracaoCatraca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlConfiguracaoCatraca.Controls.Add(this.textBox1);
            this.pnlConfiguracaoCatraca.Controls.Add(this.txtServidor);
            this.pnlConfiguracaoCatraca.Controls.Add(this.lblServidor);
            this.pnlConfiguracaoCatraca.Controls.Add(this.lblPorta);
            this.pnlConfiguracaoCatraca.Controls.Add(this.txtPorta);
            this.pnlConfiguracaoCatraca.Controls.Add(this.btnFinaliza);
            this.pnlConfiguracaoCatraca.Controls.Add(this.btnLigarCatraca);
            this.pnlConfiguracaoCatraca.Location = new System.Drawing.Point(7, 24);
            this.pnlConfiguracaoCatraca.Margin = new System.Windows.Forms.Padding(0);
            this.pnlConfiguracaoCatraca.Name = "pnlConfiguracaoCatraca";
            this.pnlConfiguracaoCatraca.Size = new System.Drawing.Size(178, 95);
            this.pnlConfiguracaoCatraca.TabIndex = 83;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Silver;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(174, 15);
            this.textBox1.TabIndex = 76;
            this.textBox1.Text = "Configurações da Dispositivo";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(4, 39);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.ReadOnly = true;
            this.txtServidor.Size = new System.Drawing.Size(84, 20);
            this.txtServidor.TabIndex = 49;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(8, 23);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(30, 13);
            this.lblServidor.TabIndex = 47;
            this.lblServidor.Text = "IPV4";
            // 
            // lblPorta
            // 
            this.lblPorta.AutoSize = true;
            this.lblPorta.Location = new System.Drawing.Point(101, 23);
            this.lblPorta.Name = "lblPorta";
            this.lblPorta.Size = new System.Drawing.Size(32, 13);
            this.lblPorta.TabIndex = 48;
            this.lblPorta.Text = "Porta";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(101, 39);
            this.txtPorta.Margin = new System.Windows.Forms.Padding(0);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.ReadOnly = true;
            this.txtPorta.Size = new System.Drawing.Size(73, 20);
            this.txtPorta.TabIndex = 50;
            this.txtPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnFinaliza
            // 
            this.btnFinaliza.BackColor = System.Drawing.Color.White;
            this.btnFinaliza.ForeColor = System.Drawing.Color.Black;
            this.btnFinaliza.Location = new System.Drawing.Point(91, 65);
            this.btnFinaliza.Margin = new System.Windows.Forms.Padding(0);
            this.btnFinaliza.Name = "btnFinaliza";
            this.btnFinaliza.Size = new System.Drawing.Size(84, 23);
            this.btnFinaliza.TabIndex = 63;
            this.btnFinaliza.Text = "Desligar";
            this.btnFinaliza.UseVisualStyleBackColor = false;
            this.btnFinaliza.Click += new System.EventHandler(this.btnFinaliza_Click);
            // 
            // btnLigarCatraca
            // 
            this.btnLigarCatraca.BackColor = System.Drawing.Color.White;
            this.btnLigarCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnLigarCatraca.Location = new System.Drawing.Point(4, 65);
            this.btnLigarCatraca.Name = "btnLigarCatraca";
            this.btnLigarCatraca.Size = new System.Drawing.Size(84, 23);
            this.btnLigarCatraca.TabIndex = 51;
            this.btnLigarCatraca.Text = "Ligar";
            this.btnLigarCatraca.UseVisualStyleBackColor = false;
            this.btnLigarCatraca.Click += new System.EventHandler(this.btnLigarCatraca_Click);
            // 
            // pnlConfiguracaoRand
            // 
            this.pnlConfiguracaoRand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlConfiguracaoRand.Controls.Add(this.textBox3);
            this.pnlConfiguracaoRand.Controls.Add(this.textBox2);
            this.pnlConfiguracaoRand.Controls.Add(this.txtIniRand);
            this.pnlConfiguracaoRand.Controls.Add(this.label1);
            this.pnlConfiguracaoRand.Controls.Add(this.btnFinalizarRand);
            this.pnlConfiguracaoRand.Controls.Add(this.btnIniciarRand);
            this.pnlConfiguracaoRand.Controls.Add(this.txtPessoa);
            this.pnlConfiguracaoRand.Controls.Add(this.label2);
            this.pnlConfiguracaoRand.Controls.Add(this.label3);
            this.pnlConfiguracaoRand.Controls.Add(this.txtFimRand);
            this.pnlConfiguracaoRand.Location = new System.Drawing.Point(189, 24);
            this.pnlConfiguracaoRand.Margin = new System.Windows.Forms.Padding(0);
            this.pnlConfiguracaoRand.Name = "pnlConfiguracaoRand";
            this.pnlConfiguracaoRand.Size = new System.Drawing.Size(211, 95);
            this.pnlConfiguracaoRand.TabIndex = 84;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(33, 43);
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(58, 13);
            this.textBox3.TabIndex = 78;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Silver;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(207, 15);
            this.textBox2.TabIndex = 77;
            this.textBox2.Text = "Simular Vários acessos.";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIniRand
            // 
            this.txtIniRand.Location = new System.Drawing.Point(93, 39);
            this.txtIniRand.Name = "txtIniRand";
            this.txtIniRand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIniRand.Size = new System.Drawing.Size(53, 20);
            this.txtIniRand.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Inicio:";
            // 
            // btnFinalizarRand
            // 
            this.btnFinalizarRand.BackColor = System.Drawing.Color.White;
            this.btnFinalizarRand.ForeColor = System.Drawing.Color.Black;
            this.btnFinalizarRand.Location = new System.Drawing.Point(121, 65);
            this.btnFinalizarRand.Name = "btnFinalizarRand";
            this.btnFinalizarRand.Size = new System.Drawing.Size(84, 23);
            this.btnFinalizarRand.TabIndex = 70;
            this.btnFinalizarRand.Text = "Parar Rand";
            this.btnFinalizarRand.UseVisualStyleBackColor = false;
            this.btnFinalizarRand.Click += new System.EventHandler(this.btnFinalizarRand_Click);
            // 
            // btnIniciarRand
            // 
            this.btnIniciarRand.BackColor = System.Drawing.Color.White;
            this.btnIniciarRand.ForeColor = System.Drawing.Color.Black;
            this.btnIniciarRand.Location = new System.Drawing.Point(4, 65);
            this.btnIniciarRand.Name = "btnIniciarRand";
            this.btnIniciarRand.Size = new System.Drawing.Size(84, 23);
            this.btnIniciarRand.TabIndex = 65;
            this.btnIniciarRand.Text = "Iniciar Rand";
            this.btnIniciarRand.UseVisualStyleBackColor = false;
            this.btnIniciarRand.Click += new System.EventHandler(this.btnIniciarRand_Click);
            // 
            // txtPessoa
            // 
            this.txtPessoa.Location = new System.Drawing.Point(4, 39);
            this.txtPessoa.Name = "txtPessoa";
            this.txtPessoa.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPessoa.Size = new System.Drawing.Size(29, 20);
            this.txtPessoa.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Fim";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Tipo Pessoa";
            // 
            // txtFimRand
            // 
            this.txtFimRand.Location = new System.Drawing.Point(152, 39);
            this.txtFimRand.Name = "txtFimRand";
            this.txtFimRand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFimRand.Size = new System.Drawing.Size(53, 20);
            this.txtFimRand.TabIndex = 69;
            // 
            // txtDecricaoDaCatraca
            // 
            this.txtDecricaoDaCatraca.BackColor = System.Drawing.Color.Silver;
            this.txtDecricaoDaCatraca.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDecricaoDaCatraca.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDecricaoDaCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDecricaoDaCatraca.ForeColor = System.Drawing.Color.Black;
            this.txtDecricaoDaCatraca.Location = new System.Drawing.Point(0, 0);
            this.txtDecricaoDaCatraca.Margin = new System.Windows.Forms.Padding(0);
            this.txtDecricaoDaCatraca.Name = "txtDecricaoDaCatraca";
            this.txtDecricaoDaCatraca.ReadOnly = true;
            this.txtDecricaoDaCatraca.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDecricaoDaCatraca.Size = new System.Drawing.Size(406, 15);
            this.txtDecricaoDaCatraca.TabIndex = 86;
            this.txtDecricaoDaCatraca.Text = "Dispositivo ";
            this.txtDecricaoDaCatraca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmSimuladorCatracasHenrry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 462);
            this.Controls.Add(this.pnCatraca);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSimuladorCatracasHenrry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Simulador  TCIP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSimuladorCatracasHenrry_FormClosing);
            this.pnCatraca.ResumeLayout(false);
            this.pnCatraca.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlConfiguracaoCatraca.ResumeLayout(false);
            this.pnlConfiguracaoCatraca.PerformLayout();
            this.pnlConfiguracaoRand.ResumeLayout(false);
            this.pnlConfiguracaoRand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnCatraca;
        private System.Windows.Forms.Panel pnlConfiguracaoCatraca;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.Label lblServidor;
        private System.Windows.Forms.Label lblPorta;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Button btnFinaliza;
        private System.Windows.Forms.Button btnLigarCatraca;
        private System.Windows.Forms.Panel pnlConfiguracaoRand;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtIniRand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFinalizarRand;
        private System.Windows.Forms.Button btnIniciarRand;
        private System.Windows.Forms.TextBox txtPessoa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFimRand;
        private System.Windows.Forms.TextBox txtDecricaoDaCatraca;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.TextBox txtComando;
        private System.Windows.Forms.TextBox txtMatricula;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNaoGiraCatraca;
        private System.Windows.Forms.Button btnGiraCatraca;
        private System.Windows.Forms.Button btnSolicita;
        private System.Windows.Forms.Button btnLimparComandos;
        private System.Windows.Forms.Label lbComunicacao;
    }
}