namespace EasyInnerSDK.UI
{
    partial class FrmOnline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOnline));
            this.tcInnerBIO = new System.Windows.Forms.TabControl();
            this.tbpExOnline = new System.Windows.Forms.TabPage();
            this.gbOnline = new System.Windows.Forms.GroupBox();
            this.gbCadastro = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstBilhetes = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstVersaoInners = new System.Windows.Forms.ListBox();
            this.btnIniciarMaquina = new System.Windows.Forms.Button();
            this.btnRemoverInnerLista = new System.Windows.Forms.Button();
            this.btnAdicionarUsuarioInnerOnline = new System.Windows.Forms.Button();
            this.btnPararMaquina = new System.Windows.Forms.Button();
            this.cmdLimpar = new System.Windows.Forms.Button();
            this.gbMonitoracao = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbCadastrados = new System.Windows.Forms.GroupBox();
            this.lstInnersCadastrados = new System.Windows.Forms.ListBox();
            this.cmdEntrada = new System.Windows.Forms.Button();
            this.cmdSair = new System.Windows.Forms.Button();
            this.gbInners = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTipoLeitor = new System.Windows.Forms.ComboBox();
            this.ckbDoisLeitores = new System.Windows.Forms.CheckBox();
            this.gbParamtros = new System.Windows.Forms.GroupBox();
            this.chkModuloLC = new System.Windows.Forms.CheckBox();
            this.txtCartaoMaster = new System.Windows.Forms.TextBox();
            this.chkCartaoMaster = new System.Windows.Forms.CheckBox();
            this.chkLista = new System.Windows.Forms.CheckBox();
            this.ckbBIO = new System.Windows.Forms.CheckBox();
            this.chkHabilitaTeclado = new System.Windows.Forms.CheckBox();
            this.chkVerificacao = new System.Windows.Forms.CheckBox();
            this.chkIdentificacao = new System.Windows.Forms.CheckBox();
            this.chkListaBio = new System.Windows.Forms.CheckBox();
            this.gbLadoCatraca = new System.Windows.Forms.GroupBox();
            this.imgCatraca = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optDireita = new System.Windows.Forms.RadioButton();
            this.optEsquerda = new System.Windows.Forms.RadioButton();
            this.cboEquipamento = new System.Windows.Forms.ComboBox();
            this.lblTipoEquipamento = new System.Windows.Forms.Label();
            this.cboPadraoCartao = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboTipoConexaoOnline = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPortaOnline = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.udNumeroInner = new System.Windows.Forms.NumericUpDown();
            this.udQtdDigitosCartao = new System.Windows.Forms.NumericUpDown();
            this.lblEmExec1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDados = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tcInnerBIO.SuspendLayout();
            this.tbpExOnline.SuspendLayout();
            this.gbOnline.SuspendLayout();
            this.gbCadastro.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbMonitoracao.SuspendLayout();
            this.gbCadastrados.SuspendLayout();
            this.gbInners.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbParamtros.SuspendLayout();
            this.gbLadoCatraca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCatraca)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortaOnline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNumeroInner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQtdDigitosCartao)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcInnerBIO
            // 
            this.tcInnerBIO.Controls.Add(this.tbpExOnline);
            this.tcInnerBIO.Location = new System.Drawing.Point(2, 0);
            this.tcInnerBIO.Name = "tcInnerBIO";
            this.tcInnerBIO.SelectedIndex = 0;
            this.tcInnerBIO.Size = new System.Drawing.Size(652, 665);
            this.tcInnerBIO.TabIndex = 23;
            // 
            // tbpExOnline
            // 
            this.tbpExOnline.Controls.Add(this.gbOnline);
            this.tbpExOnline.Location = new System.Drawing.Point(4, 22);
            this.tbpExOnline.Name = "tbpExOnline";
            this.tbpExOnline.Padding = new System.Windows.Forms.Padding(3);
            this.tbpExOnline.Size = new System.Drawing.Size(644, 639);
            this.tbpExOnline.TabIndex = 4;
            this.tbpExOnline.Text = "Exemplo Online";
            this.tbpExOnline.UseVisualStyleBackColor = true;
            // 
            // gbOnline
            // 
            this.gbOnline.Controls.Add(this.gbCadastro);
            this.gbOnline.Location = new System.Drawing.Point(6, 6);
            this.gbOnline.Name = "gbOnline";
            this.gbOnline.Size = new System.Drawing.Size(629, 628);
            this.gbOnline.TabIndex = 2;
            this.gbOnline.TabStop = false;
            this.gbOnline.Text = "Modo Online";
            // 
            // gbCadastro
            // 
            this.gbCadastro.Controls.Add(this.groupBox2);
            this.gbCadastro.Controls.Add(this.groupBox3);
            this.gbCadastro.Controls.Add(this.btnIniciarMaquina);
            this.gbCadastro.Controls.Add(this.btnRemoverInnerLista);
            this.gbCadastro.Controls.Add(this.btnAdicionarUsuarioInnerOnline);
            this.gbCadastro.Controls.Add(this.btnPararMaquina);
            this.gbCadastro.Controls.Add(this.cmdLimpar);
            this.gbCadastro.Controls.Add(this.gbMonitoracao);
            this.gbCadastro.Controls.Add(this.gbCadastrados);
            this.gbCadastro.Controls.Add(this.cmdEntrada);
            this.gbCadastro.Controls.Add(this.cmdSair);
            this.gbCadastro.Controls.Add(this.gbInners);
            this.gbCadastro.Location = new System.Drawing.Point(6, 12);
            this.gbCadastro.Name = "gbCadastro";
            this.gbCadastro.Size = new System.Drawing.Size(614, 611);
            this.gbCadastro.TabIndex = 3;
            this.gbCadastro.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstBilhetes);
            this.groupBox2.Location = new System.Drawing.Point(6, 477);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(603, 96);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bilhetes coletados";
            // 
            // lstBilhetes
            // 
            this.lstBilhetes.FormattingEnabled = true;
            this.lstBilhetes.Location = new System.Drawing.Point(6, 19);
            this.lstBilhetes.Name = "lstBilhetes";
            this.lstBilhetes.Size = new System.Drawing.Size(588, 69);
            this.lstBilhetes.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstVersaoInners);
            this.groupBox3.Location = new System.Drawing.Point(6, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(603, 80);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Versão Inner:";
            // 
            // lstVersaoInners
            // 
            this.lstVersaoInners.FormattingEnabled = true;
            this.lstVersaoInners.HorizontalScrollbar = true;
            this.lstVersaoInners.Location = new System.Drawing.Point(9, 17);
            this.lstVersaoInners.Name = "lstVersaoInners";
            this.lstVersaoInners.Size = new System.Drawing.Size(585, 56);
            this.lstVersaoInners.TabIndex = 31;
            // 
            // btnIniciarMaquina
            // 
            this.btnIniciarMaquina.Enabled = false;
            this.btnIniciarMaquina.Location = new System.Drawing.Point(547, 579);
            this.btnIniciarMaquina.Name = "btnIniciarMaquina";
            this.btnIniciarMaquina.Size = new System.Drawing.Size(59, 23);
            this.btnIniciarMaquina.TabIndex = 6;
            this.btnIniciarMaquina.Text = "Iniciar";
            this.btnIniciarMaquina.UseVisualStyleBackColor = true;
            this.btnIniciarMaquina.Click += new System.EventHandler(this.btnIniciarMaquina_Click);
            // 
            // btnRemoverInnerLista
            // 
            this.btnRemoverInnerLista.Location = new System.Drawing.Point(119, 217);
            this.btnRemoverInnerLista.Name = "btnRemoverInnerLista";
            this.btnRemoverInnerLista.Size = new System.Drawing.Size(107, 23);
            this.btnRemoverInnerLista.TabIndex = 4;
            this.btnRemoverInnerLista.Text = "Remover da Lista";
            this.btnRemoverInnerLista.UseVisualStyleBackColor = true;
            this.btnRemoverInnerLista.Click += new System.EventHandler(this.btnRemoverInnerLista_Click);
            // 
            // btnAdicionarUsuarioInnerOnline
            // 
            this.btnAdicionarUsuarioInnerOnline.Location = new System.Drawing.Point(6, 217);
            this.btnAdicionarUsuarioInnerOnline.Name = "btnAdicionarUsuarioInnerOnline";
            this.btnAdicionarUsuarioInnerOnline.Size = new System.Drawing.Size(107, 23);
            this.btnAdicionarUsuarioInnerOnline.TabIndex = 2;
            this.btnAdicionarUsuarioInnerOnline.Text = "Incluir na Lista";
            this.btnAdicionarUsuarioInnerOnline.UseVisualStyleBackColor = true;
            this.btnAdicionarUsuarioInnerOnline.Click += new System.EventHandler(this.btnAdicionarUsuarioInnerOnline_Click);
            // 
            // btnPararMaquina
            // 
            this.btnPararMaquina.Location = new System.Drawing.Point(482, 579);
            this.btnPararMaquina.Name = "btnPararMaquina";
            this.btnPararMaquina.Size = new System.Drawing.Size(59, 23);
            this.btnPararMaquina.TabIndex = 7;
            this.btnPararMaquina.Text = "Parar";
            this.btnPararMaquina.UseVisualStyleBackColor = true;
            this.btnPararMaquina.Click += new System.EventHandler(this.btnPararMaquina_Click);
            // 
            // cmdLimpar
            // 
            this.cmdLimpar.Location = new System.Drawing.Point(412, 579);
            this.cmdLimpar.Name = "cmdLimpar";
            this.cmdLimpar.Size = new System.Drawing.Size(66, 23);
            this.cmdLimpar.TabIndex = 4;
            this.cmdLimpar.Text = "Limpar";
            this.cmdLimpar.UseVisualStyleBackColor = true;
            this.cmdLimpar.Click += new System.EventHandler(this.cmdLimpar_Click);
            // 
            // gbMonitoracao
            // 
            this.gbMonitoracao.Controls.Add(this.lblStatus);
            this.gbMonitoracao.Location = new System.Drawing.Point(6, 428);
            this.gbMonitoracao.Name = "gbMonitoracao";
            this.gbMonitoracao.Size = new System.Drawing.Size(603, 43);
            this.gbMonitoracao.TabIndex = 5;
            this.gbMonitoracao.TabStop = false;
            this.gbMonitoracao.Text = "Status comunicação";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 1;
            // 
            // gbCadastrados
            // 
            this.gbCadastrados.Controls.Add(this.lstInnersCadastrados);
            this.gbCadastrados.Location = new System.Drawing.Point(6, 246);
            this.gbCadastrados.Name = "gbCadastrados";
            this.gbCadastrados.Size = new System.Drawing.Size(603, 90);
            this.gbCadastrados.TabIndex = 4;
            this.gbCadastrados.TabStop = false;
            this.gbCadastrados.Text = "Dispositivos Cadastrados em Memória";
            // 
            // lstInnersCadastrados
            // 
            this.lstInnersCadastrados.FormattingEnabled = true;
            this.lstInnersCadastrados.HorizontalScrollbar = true;
            this.lstInnersCadastrados.Location = new System.Drawing.Point(10, 17);
            this.lstInnersCadastrados.Name = "lstInnersCadastrados";
            this.lstInnersCadastrados.Size = new System.Drawing.Size(584, 69);
            this.lstInnersCadastrados.TabIndex = 5;
            this.lstInnersCadastrados.Click += new System.EventHandler(this.lstInnersCadastrados_Click);
            // 
            // cmdEntrada
            // 
            this.cmdEntrada.Enabled = false;
            this.cmdEntrada.Location = new System.Drawing.Point(6, 579);
            this.cmdEntrada.Name = "cmdEntrada";
            this.cmdEntrada.Size = new System.Drawing.Size(64, 23);
            this.cmdEntrada.TabIndex = 3;
            this.cmdEntrada.Text = "Entrada";
            this.cmdEntrada.UseVisualStyleBackColor = true;
            this.cmdEntrada.Click += new System.EventHandler(this.cmdEntrada_Click);
            // 
            // cmdSair
            // 
            this.cmdSair.Enabled = false;
            this.cmdSair.Location = new System.Drawing.Point(76, 579);
            this.cmdSair.Name = "cmdSair";
            this.cmdSair.Size = new System.Drawing.Size(61, 23);
            this.cmdSair.TabIndex = 2;
            this.cmdSair.Text = "Saida";
            this.cmdSair.UseVisualStyleBackColor = true;
            this.cmdSair.Click += new System.EventHandler(this.cmdSair_Click);
            // 
            // gbInners
            // 
            this.gbInners.Controls.Add(this.groupBox1);
            this.gbInners.Controls.Add(this.gbParamtros);
            this.gbInners.Controls.Add(this.gbLadoCatraca);
            this.gbInners.Controls.Add(this.cboEquipamento);
            this.gbInners.Controls.Add(this.lblTipoEquipamento);
            this.gbInners.Controls.Add(this.cboPadraoCartao);
            this.gbInners.Controls.Add(this.label9);
            this.gbInners.Controls.Add(this.cboTipoConexaoOnline);
            this.gbInners.Controls.Add(this.label11);
            this.gbInners.Controls.Add(this.txtPortaOnline);
            this.gbInners.Controls.Add(this.label10);
            this.gbInners.Controls.Add(this.label7);
            this.gbInners.Controls.Add(this.label8);
            this.gbInners.Controls.Add(this.udNumeroInner);
            this.gbInners.Controls.Add(this.udQtdDigitosCartao);
            this.gbInners.Location = new System.Drawing.Point(6, 7);
            this.gbInners.Name = "gbInners";
            this.gbInners.Size = new System.Drawing.Size(603, 204);
            this.gbInners.TabIndex = 19;
            this.gbInners.TabStop = false;
            this.gbInners.Text = "Configurações";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTipoLeitor);
            this.groupBox1.Controls.Add(this.ckbDoisLeitores);
            this.groupBox1.Location = new System.Drawing.Point(422, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 80);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Leitor:";
            // 
            // cboTipoLeitor
            // 
            this.cboTipoLeitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoLeitor.Location = new System.Drawing.Point(6, 16);
            this.cboTipoLeitor.Name = "cboTipoLeitor";
            this.cboTipoLeitor.Size = new System.Drawing.Size(160, 21);
            this.cboTipoLeitor.TabIndex = 22;
            this.cboTipoLeitor.SelectedIndexChanged += new System.EventHandler(this.cboPadraoCartao_SelectedIndexChanged);
            // 
            // ckbDoisLeitores
            // 
            this.ckbDoisLeitores.AutoSize = true;
            this.ckbDoisLeitores.Enabled = false;
            this.ckbDoisLeitores.Location = new System.Drawing.Point(88, 43);
            this.ckbDoisLeitores.Name = "ckbDoisLeitores";
            this.ckbDoisLeitores.Size = new System.Drawing.Size(78, 17);
            this.ckbDoisLeitores.TabIndex = 26;
            this.ckbDoisLeitores.Text = "2 Leitores?";
            this.ckbDoisLeitores.UseVisualStyleBackColor = true;
            // 
            // gbParamtros
            // 
            this.gbParamtros.Controls.Add(this.chkModuloLC);
            this.gbParamtros.Controls.Add(this.txtCartaoMaster);
            this.gbParamtros.Controls.Add(this.chkCartaoMaster);
            this.gbParamtros.Controls.Add(this.chkLista);
            this.gbParamtros.Controls.Add(this.ckbBIO);
            this.gbParamtros.Controls.Add(this.chkHabilitaTeclado);
            this.gbParamtros.Controls.Add(this.chkVerificacao);
            this.gbParamtros.Controls.Add(this.chkIdentificacao);
            this.gbParamtros.Controls.Add(this.chkListaBio);
            this.gbParamtros.Location = new System.Drawing.Point(6, 105);
            this.gbParamtros.Name = "gbParamtros";
            this.gbParamtros.Size = new System.Drawing.Size(325, 95);
            this.gbParamtros.TabIndex = 36;
            this.gbParamtros.TabStop = false;
            this.gbParamtros.Text = "Parâmetros:";
            // 
            // chkModuloLC
            // 
            this.chkModuloLC.AutoSize = true;
            this.chkModuloLC.Enabled = false;
            this.chkModuloLC.Location = new System.Drawing.Point(163, 73);
            this.chkModuloLC.Name = "chkModuloLC";
            this.chkModuloLC.Size = new System.Drawing.Size(94, 17);
            this.chkModuloLC.TabIndex = 31;
            this.chkModuloLC.Text = "Módulo bio LC";
            this.chkModuloLC.UseVisualStyleBackColor = true;
            // 
            // txtCartaoMaster
            // 
            this.txtCartaoMaster.Enabled = false;
            this.txtCartaoMaster.Location = new System.Drawing.Point(15, 67);
            this.txtCartaoMaster.Name = "txtCartaoMaster";
            this.txtCartaoMaster.Size = new System.Drawing.Size(110, 20);
            this.txtCartaoMaster.TabIndex = 30;
            // 
            // chkCartaoMaster
            // 
            this.chkCartaoMaster.AutoSize = true;
            this.chkCartaoMaster.Location = new System.Drawing.Point(6, 52);
            this.chkCartaoMaster.Name = "chkCartaoMaster";
            this.chkCartaoMaster.Size = new System.Drawing.Size(92, 17);
            this.chkCartaoMaster.TabIndex = 29;
            this.chkCartaoMaster.Text = "Cartão Master";
            this.chkCartaoMaster.UseVisualStyleBackColor = true;
            this.chkCartaoMaster.CheckedChanged += new System.EventHandler(this.chkCartaoMaster_CheckedChanged);
            // 
            // chkLista
            // 
            this.chkLista.AutoSize = true;
            this.chkLista.Location = new System.Drawing.Point(6, 19);
            this.chkLista.Name = "chkLista";
            this.chkLista.Size = new System.Drawing.Size(85, 17);
            this.chkLista.TabIndex = 27;
            this.chkLista.Text = "Lista OffLine";
            this.chkLista.UseVisualStyleBackColor = true;
            // 
            // ckbBIO
            // 
            this.ckbBIO.AutoSize = true;
            this.ckbBIO.Location = new System.Drawing.Point(148, 9);
            this.ckbBIO.Name = "ckbBIO";
            this.ckbBIO.Size = new System.Drawing.Size(69, 17);
            this.ckbBIO.TabIndex = 21;
            this.ckbBIO.Text = "Biometria";
            this.ckbBIO.UseVisualStyleBackColor = true;
            this.ckbBIO.Click += new System.EventHandler(this.ckbBIO_Click);
            // 
            // chkHabilitaTeclado
            // 
            this.chkHabilitaTeclado.AutoSize = true;
            this.chkHabilitaTeclado.Checked = true;
            this.chkHabilitaTeclado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitaTeclado.Location = new System.Drawing.Point(6, 35);
            this.chkHabilitaTeclado.Name = "chkHabilitaTeclado";
            this.chkHabilitaTeclado.Size = new System.Drawing.Size(65, 17);
            this.chkHabilitaTeclado.TabIndex = 23;
            this.chkHabilitaTeclado.Text = "Teclado";
            this.chkHabilitaTeclado.UseVisualStyleBackColor = true;
            // 
            // chkVerificacao
            // 
            this.chkVerificacao.AutoSize = true;
            this.chkVerificacao.Enabled = false;
            this.chkVerificacao.Location = new System.Drawing.Point(163, 41);
            this.chkVerificacao.Name = "chkVerificacao";
            this.chkVerificacao.Size = new System.Drawing.Size(79, 17);
            this.chkVerificacao.TabIndex = 24;
            this.chkVerificacao.Text = "Verificação";
            this.chkVerificacao.UseVisualStyleBackColor = true;
            this.chkVerificacao.Click += new System.EventHandler(this.ckbVerificacao_Click);
            // 
            // chkIdentificacao
            // 
            this.chkIdentificacao.AutoSize = true;
            this.chkIdentificacao.Enabled = false;
            this.chkIdentificacao.Location = new System.Drawing.Point(163, 57);
            this.chkIdentificacao.Name = "chkIdentificacao";
            this.chkIdentificacao.Size = new System.Drawing.Size(87, 17);
            this.chkIdentificacao.TabIndex = 25;
            this.chkIdentificacao.Text = "Identificação";
            this.chkIdentificacao.UseVisualStyleBackColor = true;
            // 
            // chkListaBio
            // 
            this.chkListaBio.AutoSize = true;
            this.chkListaBio.Enabled = false;
            this.chkListaBio.Location = new System.Drawing.Point(163, 25);
            this.chkListaBio.Name = "chkListaBio";
            this.chkListaBio.Size = new System.Drawing.Size(125, 17);
            this.chkListaBio.TabIndex = 28;
            this.chkListaBio.Text = "Lista sem Bio OffLine";
            this.chkListaBio.UseVisualStyleBackColor = true;
            this.chkListaBio.Click += new System.EventHandler(this.ckbListaBio_Click);
            // 
            // gbLadoCatraca
            // 
            this.gbLadoCatraca.Controls.Add(this.imgCatraca);
            this.gbLadoCatraca.Controls.Add(this.panel1);
            this.gbLadoCatraca.Location = new System.Drawing.Point(337, 105);
            this.gbLadoCatraca.Name = "gbLadoCatraca";
            this.gbLadoCatraca.Size = new System.Drawing.Size(257, 95);
            this.gbLadoCatraca.TabIndex = 35;
            this.gbLadoCatraca.TabStop = false;
            this.gbLadoCatraca.Text = "Ao entrar, a catraca está instalada à sua:";
            // 
            // imgCatraca
            // 
            this.imgCatraca.Image = global::EasyInnerSDK.Properties.Resources.nenhum;
            this.imgCatraca.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgCatraca.InitialImage")));
            this.imgCatraca.Location = new System.Drawing.Point(160, 17);
            this.imgCatraca.Name = "imgCatraca";
            this.imgCatraca.Size = new System.Drawing.Size(83, 66);
            this.imgCatraca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgCatraca.TabIndex = 31;
            this.imgCatraca.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optDireita);
            this.panel1.Controls.Add(this.optEsquerda);
            this.panel1.Location = new System.Drawing.Point(10, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 28);
            this.panel1.TabIndex = 32;
            // 
            // optDireita
            // 
            this.optDireita.AutoSize = true;
            this.optDireita.Enabled = false;
            this.optDireita.Location = new System.Drawing.Point(81, 6);
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
            // cboEquipamento
            // 
            this.cboEquipamento.FormattingEnabled = true;
            this.cboEquipamento.Location = new System.Drawing.Point(182, 78);
            this.cboEquipamento.Name = "cboEquipamento";
            this.cboEquipamento.Size = new System.Drawing.Size(234, 21);
            this.cboEquipamento.TabIndex = 34;
            this.cboEquipamento.SelectedIndexChanged += new System.EventHandler(this.cboEquipamento_SelectedIndexChanged);
            // 
            // lblTipoEquipamento
            // 
            this.lblTipoEquipamento.AutoSize = true;
            this.lblTipoEquipamento.Location = new System.Drawing.Point(183, 62);
            this.lblTipoEquipamento.Name = "lblTipoEquipamento";
            this.lblTipoEquipamento.Size = new System.Drawing.Size(96, 13);
            this.lblTipoEquipamento.TabIndex = 33;
            this.lblTipoEquipamento.Text = "Tipo Equipamento:";
            // 
            // cboPadraoCartao
            // 
            this.cboPadraoCartao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPadraoCartao.Location = new System.Drawing.Point(6, 78);
            this.cboPadraoCartao.Name = "cboPadraoCartao";
            this.cboPadraoCartao.Size = new System.Drawing.Size(163, 21);
            this.cboPadraoCartao.TabIndex = 17;
            this.cboPadraoCartao.SelectedIndexChanged += new System.EventHandler(this.cboPadraoCartaoOnline_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Padrão Cartão:";
            // 
            // cboTipoConexaoOnline
            // 
            this.cboTipoConexaoOnline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoConexaoOnline.Location = new System.Drawing.Point(294, 33);
            this.cboTipoConexaoOnline.Name = "cboTipoConexaoOnline";
            this.cboTipoConexaoOnline.Size = new System.Drawing.Size(122, 21);
            this.cboTipoConexaoOnline.TabIndex = 16;
            this.cboTipoConexaoOnline.SelectedIndexChanged += new System.EventHandler(this.cboTipoConexaoOnline_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(291, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Tipo Conexão:";
            // 
            // txtPortaOnline
            // 
            this.txtPortaOnline.Location = new System.Drawing.Point(123, 34);
            this.txtPortaOnline.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtPortaOnline.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPortaOnline.Name = "txtPortaOnline";
            this.txtPortaOnline.Size = new System.Drawing.Size(53, 20);
            this.txtPortaOnline.TabIndex = 18;
            this.txtPortaOnline.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPortaOnline.Value = new decimal(new int[] {
            3570,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(120, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Porta:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Número Inner:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(179, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Qtd de Dígitos:";
            // 
            // udNumeroInner
            // 
            this.udNumeroInner.Location = new System.Drawing.Point(6, 34);
            this.udNumeroInner.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.udNumeroInner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udNumeroInner.Name = "udNumeroInner";
            this.udNumeroInner.Size = new System.Drawing.Size(106, 20);
            this.udNumeroInner.TabIndex = 5;
            this.udNumeroInner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udNumeroInner.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udQtdDigitosCartao
            // 
            this.udQtdDigitosCartao.Location = new System.Drawing.Point(182, 34);
            this.udQtdDigitosCartao.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.udQtdDigitosCartao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udQtdDigitosCartao.Name = "udQtdDigitosCartao";
            this.udQtdDigitosCartao.Size = new System.Drawing.Size(106, 20);
            this.udQtdDigitosCartao.TabIndex = 5;
            this.udQtdDigitosCartao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udQtdDigitosCartao.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // lblEmExec1
            // 
            this.lblEmExec1.Name = "lblEmExec1";
            this.lblEmExec1.Size = new System.Drawing.Size(10, 17);
            this.lblEmExec1.Text = " ";
            // 
            // lblDados
            // 
            this.lblDados.Name = "lblDados";
            this.lblDados.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEmExec1,
            this.lblDados});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(651, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 34;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FrmOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 684);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tcInnerBIO);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOnline";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OnLine";
            this.Load += new System.EventHandler(this.FrmOnline_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmOnline_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainBIO_FormClosing);
            this.tcInnerBIO.ResumeLayout(false);
            this.tbpExOnline.ResumeLayout(false);
            this.gbOnline.ResumeLayout(false);
            this.gbCadastro.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gbMonitoracao.ResumeLayout(false);
            this.gbMonitoracao.PerformLayout();
            this.gbCadastrados.ResumeLayout(false);
            this.gbInners.ResumeLayout(false);
            this.gbInners.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbParamtros.ResumeLayout(false);
            this.gbParamtros.PerformLayout();
            this.gbLadoCatraca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgCatraca)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPortaOnline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNumeroInner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQtdDigitosCartao)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcInnerBIO;
        private System.Windows.Forms.TabPage tbpExOnline;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbOnline;
        private System.Windows.Forms.GroupBox gbCadastro;
        public System.Windows.Forms.Button btnRemoverInnerLista;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button btnAdicionarUsuarioInnerOnline;
        private System.Windows.Forms.GroupBox gbCadastrados;
        public System.Windows.Forms.ListBox lstInnersCadastrados;
        public System.Windows.Forms.Button btnPararMaquina;
        public System.Windows.Forms.Button btnIniciarMaquina;
        public System.Windows.Forms.NumericUpDown udQtdDigitosCartao;
        public System.Windows.Forms.NumericUpDown udNumeroInner;
        public System.Windows.Forms.NumericUpDown txtPortaOnline;
        public System.Windows.Forms.ComboBox cboPadraoCartao;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox cboTipoConexaoOnline;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbInners;
        public System.Windows.Forms.CheckBox ckbBIO;
        public System.Windows.Forms.ComboBox cboTipoLeitor;
        public System.Windows.Forms.CheckBox chkHabilitaTeclado;
        public System.Windows.Forms.CheckBox chkIdentificacao;
        public System.Windows.Forms.CheckBox chkVerificacao;
        public System.Windows.Forms.CheckBox ckbDoisLeitores;
        public System.Windows.Forms.CheckBox chkLista;
        public System.Windows.Forms.CheckBox chkListaBio;
        private System.Windows.Forms.GroupBox gbMonitoracao;
        private System.Windows.Forms.Button cmdLimpar;
        public System.Windows.Forms.Button cmdEntrada;
        public System.Windows.Forms.Button cmdSair;
        public System.Windows.Forms.ListBox lstBilhetes;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton optDireita;
        public System.Windows.Forms.RadioButton optEsquerda;
        private System.Windows.Forms.PictureBox imgCatraca;
        public System.Windows.Forms.ComboBox cboEquipamento;
        public System.Windows.Forms.Label lblTipoEquipamento;
        public System.Windows.Forms.ListBox lstVersaoInners;
        private System.Windows.Forms.GroupBox gbParamtros;
        private System.Windows.Forms.GroupBox gbLadoCatraca;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ToolStripStatusLabel lblEmExec1;
        public System.Windows.Forms.ToolStripStatusLabel lblDados;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.TextBox txtCartaoMaster;
        public System.Windows.Forms.CheckBox chkCartaoMaster;
        public System.Windows.Forms.CheckBox chkModuloLC;
    }
}