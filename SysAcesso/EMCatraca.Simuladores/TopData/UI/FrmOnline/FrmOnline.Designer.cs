namespace EMCatraca.Simuladores.SimuladorTopData.UI.FrmOnline
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
            this.gbParamtros = new System.Windows.Forms.GroupBox();
            this.txtCartaoMaster = new System.Windows.Forms.TextBox();
            this.chkHabilitaCartaoMaster = new System.Windows.Forms.CheckBox();
            this.chkHabilitaListaOffLine = new System.Windows.Forms.CheckBox();
            this.ckbHabililtaBiometria = new System.Windows.Forms.CheckBox();
            this.chkHabilitaTeclado = new System.Windows.Forms.CheckBox();
            this.chkHabilitaVerificacao = new System.Windows.Forms.CheckBox();
            this.chkHabilitaIdentificacao = new System.Windows.Forms.CheckBox();
            this.chkHabilitaListaSemDigital = new System.Windows.Forms.CheckBox();
            this.lblDados = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbLadoCatraca = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optDireita = new System.Windows.Forms.RadioButton();
            this.optGiroEsquerda = new System.Windows.Forms.RadioButton();
            this.cboTipoEquipamento = new System.Windows.Forms.ComboBox();
            this.lblTipoEquipamento = new System.Windows.Forms.Label();
            this.cboPadraoCartao = new System.Windows.Forms.ComboBox();
            this.cboTipoConexaoOnline = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.udPorta = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.udNumeroInner = new System.Windows.Forms.NumericUpDown();
            this.udQtdDigitosCartao = new System.Windows.Forms.NumericUpDown();
            this.lblEmExec1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstVersaoInners = new System.Windows.Forms.ListBox();
            this.btnIniciarMaquina = new System.Windows.Forms.Button();
            this.btnRemoverInnerLista = new System.Windows.Forms.Button();
            this.lstBilhetes = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdicionarCatracaInner = new System.Windows.Forms.Button();
            this.gbOnline = new System.Windows.Forms.GroupBox();
            this.gbCadastro = new System.Windows.Forms.GroupBox();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnPararMaquina = new System.Windows.Forms.Button();
            this.cmdLimparBilhetes = new System.Windows.Forms.Button();
            this.gbMonitoracao = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbCadastrados = new System.Windows.Forms.GroupBox();
            this.lstInnersCadastrados = new System.Windows.Forms.ListBox();
            this.btnComandoEntrada = new System.Windows.Forms.Button();
            this.cmdSair = new System.Windows.Forms.Button();
            this.gbInners = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTipoLeitor = new System.Windows.Forms.ComboBox();
            this.ckbDoisLeitores = new System.Windows.Forms.CheckBox();
            this.tbpExOnline = new System.Windows.Forms.TabPage();
            this.tcInnerBIO = new System.Windows.Forms.TabControl();
            this.gbParamtros.SuspendLayout();
            this.gbLadoCatraca.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPorta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNumeroInner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQtdDigitosCartao)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbOnline.SuspendLayout();
            this.gbCadastro.SuspendLayout();
            this.gbMonitoracao.SuspendLayout();
            this.gbCadastrados.SuspendLayout();
            this.gbInners.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbpExOnline.SuspendLayout();
            this.tcInnerBIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbParamtros
            // 
            this.gbParamtros.Controls.Add(this.txtCartaoMaster);
            this.gbParamtros.Controls.Add(this.chkHabilitaCartaoMaster);
            this.gbParamtros.Controls.Add(this.chkHabilitaListaOffLine);
            this.gbParamtros.Controls.Add(this.ckbHabililtaBiometria);
            this.gbParamtros.Controls.Add(this.chkHabilitaTeclado);
            this.gbParamtros.Controls.Add(this.chkHabilitaVerificacao);
            this.gbParamtros.Controls.Add(this.chkHabilitaIdentificacao);
            this.gbParamtros.Controls.Add(this.chkHabilitaListaSemDigital);
            this.gbParamtros.Location = new System.Drawing.Point(6, 105);
            this.gbParamtros.Name = "gbParamtros";
            this.gbParamtros.Size = new System.Drawing.Size(363, 95);
            this.gbParamtros.TabIndex = 36;
            this.gbParamtros.TabStop = false;
            this.gbParamtros.Text = "Parâmetros:";
            // 
            // txtCartaoMaster
            // 
            this.txtCartaoMaster.Enabled = false;
            this.txtCartaoMaster.Location = new System.Drawing.Point(15, 72);
            this.txtCartaoMaster.Name = "txtCartaoMaster";
            this.txtCartaoMaster.Size = new System.Drawing.Size(110, 20);
            this.txtCartaoMaster.TabIndex = 30;
            // 
            // chkHabilitaCartaoMaster
            // 
            this.chkHabilitaCartaoMaster.AutoSize = true;
            this.chkHabilitaCartaoMaster.Location = new System.Drawing.Point(6, 52);
            this.chkHabilitaCartaoMaster.Name = "chkHabilitaCartaoMaster";
            this.chkHabilitaCartaoMaster.Size = new System.Drawing.Size(92, 17);
            this.chkHabilitaCartaoMaster.TabIndex = 29;
            this.chkHabilitaCartaoMaster.Text = "Cartão Master";
            this.chkHabilitaCartaoMaster.UseVisualStyleBackColor = true;
            this.chkHabilitaCartaoMaster.CheckStateChanged += new System.EventHandler(this.chkCartaoMaster_CheckedChanged);
            // 
            // chkHabilitaListaOffLine
            // 
            this.chkHabilitaListaOffLine.AutoSize = true;
            this.chkHabilitaListaOffLine.Location = new System.Drawing.Point(6, 19);
            this.chkHabilitaListaOffLine.Name = "chkHabilitaListaOffLine";
            this.chkHabilitaListaOffLine.Size = new System.Drawing.Size(85, 17);
            this.chkHabilitaListaOffLine.TabIndex = 27;
            this.chkHabilitaListaOffLine.Text = "Lista OffLine";
            this.chkHabilitaListaOffLine.UseVisualStyleBackColor = true;
            // 
            // ckbHabililtaFucoesBiometrica
            // 
            this.ckbHabililtaBiometria.AutoSize = true;
            this.ckbHabililtaBiometria.Location = new System.Drawing.Point(197, 9);
            this.ckbHabililtaBiometria.Name = "ckbHabililtaFucoesBiometrica";
            this.ckbHabililtaBiometria.Size = new System.Drawing.Size(69, 17);
            this.ckbHabililtaBiometria.TabIndex = 21;
            this.ckbHabililtaBiometria.Text = "Biometria";
            this.ckbHabililtaBiometria.UseVisualStyleBackColor = true;
            this.ckbHabililtaBiometria.Click += new System.EventHandler(this.ckbHabililtaFucoesBiometrica_Click);
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
            // chkHabilitaVerificacao
            // 
            this.chkHabilitaVerificacao.AutoSize = true;
            this.chkHabilitaVerificacao.Enabled = false;
            this.chkHabilitaVerificacao.Location = new System.Drawing.Point(212, 51);
            this.chkHabilitaVerificacao.Name = "chkHabilitaVerificacao";
            this.chkHabilitaVerificacao.Size = new System.Drawing.Size(79, 17);
            this.chkHabilitaVerificacao.TabIndex = 24;
            this.chkHabilitaVerificacao.Text = "Verificação";
            this.chkHabilitaVerificacao.UseVisualStyleBackColor = true;
            this.chkHabilitaVerificacao.Click += new System.EventHandler(this.ckbVerificacao_Click);
            // 
            // chkHabilitaIdentificacao
            // 
            this.chkHabilitaIdentificacao.AutoSize = true;
            this.chkHabilitaIdentificacao.Enabled = false;
            this.chkHabilitaIdentificacao.Location = new System.Drawing.Point(212, 72);
            this.chkHabilitaIdentificacao.Name = "chkHabilitaIdentificacao";
            this.chkHabilitaIdentificacao.Size = new System.Drawing.Size(87, 17);
            this.chkHabilitaIdentificacao.TabIndex = 25;
            this.chkHabilitaIdentificacao.Text = "Identificação";
            this.chkHabilitaIdentificacao.UseVisualStyleBackColor = true;
            // 
            // chkHabilitaListaSemDigital
            // 
            this.chkHabilitaListaSemDigital.AutoSize = true;
            this.chkHabilitaListaSemDigital.Enabled = false;
            this.chkHabilitaListaSemDigital.Location = new System.Drawing.Point(212, 30);
            this.chkHabilitaListaSemDigital.Name = "chkHabilitaListaSemDigital";
            this.chkHabilitaListaSemDigital.Size = new System.Drawing.Size(125, 17);
            this.chkHabilitaListaSemDigital.TabIndex = 28;
            this.chkHabilitaListaSemDigital.Text = "Lista sem Bio OffLine";
            this.chkHabilitaListaSemDigital.UseVisualStyleBackColor = true;
            this.chkHabilitaListaSemDigital.Click += new System.EventHandler(this.ckbListaBio_Click);
            // 
            // lblDados
            // 
            this.lblDados.Name = "lblDados";
            this.lblDados.Size = new System.Drawing.Size(0, 17);
            // 
            // gbLadoCatraca
            // 
            this.gbLadoCatraca.Controls.Add(this.panel1);
            this.gbLadoCatraca.Location = new System.Drawing.Point(375, 105);
            this.gbLadoCatraca.Name = "gbLadoCatraca";
            this.gbLadoCatraca.Size = new System.Drawing.Size(219, 95);
            this.gbLadoCatraca.TabIndex = 35;
            this.gbLadoCatraca.TabStop = false;
            this.gbLadoCatraca.Text = "Ao entrar, a catraca está instalada à sua:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optDireita);
            this.panel1.Controls.Add(this.optGiroEsquerda);
            this.panel1.Location = new System.Drawing.Point(10, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 28);
            this.panel1.TabIndex = 32;
            // 
            // optDireita
            // 
            this.optDireita.AutoSize = true;
            this.optDireita.Checked = true;
            this.optDireita.Enabled = false;
            this.optDireita.Location = new System.Drawing.Point(81, 6);
            this.optDireita.Name = "optDireita";
            this.optDireita.Size = new System.Drawing.Size(55, 17);
            this.optDireita.TabIndex = 21;
            this.optDireita.TabStop = true;
            this.optDireita.Text = "Direita";
            this.optDireita.UseVisualStyleBackColor = true;
            // 
            // optGiroEsquerda
            // 
            this.optGiroEsquerda.AutoSize = true;
            this.optGiroEsquerda.Enabled = false;
            this.optGiroEsquerda.Location = new System.Drawing.Point(5, 6);
            this.optGiroEsquerda.Name = "optGiroEsquerda";
            this.optGiroEsquerda.Size = new System.Drawing.Size(70, 17);
            this.optGiroEsquerda.TabIndex = 20;
            this.optGiroEsquerda.Text = "Esquerda";
            this.optGiroEsquerda.UseVisualStyleBackColor = true;
            // 
            // cboTipoEquipamento
            // 
            this.cboTipoEquipamento.FormattingEnabled = true;
            this.cboTipoEquipamento.Location = new System.Drawing.Point(149, 78);
            this.cboTipoEquipamento.Name = "cboTipoEquipamento";
            this.cboTipoEquipamento.Size = new System.Drawing.Size(220, 21);
            this.cboTipoEquipamento.TabIndex = 34;
            // 
            // lblTipoEquipamento
            // 
            this.lblTipoEquipamento.AutoSize = true;
            this.lblTipoEquipamento.Location = new System.Drawing.Point(150, 62);
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
            this.cboPadraoCartao.Size = new System.Drawing.Size(138, 21);
            this.cboPadraoCartao.TabIndex = 17;
            // 
            // cboTipoConexaoOnline
            // 
            this.cboTipoConexaoOnline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoConexaoOnline.Location = new System.Drawing.Point(224, 33);
            this.cboTipoConexaoOnline.Name = "cboTipoConexaoOnline";
            this.cboTipoConexaoOnline.Size = new System.Drawing.Size(145, 21);
            this.cboTipoConexaoOnline.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(221, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Tipo Conexão:";
            // 
            // udPorta
            // 
            this.udPorta.Location = new System.Drawing.Point(82, 34);
            this.udPorta.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udPorta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPorta.Name = "udPorta";
            this.udPorta.Size = new System.Drawing.Size(53, 20);
            this.udPorta.TabIndex = 18;
            this.udPorta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udPorta.Value = new decimal(new int[] {
            3570,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(79, 19);
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
            this.label8.Location = new System.Drawing.Point(138, 19);
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
            this.udNumeroInner.Size = new System.Drawing.Size(71, 20);
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
            this.udQtdDigitosCartao.Location = new System.Drawing.Point(141, 34);
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
            this.udQtdDigitosCartao.Size = new System.Drawing.Size(76, 20);
            this.udQtdDigitosCartao.TabIndex = 5;
            this.udQtdDigitosCartao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udQtdDigitosCartao.Value = new decimal(new int[] {
            10,
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
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Padrão Cartão:";
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
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
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
            this.btnRemoverInnerLista.Location = new System.Drawing.Point(305, 217);
            this.btnRemoverInnerLista.Name = "btnRemoverInnerLista";
            this.btnRemoverInnerLista.Size = new System.Drawing.Size(107, 23);
            this.btnRemoverInnerLista.TabIndex = 4;
            this.btnRemoverInnerLista.Text = "Remover da Lista";
            this.btnRemoverInnerLista.UseVisualStyleBackColor = true;
            this.btnRemoverInnerLista.Click += new System.EventHandler(this.btnRemoverInnerLista_Click);
            // 
            // lstBilhetes
            // 
            this.lstBilhetes.FormattingEnabled = true;
            this.lstBilhetes.Location = new System.Drawing.Point(6, 19);
            this.lstBilhetes.Name = "lstBilhetes";
            this.lstBilhetes.Size = new System.Drawing.Size(588, 69);
            this.lstBilhetes.TabIndex = 0;
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
            // btnAdicionarCatracaInner
            // 
            this.btnAdicionarCatracaInner.Location = new System.Drawing.Point(6, 217);
            this.btnAdicionarCatracaInner.Name = "btnAdicionarCatracaInner";
            this.btnAdicionarCatracaInner.Size = new System.Drawing.Size(107, 23);
            this.btnAdicionarCatracaInner.TabIndex = 2;
            this.btnAdicionarCatracaInner.Text = "Incluir na Lista";
            this.btnAdicionarCatracaInner.UseVisualStyleBackColor = true;
            this.btnAdicionarCatracaInner.Click += new System.EventHandler(this.btnAdicionarCatracaInnerOnline_Click);
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
            this.gbCadastro.Controls.Add(this.btnAlterar);
            this.gbCadastro.Controls.Add(this.groupBox2);
            this.gbCadastro.Controls.Add(this.groupBox3);
            this.gbCadastro.Controls.Add(this.btnIniciarMaquina);
            this.gbCadastro.Controls.Add(this.btnRemoverInnerLista);
            this.gbCadastro.Controls.Add(this.btnAdicionarCatracaInner);
            this.gbCadastro.Controls.Add(this.btnPararMaquina);
            this.gbCadastro.Controls.Add(this.cmdLimparBilhetes);
            this.gbCadastro.Controls.Add(this.gbMonitoracao);
            this.gbCadastro.Controls.Add(this.gbCadastrados);
            this.gbCadastro.Controls.Add(this.btnComandoEntrada);
            this.gbCadastro.Controls.Add(this.cmdSair);
            this.gbCadastro.Controls.Add(this.gbInners);
            this.gbCadastro.Location = new System.Drawing.Point(6, 12);
            this.gbCadastro.Name = "gbCadastro";
            this.gbCadastro.Size = new System.Drawing.Size(614, 611);
            this.gbCadastro.TabIndex = 3;
            this.gbCadastro.TabStop = false;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Location = new System.Drawing.Point(122, 217);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(172, 23);
            this.btnAlterar.TabIndex = 35;
            this.btnAlterar.Text = "Alterar item selecionado da Lista";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
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
            // cmdLimparBilhetes
            // 
            this.cmdLimparBilhetes.Location = new System.Drawing.Point(412, 579);
            this.cmdLimparBilhetes.Name = "cmdLimparBilhetes";
            this.cmdLimparBilhetes.Size = new System.Drawing.Size(66, 23);
            this.cmdLimparBilhetes.TabIndex = 4;
            this.cmdLimparBilhetes.Text = "Limpar";
            this.cmdLimparBilhetes.UseVisualStyleBackColor = true;
            this.cmdLimparBilhetes.Click += new System.EventHandler(this.cmdLimparBilhetes_Click);
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
            this.lstInnersCadastrados.SelectedIndexChanged += new System.EventHandler(this.lstInnersCadastrados_SelectedIndexChanged);
            // 
            // btnComandoEntrada
            // 
            this.btnComandoEntrada.Enabled = false;
            this.btnComandoEntrada.Location = new System.Drawing.Point(6, 579);
            this.btnComandoEntrada.Name = "btnComandoEntrada";
            this.btnComandoEntrada.Size = new System.Drawing.Size(100, 23);
            this.btnComandoEntrada.TabIndex = 3;
            this.btnComandoEntrada.Text = "Porta 1 | Entrada";
            this.btnComandoEntrada.UseVisualStyleBackColor = true;
            this.btnComandoEntrada.Click += new System.EventHandler(this.btnComandoEntrada_Click);
            // 
            // cmdSair
            // 
            this.cmdSair.Enabled = false;
            this.cmdSair.Location = new System.Drawing.Point(113, 579);
            this.cmdSair.Name = "cmdSair";
            this.cmdSair.Size = new System.Drawing.Size(100, 23);
            this.cmdSair.TabIndex = 2;
            this.cmdSair.Text = "Porta 2 | Saída";
            this.cmdSair.UseVisualStyleBackColor = true;
            this.cmdSair.Click += new System.EventHandler(this.btnComandoSair_Click);
            // 
            // gbInners
            // 
            this.gbInners.Controls.Add(this.groupBox1);
            this.gbInners.Controls.Add(this.gbParamtros);
            this.gbInners.Controls.Add(this.gbLadoCatraca);
            this.gbInners.Controls.Add(this.cboTipoEquipamento);
            this.gbInners.Controls.Add(this.lblTipoEquipamento);
            this.gbInners.Controls.Add(this.cboPadraoCartao);
            this.gbInners.Controls.Add(this.label9);
            this.gbInners.Controls.Add(this.cboTipoConexaoOnline);
            this.gbInners.Controls.Add(this.label11);
            this.gbInners.Controls.Add(this.udPorta);
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
            this.groupBox1.Location = new System.Drawing.Point(375, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 80);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo Leitor:";
            // 
            // cboTipoLeitor
            // 
            this.cboTipoLeitor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoLeitor.Location = new System.Drawing.Point(6, 16);
            this.cboTipoLeitor.Name = "cboTipoLeitor";
            this.cboTipoLeitor.Size = new System.Drawing.Size(207, 21);
            this.cboTipoLeitor.TabIndex = 22;
            this.cboTipoLeitor.SelectedIndexChanged += new System.EventHandler(this.cboPadraoCartao_SelectedIndexChanged);
            // 
            // ckbDoisLeitores
            // 
            this.ckbDoisLeitores.AutoSize = true;
            this.ckbDoisLeitores.Enabled = false;
            this.ckbDoisLeitores.Location = new System.Drawing.Point(79, 49);
            this.ckbDoisLeitores.Name = "ckbDoisLeitores";
            this.ckbDoisLeitores.Size = new System.Drawing.Size(78, 17);
            this.ckbDoisLeitores.TabIndex = 26;
            this.ckbDoisLeitores.Text = "2 Leitores?";
            this.ckbDoisLeitores.UseVisualStyleBackColor = true;
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
            // tcInnerBIO
            // 
            this.tcInnerBIO.Controls.Add(this.tbpExOnline);
            this.tcInnerBIO.Location = new System.Drawing.Point(0, 0);
            this.tcInnerBIO.Name = "tcInnerBIO";
            this.tcInnerBIO.SelectedIndex = 0;
            this.tcInnerBIO.Size = new System.Drawing.Size(652, 665);
            this.tcInnerBIO.TabIndex = 35;
            // 
            // FrmOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 684);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tcInnerBIO);
            this.Name = "FrmOnline";
            this.Text = "FrmOnline";
            this.Load += new System.EventHandler(this.FrmOnline_Load);
            this.gbParamtros.ResumeLayout(false);
            this.gbParamtros.PerformLayout();
            this.gbLadoCatraca.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPorta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udNumeroInner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udQtdDigitosCartao)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbOnline.ResumeLayout(false);
            this.gbCadastro.ResumeLayout(false);
            this.gbMonitoracao.ResumeLayout(false);
            this.gbMonitoracao.PerformLayout();
            this.gbCadastrados.ResumeLayout(false);
            this.gbInners.ResumeLayout(false);
            this.gbInners.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbpExOnline.ResumeLayout(false);
            this.tcInnerBIO.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbParamtros;
        public System.Windows.Forms.TextBox txtCartaoMaster;
        public System.Windows.Forms.CheckBox chkHabilitaCartaoMaster;
        public System.Windows.Forms.CheckBox chkHabilitaListaOffLine;
        public System.Windows.Forms.CheckBox ckbHabililtaBiometria;
        public System.Windows.Forms.CheckBox chkHabilitaTeclado;
        public System.Windows.Forms.CheckBox chkHabilitaVerificacao;
        public System.Windows.Forms.CheckBox chkHabilitaIdentificacao;
        public System.Windows.Forms.CheckBox chkHabilitaListaSemDigital;
        public System.Windows.Forms.ToolStripStatusLabel lblDados;
        private System.Windows.Forms.GroupBox gbLadoCatraca;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton optDireita;
        public System.Windows.Forms.RadioButton optGiroEsquerda;
        public System.Windows.Forms.ComboBox cboTipoEquipamento;
        public System.Windows.Forms.Label lblTipoEquipamento;
        public System.Windows.Forms.ComboBox cboPadraoCartao;
        public System.Windows.Forms.ComboBox cboTipoConexaoOnline;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.NumericUpDown udPorta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown udNumeroInner;
        public System.Windows.Forms.NumericUpDown udQtdDigitosCartao;
        public System.Windows.Forms.ToolStripStatusLabel lblEmExec1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ListBox lstVersaoInners;
        public System.Windows.Forms.Button btnIniciarMaquina;
        public System.Windows.Forms.Button btnRemoverInnerLista;
        public System.Windows.Forms.ListBox lstBilhetes;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button btnAdicionarCatracaInner;
        private System.Windows.Forms.GroupBox gbOnline;
        private System.Windows.Forms.GroupBox gbCadastro;
        public System.Windows.Forms.Button btnPararMaquina;
        private System.Windows.Forms.Button cmdLimparBilhetes;
        private System.Windows.Forms.GroupBox gbMonitoracao;
        public System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbCadastrados;
        public System.Windows.Forms.ListBox lstInnersCadastrados;
        public System.Windows.Forms.Button btnComandoEntrada;
        public System.Windows.Forms.Button cmdSair;
        private System.Windows.Forms.GroupBox gbInners;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cboTipoLeitor;
        public System.Windows.Forms.CheckBox ckbDoisLeitores;
        private System.Windows.Forms.TabPage tbpExOnline;
        private System.Windows.Forms.TabControl tcInnerBIO;
        public System.Windows.Forms.Button btnAlterar;
    }
}