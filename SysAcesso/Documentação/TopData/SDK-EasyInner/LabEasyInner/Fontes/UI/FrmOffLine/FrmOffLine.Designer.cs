namespace EasyInnerSDK.UI
{
    partial class FrmOffLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOffLine));
            this.chkIdentificacao = new System.Windows.Forms.CheckBox();
            this.chkVerificacao = new System.Windows.Forms.CheckBox();
            this.chkListaBio = new System.Windows.Forms.CheckBox();
            this.chkBio = new System.Windows.Forms.CheckBox();
            this.rdbPadraoTopdata = new System.Windows.Forms.RadioButton();
            this.rdbPadraoLivre = new System.Windows.Forms.RadioButton();
            this.chkLista = new System.Windows.Forms.CheckBox();
            this.chkHorarios = new System.Windows.Forms.CheckBox();
            this.chkTeclado = new System.Windows.Forms.CheckBox();
            this.chkSirene = new System.Windows.Forms.CheckBox();
            this.chkRelogio = new System.Windows.Forms.CheckBox();
            this.chkMensagem = new System.Windows.Forms.CheckBox();
            this.lblEnvia = new System.Windows.Forms.Label();
            this.gpbConfiguracoes = new System.Windows.Forms.GroupBox();
            this.chkModuloLC = new System.Windows.Forms.CheckBox();
            this.txtCartaoMaster = new System.Windows.Forms.TextBox();
            this.chkCartaoMaster = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optDireita = new System.Windows.Forms.RadioButton();
            this.optEsquerda = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.imgCatraca = new System.Windows.Forms.PictureBox();
            this.lblCatraca = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEquipamento = new System.Windows.Forms.ComboBox();
            this.lblTipoEquipamento = new System.Windows.Forms.Label();
            this.chkDoisLeitores = new System.Windows.Forms.CheckBox();
            this.gpbVersao = new System.Windows.Forms.GroupBox();
            this.lblVersao = new System.Windows.Forms.TextBox();
            this.cboTipoLeitor = new System.Windows.Forms.ComboBox();
            this.cboTipoConexao = new System.Windows.Forms.ComboBox();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.txtDigitos = new System.Windows.Forms.TextBox();
            this.txtNumInner = new System.Windows.Forms.TextBox();
            this.lblTipoLeitor = new System.Windows.Forms.Label();
            this.lblTipoConexao = new System.Windows.Forms.Label();
            this.lblPorta = new System.Windows.Forms.Label();
            this.lblQdtDigitos = new System.Windows.Forms.Label();
            this.lblInner = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lstBilhetes = new System.Windows.Forms.ListBox();
            this.btnReceber = new System.Windows.Forms.Button();
            this.lblBilhetes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbConfiguracoes.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCatraca)).BeginInit();
            this.gpbVersao.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkIdentificacao
            // 
            this.chkIdentificacao.AutoSize = true;
            this.chkIdentificacao.Enabled = false;
            this.chkIdentificacao.Location = new System.Drawing.Point(227, 279);
            this.chkIdentificacao.Name = "chkIdentificacao";
            this.chkIdentificacao.Size = new System.Drawing.Size(87, 17);
            this.chkIdentificacao.TabIndex = 13;
            this.chkIdentificacao.Text = "Identificação";
            this.chkIdentificacao.UseVisualStyleBackColor = true;
            // 
            // chkVerificacao
            // 
            this.chkVerificacao.AutoSize = true;
            this.chkVerificacao.Enabled = false;
            this.chkVerificacao.Location = new System.Drawing.Point(227, 263);
            this.chkVerificacao.Name = "chkVerificacao";
            this.chkVerificacao.Size = new System.Drawing.Size(79, 17);
            this.chkVerificacao.TabIndex = 12;
            this.chkVerificacao.Text = "Verificação";
            this.chkVerificacao.UseVisualStyleBackColor = true;
            this.chkVerificacao.CheckedChanged += new System.EventHandler(this.chkVerificacao_CheckedChanged);
            // 
            // chkListaBio
            // 
            this.chkListaBio.AutoSize = true;
            this.chkListaBio.Enabled = false;
            this.chkListaBio.Location = new System.Drawing.Point(227, 247);
            this.chkListaBio.Name = "chkListaBio";
            this.chkListaBio.Size = new System.Drawing.Size(88, 17);
            this.chkListaBio.TabIndex = 11;
            this.chkListaBio.Text = "Lista sem Bio";
            this.chkListaBio.UseVisualStyleBackColor = true;
            this.chkListaBio.CheckedChanged += new System.EventHandler(this.chkListaBio_CheckedChanged);
            // 
            // chkBio
            // 
            this.chkBio.AutoSize = true;
            this.chkBio.Location = new System.Drawing.Point(227, 230);
            this.chkBio.Name = "chkBio";
            this.chkBio.Size = new System.Drawing.Size(69, 17);
            this.chkBio.TabIndex = 10;
            this.chkBio.Text = "Biometria";
            this.chkBio.UseVisualStyleBackColor = true;
            this.chkBio.CheckedChanged += new System.EventHandler(this.chkBio_CheckedChanged);
            // 
            // rdbPadraoTopdata
            // 
            this.rdbPadraoTopdata.AutoSize = true;
            this.rdbPadraoTopdata.Location = new System.Drawing.Point(411, 92);
            this.rdbPadraoTopdata.Name = "rdbPadraoTopdata";
            this.rdbPadraoTopdata.Size = new System.Drawing.Size(65, 17);
            this.rdbPadraoTopdata.TabIndex = 9;
            this.rdbPadraoTopdata.Text = "Topdata";
            this.rdbPadraoTopdata.UseVisualStyleBackColor = true;
            this.rdbPadraoTopdata.CheckedChanged += new System.EventHandler(this.rdbPadraoTopdata_CheckedChanged);
            // 
            // rdbPadraoLivre
            // 
            this.rdbPadraoLivre.AutoSize = true;
            this.rdbPadraoLivre.Checked = true;
            this.rdbPadraoLivre.Location = new System.Drawing.Point(356, 92);
            this.rdbPadraoLivre.Name = "rdbPadraoLivre";
            this.rdbPadraoLivre.Size = new System.Drawing.Size(48, 17);
            this.rdbPadraoLivre.TabIndex = 8;
            this.rdbPadraoLivre.TabStop = true;
            this.rdbPadraoLivre.Text = "Livre";
            this.rdbPadraoLivre.UseVisualStyleBackColor = true;
            // 
            // chkLista
            // 
            this.chkLista.AutoSize = true;
            this.chkLista.Enabled = false;
            this.chkLista.Location = new System.Drawing.Point(119, 264);
            this.chkLista.Name = "chkLista";
            this.chkLista.Size = new System.Drawing.Size(48, 17);
            this.chkLista.TabIndex = 7;
            this.chkLista.Text = "Lista";
            this.chkLista.UseVisualStyleBackColor = true;
            // 
            // chkHorarios
            // 
            this.chkHorarios.AutoSize = true;
            this.chkHorarios.Location = new System.Drawing.Point(119, 247);
            this.chkHorarios.Name = "chkHorarios";
            this.chkHorarios.Size = new System.Drawing.Size(65, 17);
            this.chkHorarios.TabIndex = 6;
            this.chkHorarios.Text = "Horários";
            this.chkHorarios.UseVisualStyleBackColor = true;
            this.chkHorarios.CheckedChanged += new System.EventHandler(this.chkHorarios_CheckedChanged);
            // 
            // chkTeclado
            // 
            this.chkTeclado.AutoSize = true;
            this.chkTeclado.Checked = true;
            this.chkTeclado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTeclado.Location = new System.Drawing.Point(342, 257);
            this.chkTeclado.Name = "chkTeclado";
            this.chkTeclado.Size = new System.Drawing.Size(65, 17);
            this.chkTeclado.TabIndex = 5;
            this.chkTeclado.Text = "Teclado";
            this.chkTeclado.UseVisualStyleBackColor = true;
            // 
            // chkSirene
            // 
            this.chkSirene.AutoSize = true;
            this.chkSirene.Enabled = false;
            this.chkSirene.Location = new System.Drawing.Point(119, 282);
            this.chkSirene.Name = "chkSirene";
            this.chkSirene.Size = new System.Drawing.Size(56, 17);
            this.chkSirene.TabIndex = 3;
            this.chkSirene.Text = "Sirene";
            this.chkSirene.UseVisualStyleBackColor = true;
            // 
            // chkRelogio
            // 
            this.chkRelogio.AutoSize = true;
            this.chkRelogio.Location = new System.Drawing.Point(342, 234);
            this.chkRelogio.Name = "chkRelogio";
            this.chkRelogio.Size = new System.Drawing.Size(62, 17);
            this.chkRelogio.TabIndex = 2;
            this.chkRelogio.Text = "Relógio";
            this.chkRelogio.UseVisualStyleBackColor = true;
            // 
            // chkMensagem
            // 
            this.chkMensagem.AutoSize = true;
            this.chkMensagem.Location = new System.Drawing.Point(342, 280);
            this.chkMensagem.Name = "chkMensagem";
            this.chkMensagem.Size = new System.Drawing.Size(81, 17);
            this.chkMensagem.TabIndex = 1;
            this.chkMensagem.Text = "Mensagens";
            this.chkMensagem.UseVisualStyleBackColor = true;
            // 
            // lblEnvia
            // 
            this.lblEnvia.AutoSize = true;
            this.lblEnvia.Location = new System.Drawing.Point(282, 350);
            this.lblEnvia.Name = "lblEnvia";
            this.lblEnvia.Size = new System.Drawing.Size(35, 13);
            this.lblEnvia.TabIndex = 14;
            this.lblEnvia.Text = "status";
            // 
            // gpbConfiguracoes
            // 
            this.gpbConfiguracoes.Controls.Add(this.chkModuloLC);
            this.gpbConfiguracoes.Controls.Add(this.txtCartaoMaster);
            this.gpbConfiguracoes.Controls.Add(this.chkCartaoMaster);
            this.gpbConfiguracoes.Controls.Add(this.panel1);
            this.gpbConfiguracoes.Controls.Add(this.label3);
            this.gpbConfiguracoes.Controls.Add(this.imgCatraca);
            this.gpbConfiguracoes.Controls.Add(this.lblCatraca);
            this.gpbConfiguracoes.Controls.Add(this.label2);
            this.gpbConfiguracoes.Controls.Add(this.chkIdentificacao);
            this.gpbConfiguracoes.Controls.Add(this.cboEquipamento);
            this.gpbConfiguracoes.Controls.Add(this.chkVerificacao);
            this.gpbConfiguracoes.Controls.Add(this.lblTipoEquipamento);
            this.gpbConfiguracoes.Controls.Add(this.chkListaBio);
            this.gpbConfiguracoes.Controls.Add(this.chkDoisLeitores);
            this.gpbConfiguracoes.Controls.Add(this.chkBio);
            this.gpbConfiguracoes.Controls.Add(this.gpbVersao);
            this.gpbConfiguracoes.Controls.Add(this.rdbPadraoTopdata);
            this.gpbConfiguracoes.Controls.Add(this.cboTipoLeitor);
            this.gpbConfiguracoes.Controls.Add(this.rdbPadraoLivre);
            this.gpbConfiguracoes.Controls.Add(this.cboTipoConexao);
            this.gpbConfiguracoes.Controls.Add(this.chkTeclado);
            this.gpbConfiguracoes.Controls.Add(this.chkLista);
            this.gpbConfiguracoes.Controls.Add(this.chkSirene);
            this.gpbConfiguracoes.Controls.Add(this.txtPorta);
            this.gpbConfiguracoes.Controls.Add(this.chkRelogio);
            this.gpbConfiguracoes.Controls.Add(this.chkMensagem);
            this.gpbConfiguracoes.Controls.Add(this.chkHorarios);
            this.gpbConfiguracoes.Controls.Add(this.txtDigitos);
            this.gpbConfiguracoes.Controls.Add(this.txtNumInner);
            this.gpbConfiguracoes.Controls.Add(this.lblTipoLeitor);
            this.gpbConfiguracoes.Controls.Add(this.lblTipoConexao);
            this.gpbConfiguracoes.Controls.Add(this.lblPorta);
            this.gpbConfiguracoes.Controls.Add(this.lblQdtDigitos);
            this.gpbConfiguracoes.Controls.Add(this.lblInner);
            this.gpbConfiguracoes.Location = new System.Drawing.Point(7, 8);
            this.gpbConfiguracoes.Name = "gpbConfiguracoes";
            this.gpbConfiguracoes.Size = new System.Drawing.Size(482, 328);
            this.gpbConfiguracoes.TabIndex = 2;
            this.gpbConfiguracoes.TabStop = false;
            this.gpbConfiguracoes.Text = "Configurações";
            // 
            // chkModuloLC
            // 
            this.chkModuloLC.AutoSize = true;
            this.chkModuloLC.Enabled = false;
            this.chkModuloLC.Location = new System.Drawing.Point(227, 296);
            this.chkModuloLC.Name = "chkModuloLC";
            this.chkModuloLC.Size = new System.Drawing.Size(77, 17);
            this.chkModuloLC.TabIndex = 33;
            this.chkModuloLC.Text = "Módulo LC";
            this.chkModuloLC.UseVisualStyleBackColor = true;
            // 
            // txtCartaoMaster
            // 
            this.txtCartaoMaster.Enabled = false;
            this.txtCartaoMaster.Location = new System.Drawing.Point(31, 300);
            this.txtCartaoMaster.Name = "txtCartaoMaster";
            this.txtCartaoMaster.Size = new System.Drawing.Size(110, 20);
            this.txtCartaoMaster.TabIndex = 32;
            // 
            // chkCartaoMaster
            // 
            this.chkCartaoMaster.AutoSize = true;
            this.chkCartaoMaster.Location = new System.Drawing.Point(22, 285);
            this.chkCartaoMaster.Name = "chkCartaoMaster";
            this.chkCartaoMaster.Size = new System.Drawing.Size(92, 17);
            this.chkCartaoMaster.TabIndex = 31;
            this.chkCartaoMaster.Text = "Cartão Master";
            this.chkCartaoMaster.UseVisualStyleBackColor = true;
            this.chkCartaoMaster.CheckedChanged += new System.EventHandler(this.chkCartaoMaster_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optDireita);
            this.panel1.Controls.Add(this.optEsquerda);
            this.panel1.Location = new System.Drawing.Point(212, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 28);
            this.panel1.TabIndex = 20;
            // 
            // optDireita
            // 
            this.optDireita.AutoSize = true;
            this.optDireita.Enabled = false;
            this.optDireita.Location = new System.Drawing.Point(80, 6);
            this.optDireita.Name = "optDireita";
            this.optDireita.Size = new System.Drawing.Size(55, 17);
            this.optDireita.TabIndex = 21;
            this.optDireita.Text = "Direita";
            this.optDireita.UseVisualStyleBackColor = true;
            this.optDireita.CheckedChanged += new System.EventHandler(this.optDireita_CheckedChanged);
            // 
            // optEsquerda
            // 
            this.optEsquerda.AutoSize = true;
            this.optEsquerda.Enabled = false;
            this.optEsquerda.Location = new System.Drawing.Point(5, 6);
            this.optEsquerda.Name = "optEsquerda";
            this.optEsquerda.Size = new System.Drawing.Size(70, 17);
            this.optEsquerda.TabIndex = 20;
            this.optEsquerda.Text = "Esquerda";
            this.optEsquerda.UseVisualStyleBackColor = true;
            this.optEsquerda.CheckedChanged += new System.EventHandler(this.optEsquerda_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Padrão Cartão:";
            // 
            // imgCatraca
            // 
            this.imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.nenhum;
            this.imgCatraca.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgCatraca.InitialImage")));
            this.imgCatraca.Location = new System.Drawing.Point(362, 158);
            this.imgCatraca.Name = "imgCatraca";
            this.imgCatraca.Size = new System.Drawing.Size(83, 70);
            this.imgCatraca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCatraca.TabIndex = 18;
            this.imgCatraca.TabStop = false;
            // 
            // lblCatraca
            // 
            this.lblCatraca.AutoSize = true;
            this.lblCatraca.Enabled = false;
            this.lblCatraca.Location = new System.Drawing.Point(6, 197);
            this.lblCatraca.Name = "lblCatraca";
            this.lblCatraca.Size = new System.Drawing.Size(201, 13);
            this.lblCatraca.TabIndex = 15;
            this.lblCatraca.Text = "Ao entrar, a catraca está instalada à sua:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Parâmetros:";
            // 
            // cboEquipamento
            // 
            this.cboEquipamento.FormattingEnabled = true;
            this.cboEquipamento.Location = new System.Drawing.Point(104, 126);
            this.cboEquipamento.Name = "cboEquipamento";
            this.cboEquipamento.Size = new System.Drawing.Size(211, 21);
            this.cboEquipamento.TabIndex = 13;
            this.cboEquipamento.SelectedIndexChanged += new System.EventHandler(this.cboEquipamento_SelectedIndexChanged);
            // 
            // lblTipoEquipamento
            // 
            this.lblTipoEquipamento.AutoSize = true;
            this.lblTipoEquipamento.Location = new System.Drawing.Point(4, 129);
            this.lblTipoEquipamento.Name = "lblTipoEquipamento";
            this.lblTipoEquipamento.Size = new System.Drawing.Size(96, 13);
            this.lblTipoEquipamento.TabIndex = 12;
            this.lblTipoEquipamento.Text = "Tipo Equipamento:";
            // 
            // chkDoisLeitores
            // 
            this.chkDoisLeitores.AutoSize = true;
            this.chkDoisLeitores.Enabled = false;
            this.chkDoisLeitores.Location = new System.Drawing.Point(278, 160);
            this.chkDoisLeitores.Name = "chkDoisLeitores";
            this.chkDoisLeitores.Size = new System.Drawing.Size(78, 17);
            this.chkDoisLeitores.TabIndex = 11;
            this.chkDoisLeitores.Text = "2 Leitores?";
            this.chkDoisLeitores.UseVisualStyleBackColor = true;
            // 
            // gpbVersao
            // 
            this.gpbVersao.Controls.Add(this.lblVersao);
            this.gpbVersao.Location = new System.Drawing.Point(207, 22);
            this.gpbVersao.Name = "gpbVersao";
            this.gpbVersao.Size = new System.Drawing.Size(267, 64);
            this.gpbVersao.TabIndex = 10;
            this.gpbVersao.TabStop = false;
            this.gpbVersao.Text = "Versão";
            // 
            // lblVersao
            // 
            this.lblVersao.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblVersao.Location = new System.Drawing.Point(8, 18);
            this.lblVersao.Multiline = true;
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.ReadOnly = true;
            this.lblVersao.Size = new System.Drawing.Size(253, 39);
            this.lblVersao.TabIndex = 0;
            // 
            // cboTipoLeitor
            // 
            this.cboTipoLeitor.FormattingEnabled = true;
            this.cboTipoLeitor.Location = new System.Drawing.Point(104, 153);
            this.cboTipoLeitor.Name = "cboTipoLeitor";
            this.cboTipoLeitor.Size = new System.Drawing.Size(173, 21);
            this.cboTipoLeitor.TabIndex = 9;
            this.cboTipoLeitor.SelectedIndexChanged += new System.EventHandler(this.cboPadraoCartao_SelectedIndexChanged);
            // 
            // cboTipoConexao
            // 
            this.cboTipoConexao.FormattingEnabled = true;
            this.cboTipoConexao.Location = new System.Drawing.Point(104, 99);
            this.cboTipoConexao.Name = "cboTipoConexao";
            this.cboTipoConexao.Size = new System.Drawing.Size(173, 21);
            this.cboTipoConexao.TabIndex = 8;
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(104, 73);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(80, 20);
            this.txtPorta.TabIndex = 7;
            this.txtPorta.Text = "3570";
            // 
            // txtDigitos
            // 
            this.txtDigitos.Location = new System.Drawing.Point(104, 47);
            this.txtDigitos.Name = "txtDigitos";
            this.txtDigitos.Size = new System.Drawing.Size(80, 20);
            this.txtDigitos.TabIndex = 6;
            this.txtDigitos.Text = "14";
            // 
            // txtNumInner
            // 
            this.txtNumInner.Location = new System.Drawing.Point(104, 22);
            this.txtNumInner.Name = "txtNumInner";
            this.txtNumInner.Size = new System.Drawing.Size(80, 20);
            this.txtNumInner.TabIndex = 5;
            this.txtNumInner.Text = "1";
            // 
            // lblTipoLeitor
            // 
            this.lblTipoLeitor.AutoSize = true;
            this.lblTipoLeitor.Location = new System.Drawing.Point(7, 156);
            this.lblTipoLeitor.Name = "lblTipoLeitor";
            this.lblTipoLeitor.Size = new System.Drawing.Size(60, 13);
            this.lblTipoLeitor.TabIndex = 4;
            this.lblTipoLeitor.Text = "Tipo Leitor:";
            // 
            // lblTipoConexao
            // 
            this.lblTipoConexao.AutoSize = true;
            this.lblTipoConexao.Location = new System.Drawing.Point(4, 102);
            this.lblTipoConexao.Name = "lblTipoConexao";
            this.lblTipoConexao.Size = new System.Drawing.Size(76, 13);
            this.lblTipoConexao.TabIndex = 3;
            this.lblTipoConexao.Text = "Tipo Conexão:";
            // 
            // lblPorta
            // 
            this.lblPorta.AutoSize = true;
            this.lblPorta.Location = new System.Drawing.Point(7, 76);
            this.lblPorta.Name = "lblPorta";
            this.lblPorta.Size = new System.Drawing.Size(35, 13);
            this.lblPorta.TabIndex = 2;
            this.lblPorta.Text = "Porta:";
            // 
            // lblQdtDigitos
            // 
            this.lblQdtDigitos.AutoSize = true;
            this.lblQdtDigitos.Location = new System.Drawing.Point(6, 47);
            this.lblQdtDigitos.Name = "lblQdtDigitos";
            this.lblQdtDigitos.Size = new System.Drawing.Size(99, 13);
            this.lblQdtDigitos.TabIndex = 1;
            this.lblQdtDigitos.Text = "Número de Dígitos:";
            // 
            // lblInner
            // 
            this.lblInner.AutoSize = true;
            this.lblInner.Location = new System.Drawing.Point(6, 22);
            this.lblInner.Name = "lblInner";
            this.lblInner.Size = new System.Drawing.Size(74, 13);
            this.lblInner.TabIndex = 0;
            this.lblInner.Text = "Número Inner:";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(7, 345);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(123, 23);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar Configurações";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lstBilhetes
            // 
            this.lstBilhetes.FormattingEnabled = true;
            this.lstBilhetes.Location = new System.Drawing.Point(7, 374);
            this.lstBilhetes.Name = "lstBilhetes";
            this.lstBilhetes.Size = new System.Drawing.Size(482, 108);
            this.lstBilhetes.TabIndex = 3;
            // 
            // btnReceber
            // 
            this.btnReceber.Location = new System.Drawing.Point(7, 488);
            this.btnReceber.Name = "btnReceber";
            this.btnReceber.Size = new System.Drawing.Size(105, 21);
            this.btnReceber.TabIndex = 4;
            this.btnReceber.Text = "Receber Bilhetes";
            this.btnReceber.UseVisualStyleBackColor = true;
            this.btnReceber.Click += new System.EventHandler(this.btnReceber_Click);
            // 
            // lblBilhetes
            // 
            this.lblBilhetes.AutoSize = true;
            this.lblBilhetes.Location = new System.Drawing.Point(123, 496);
            this.lblBilhetes.Name = "lblBilhetes";
            this.lblBilhetes.Size = new System.Drawing.Size(0, 13);
            this.lblBilhetes.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Bilhetes coletados";
            // 
            // FrmOffLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(493, 514);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBilhetes);
            this.Controls.Add(this.lblEnvia);
            this.Controls.Add(this.btnReceber);
            this.Controls.Add(this.lstBilhetes);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.gpbConfiguracoes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOffLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OffLine";
            this.Load += new System.EventHandler(this.FrmOffLine_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmOffLine_FormClosed);
            this.gpbConfiguracoes.ResumeLayout(false);
            this.gpbConfiguracoes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCatraca)).EndInit();
            this.gpbVersao.ResumeLayout(false);
            this.gpbVersao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox chkRelogio;
        public System.Windows.Forms.CheckBox chkMensagem;
        public System.Windows.Forms.GroupBox gpbConfiguracoes;
        public System.Windows.Forms.CheckBox chkIdentificacao;
        public System.Windows.Forms.CheckBox chkVerificacao;
        public System.Windows.Forms.CheckBox chkListaBio;
        public System.Windows.Forms.CheckBox chkBio;
        public System.Windows.Forms.RadioButton rdbPadraoTopdata;
        public System.Windows.Forms.RadioButton rdbPadraoLivre;
        public System.Windows.Forms.CheckBox chkLista;
        public System.Windows.Forms.CheckBox chkHorarios;
        public System.Windows.Forms.CheckBox chkTeclado;
        public System.Windows.Forms.CheckBox chkSirene;
        public System.Windows.Forms.Label lblTipoLeitor;
        public System.Windows.Forms.Label lblTipoConexao;
        public System.Windows.Forms.Label lblPorta;
        public System.Windows.Forms.Label lblQdtDigitos;
        public System.Windows.Forms.Label lblInner;
        public System.Windows.Forms.GroupBox gpbVersao;
        public System.Windows.Forms.TextBox lblVersao;
        public System.Windows.Forms.ComboBox cboTipoLeitor;
        public System.Windows.Forms.ComboBox cboTipoConexao;
        public System.Windows.Forms.TextBox txtPorta;
        public System.Windows.Forms.TextBox txtDigitos;
        public System.Windows.Forms.TextBox txtNumInner;
        public System.Windows.Forms.CheckBox chkDoisLeitores;
        public System.Windows.Forms.Label lblEnvia;
        public System.Windows.Forms.ComboBox cboEquipamento;
        public System.Windows.Forms.Label lblTipoEquipamento;
        public System.Windows.Forms.ListBox lstBilhetes;
        public System.Windows.Forms.Button btnReceber;
        public System.Windows.Forms.Label lblBilhetes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lblCatraca;
        private System.Windows.Forms.PictureBox imgCatraca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton optDireita;
        public System.Windows.Forms.RadioButton optEsquerda;
        public System.Windows.Forms.TextBox txtCartaoMaster;
        public System.Windows.Forms.CheckBox chkCartaoMaster;
        public System.Windows.Forms.CheckBox chkModuloLC;
    }
}