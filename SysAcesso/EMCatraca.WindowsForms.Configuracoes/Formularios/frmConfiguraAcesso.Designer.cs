namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    partial class FrmConfiguraAcesso
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguraAcesso));
            this.tipConfig = new System.Windows.Forms.ToolTip(this.components);
            this.btnIntervaloRemover = new System.Windows.Forms.Button();
            this.btnCancelarIntervalo = new System.Windows.Forms.Button();
            this.btnIntervaloAdicionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnGravarGeral = new System.Windows.Forms.Button();
            this.PaginaServidor = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTipoIntegracao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPortaServidor = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbtConexaoBD = new System.Windows.Forms.RadioButton();
            this.rdbtConexaoWebApi = new System.Windows.Forms.RadioButton();
            this.PaginaCatracas = new System.Windows.Forms.TabPage();
            this.pnFiltro = new System.Windows.Forms.Panel();
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnCatracaDados = new System.Windows.Forms.Panel();
            this.rdbGiroNormal = new System.Windows.Forms.RadioButton();
            this.btnEditarCatraca = new System.Windows.Forms.Button();
            this.rdbGiroInvertido = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancelarCatraca = new System.Windows.Forms.Button();
            this.rdbGiroSaida = new System.Windows.Forms.RadioButton();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.rdbGiroEntrada = new System.Windows.Forms.RadioButton();
            this.btnIncluirCatraca = new System.Windows.Forms.Button();
            this.lblCatracaCodigo = new System.Windows.Forms.Label();
            this.txtDescricaoCatraca = new System.Windows.Forms.TextBox();
            this.txtIPCatraca = new System.Windows.Forms.TextBox();
            this.txtCodigoCatraca = new System.Windows.Forms.NumericUpDown();
            this.lblCatracaDescricao = new System.Windows.Forms.Label();
            this.lblCatracaIp = new System.Windows.Forms.Label();
            this.lblCatracaPorta = new System.Windows.Forms.Label();
            this.txtPortaCatraca = new System.Windows.Forms.TextBox();
            this.dgvCatracas = new EMCatraca.WindowsForms.Configuracoes.ControlesUsuario.DataGridSelecaoCatraca();
            this.PaginaTeclado = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.chkTecladoIverter = new System.Windows.Forms.CheckBox();
            this.chkTecladoTodos = new System.Windows.Forms.CheckBox();
            this.txtTecladoFiltrar = new System.Windows.Forms.TextBox();
            this.lblTecladoFiltrar = new System.Windows.Forms.Label();
            this.lblTecladoSelecione = new System.Windows.Forms.Label();
            this.cklTecladoPessoas = new System.Windows.Forms.CheckedListBox();
            this.cboTecladoTipoPessoa = new System.Windows.Forms.ComboBox();
            this.PaginaOutros = new System.Windows.Forms.TabPage();
            this.lblNegarAcessoOcorrenciasColaborador = new System.Windows.Forms.Label();
            this.cklNegarOcorrenciasColaborador = new System.Windows.Forms.CheckedListBox();
            this.chkBloquearAcessoColaboradorInativo = new System.Windows.Forms.CheckBox();
            this.lblNegarAcessoOcorrenciasProfessor = new System.Windows.Forms.Label();
            this.cklBloquearAcessoProfessorComOcorrencias = new System.Windows.Forms.CheckedListBox();
            this.chkBloquearAcessoProfessorInativo = new System.Windows.Forms.CheckBox();
            this.chkBloquearAcessoAutorizadoSemMatricula = new System.Windows.Forms.CheckBox();
            this.chkBloquearAcessoResponsavelSemMatricula = new System.Windows.Forms.CheckBox();
            this.PaginaAlunos = new System.Windows.Forms.TabPage();
            this.tbcAlunoAcesso = new System.Windows.Forms.TabControl();
            this.PaginaAlunoAcessoGeral = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpAlunosAcessoSituacao = new System.Windows.Forms.TabPage();
            this.chkBloquearAcessoAlunoSemMatricula = new System.Windows.Forms.CheckBox();
            this.chkNegarAlunoPendenteDocumento = new System.Windows.Forms.CheckBox();
            this.chkBloquearAcessoAlunoComPendenciaMaterial = new System.Windows.Forms.CheckBox();
            this.chkBloquearAcessoAlunoInadimplente = new System.Windows.Forms.CheckBox();
            this.tpAlunosAcessoOcorrencias = new System.Windows.Forms.TabPage();
            this.cklOcorrenciasAluno = new System.Windows.Forms.CheckedListBox();
            this.lblNegarOcorrenciasAluno = new System.Windows.Forms.Label();
            this.tpAlunosAcessoParticularidades = new System.Windows.Forms.TabPage();
            this.lblAtributoBloqueado = new System.Windows.Forms.Label();
            this.lblAtributoPodeSairSozinho = new System.Windows.Forms.Label();
            this.cboAtributoPodeSairSozinho = new System.Windows.Forms.ComboBox();
            this.cboAtributoBloqueado = new System.Windows.Forms.ComboBox();
            this.tpAlunosAcessoTemporizador = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnIntervaloAlterar = new System.Windows.Forms.Button();
            this.btnIntervaloNovo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIntervalo = new System.Windows.Forms.Label();
            this.lbTipoGiro = new System.Windows.Forms.Label();
            this.mtbIntervaloHoraInicial = new System.Windows.Forms.MaskedTextBox();
            this.rdbIntervaloSaida = new System.Windows.Forms.RadioButton();
            this.cboDiaSemana = new System.Windows.Forms.ComboBox();
            this.rdbIntervaloEntrada = new System.Windows.Forms.RadioButton();
            this.mtbIntervaloHoraFinal = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvSelecaoIntervalos = new EMCatraca.WindowsForms.Configuracoes.ControlesUsuario.DataGridSelecaoCatraca();
            this.lblAcessoAluno = new System.Windows.Forms.Label();
            this.chkUmAcessoPorIntervalo = new System.Windows.Forms.CheckBox();
            this.lblAcessoSegundos = new System.Windows.Forms.Label();
            this.nmSegundoMinimoParaNovoAcesso = new System.Windows.Forms.NumericUpDown();
            this.PaginaAlunoAcessoMensagem = new System.Windows.Forms.TabPage();
            this.chkMsgInadimplentes = new System.Windows.Forms.CheckBox();
            this.lblMsgOcorrencias = new System.Windows.Forms.Label();
            this.chkMsgPendenteDocumento = new System.Windows.Forms.CheckBox();
            this.cklMsgOcorrencias = new System.Windows.Forms.CheckedListBox();
            this.chkMsgPendenteMateriais = new System.Windows.Forms.CheckBox();
            this.PaginaAcessoAlunoLiberacao = new System.Windows.Forms.TabPage();
            this.chkFormLiberacao = new System.Windows.Forms.CheckBox();
            this.lblTempoLiberado = new System.Windows.Forms.Label();
            this.cklColaboradores = new System.Windows.Forms.CheckedListBox();
            this.lblTempoLiberadoSegundos = new System.Windows.Forms.Label();
            this.cklProfessores = new System.Windows.Forms.CheckedListBox();
            this.chkResponsavelLibera = new System.Windows.Forms.CheckBox();
            this.lblSelecionarColaboradores = new System.Windows.Forms.Label();
            this.numTempoPassagemAutorizado = new System.Windows.Forms.NumericUpDown();
            this.lblSelecionarProfessores = new System.Windows.Forms.Label();
            this.chkAutorizadoLibera = new System.Windows.Forms.CheckBox();
            this.tbcConteudo = new System.Windows.Forms.TabControl();
            this.PaginaCustomizacoes = new System.Windows.Forms.TabPage();
            this.tbcAnaliseCFG = new System.Windows.Forms.TabControl();
            this.PaginaTipoPessoa = new System.Windows.Forms.TabPage();
            this.gbIndentificadorTipoPessoa = new System.Windows.Forms.GroupBox();
            this.lbTipoPessoaAutorizadoBuscaAluno = new System.Windows.Forms.Label();
            this.lbTipoPessoaAluno = new System.Windows.Forms.Label();
            this.txtTipoPessoaAutorizadoBuscarAluno = new System.Windows.Forms.TextBox();
            this.txtTipoPessoaAluno = new System.Windows.Forms.TextBox();
            this.lbTipoPessoaProfessor = new System.Windows.Forms.Label();
            this.txtTipoPessoaResponsavel = new System.Windows.Forms.TextBox();
            this.txtTipoPessoaProfessor = new System.Windows.Forms.TextBox();
            this.lbTipoPessoaResponsavel = new System.Windows.Forms.Label();
            this.lbTipoPessoaProfissional = new System.Windows.Forms.Label();
            this.txtTipoPessoaProfissional = new System.Windows.Forms.TextBox();
            this.ckbExisteCustomizacaoTipoPessoa = new System.Windows.Forms.CheckBox();
            this.fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtAtalhos = new System.Windows.Forms.TextBox();
            this.pnlConteudo.SuspendLayout();
            this.PaginaServidor.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.PaginaCatracas.SuspendLayout();
            this.pnFiltro.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            this.pnCatracaDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCatraca)).BeginInit();
            this.PaginaTeclado.SuspendLayout();
            this.PaginaOutros.SuspendLayout();
            this.PaginaAlunos.SuspendLayout();
            this.tbcAlunoAcesso.SuspendLayout();
            this.PaginaAlunoAcessoGeral.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpAlunosAcessoSituacao.SuspendLayout();
            this.tpAlunosAcessoOcorrencias.SuspendLayout();
            this.tpAlunosAcessoParticularidades.SuspendLayout();
            this.tpAlunosAcessoTemporizador.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSegundoMinimoParaNovoAcesso)).BeginInit();
            this.PaginaAlunoAcessoMensagem.SuspendLayout();
            this.PaginaAcessoAlunoLiberacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempoPassagemAutorizado)).BeginInit();
            this.tbcConteudo.SuspendLayout();
            this.PaginaCustomizacoes.SuspendLayout();
            this.tbcAnaliseCFG.SuspendLayout();
            this.PaginaTipoPessoa.SuspendLayout();
            this.gbIndentificadorTipoPessoa.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.Controls.Add(this.txtAtalhos);
            this.pnlConteudo.Controls.Add(this.btnGravarGeral);
            this.pnlConteudo.Controls.Add(this.tbcConteudo);
            this.pnlConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlConteudo.Size = new System.Drawing.Size(647, 702);
            this.pnlConteudo.TabIndex = 0;
            this.tipConfig.SetToolTip(this.pnlConteudo, "Clica segura e arraste formulário na tela.");
            this.pnlConteudo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlConteudo_MouseDoubleClick);
            this.pnlConteudo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlConteudo_MouseMove);
            // 
            // btnIntervaloRemover
            // 
            this.btnIntervaloRemover.BackColor = System.Drawing.Color.White;
            this.btnIntervaloRemover.Enabled = false;
            this.btnIntervaloRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntervaloRemover.Location = new System.Drawing.Point(23, 202);
            this.btnIntervaloRemover.Name = "btnIntervaloRemover";
            this.btnIntervaloRemover.Size = new System.Drawing.Size(94, 49);
            this.btnIntervaloRemover.TabIndex = 67;
            this.btnIntervaloRemover.Text = "Remover Intervalo";
            this.tipConfig.SetToolTip(this.btnIntervaloRemover, "Remover intervalo selecionado.");
            this.btnIntervaloRemover.UseVisualStyleBackColor = false;
            this.btnIntervaloRemover.Click += new System.EventHandler(this.BtnIntervaloRemover_Click);
            // 
            // btnCancelarIntervalo
            // 
            this.btnCancelarIntervalo.BackColor = System.Drawing.Color.White;
            this.btnCancelarIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarIntervalo.Location = new System.Drawing.Point(23, 262);
            this.btnCancelarIntervalo.Name = "btnCancelarIntervalo";
            this.btnCancelarIntervalo.Size = new System.Drawing.Size(94, 49);
            this.btnCancelarIntervalo.TabIndex = 71;
            this.btnCancelarIntervalo.Text = "Cancelar";
            this.tipConfig.SetToolTip(this.btnCancelarIntervalo, "Remover intervalo selecionado.");
            this.btnCancelarIntervalo.UseVisualStyleBackColor = false;
            this.btnCancelarIntervalo.Click += new System.EventHandler(this.btnCancelarIntervalo_Click);
            // 
            // btnIntervaloAdicionar
            // 
            this.btnIntervaloAdicionar.BackColor = System.Drawing.Color.White;
            this.btnIntervaloAdicionar.Enabled = false;
            this.btnIntervaloAdicionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntervaloAdicionar.Location = new System.Drawing.Point(23, 83);
            this.btnIntervaloAdicionar.Name = "btnIntervaloAdicionar";
            this.btnIntervaloAdicionar.Size = new System.Drawing.Size(94, 49);
            this.btnIntervaloAdicionar.TabIndex = 66;
            this.btnIntervaloAdicionar.Text = "Adicionar Intervalo";
            this.btnIntervaloAdicionar.UseVisualStyleBackColor = false;
            this.btnIntervaloAdicionar.Click += new System.EventHandler(this.BtnIntervaloAdicionar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.Image")));
            this.btnFechar.Location = new System.Drawing.Point(602, 8);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(35, 34);
            this.btnFechar.TabIndex = 10;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnGravarGeral
            // 
            this.btnGravarGeral.BackColor = System.Drawing.Color.White;
            this.btnGravarGeral.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGravarGeral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnGravarGeral.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnGravarGeral.ForeColor = System.Drawing.Color.Black;
            this.btnGravarGeral.Location = new System.Drawing.Point(550, 25);
            this.btnGravarGeral.Name = "btnGravarGeral";
            this.btnGravarGeral.Size = new System.Drawing.Size(87, 37);
            this.btnGravarGeral.TabIndex = 52;
            this.btnGravarGeral.Text = "Gravar";
            this.btnGravarGeral.UseVisualStyleBackColor = false;
            this.btnGravarGeral.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // PaginaServidor
            // 
            this.PaginaServidor.BackColor = System.Drawing.SystemColors.Control;
            this.PaginaServidor.Controls.Add(this.groupBox4);
            this.PaginaServidor.Controls.Add(this.groupBox3);
            this.PaginaServidor.Controls.Add(this.groupBox2);
            this.PaginaServidor.Controls.Add(this.groupBox1);
            this.PaginaServidor.Location = new System.Drawing.Point(4, 25);
            this.PaginaServidor.Name = "PaginaServidor";
            this.PaginaServidor.Size = new System.Drawing.Size(536, 667);
            this.PaginaServidor.TabIndex = 2;
            this.PaginaServidor.Text = "Servidor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTipoIntegracao);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(11, 286);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(511, 108);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Integração";
            // 
            // txtTipoIntegracao
            // 
            this.txtTipoIntegracao.Location = new System.Drawing.Point(9, 44);
            this.txtTipoIntegracao.Multiline = true;
            this.txtTipoIntegracao.Name = "txtTipoIntegracao";
            this.txtTipoIntegracao.Size = new System.Drawing.Size(496, 53);
            this.txtTipoIntegracao.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tipo de Integração:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtServidor);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(11, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(511, 108);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dados do Servidor";
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(9, 42);
            this.txtServidor.Multiline = true;
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(496, 53);
            this.txtServidor.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Caminho do Banco de Dados";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblServerIP);
            this.groupBox2.Controls.Add(this.txtServerIP);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPortaServidor);
            this.groupBox2.Location = new System.Drawing.Point(11, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 91);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados da Conexão";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(6, 25);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(147, 16);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = "IP do Banco de Dados:";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(153, 22);
            this.txtServerIP.MaxLength = 15;
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(352, 22);
            this.txtServerIP.TabIndex = 1;
            this.txtServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtServerIP_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Porta TCP-IP:";
            // 
            // txtPortaServidor
            // 
            this.txtPortaServidor.Location = new System.Drawing.Point(153, 58);
            this.txtPortaServidor.Margin = new System.Windows.Forms.Padding(2);
            this.txtPortaServidor.Name = "txtPortaServidor";
            this.txtPortaServidor.Size = new System.Drawing.Size(352, 22);
            this.txtPortaServidor.TabIndex = 6;
            this.txtPortaServidor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPorta_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbtConexaoBD);
            this.groupBox1.Controls.Add(this.rdbtConexaoWebApi);
            this.groupBox1.Location = new System.Drawing.Point(11, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 54);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Conexão";
            // 
            // rdbtConexaoBD
            // 
            this.rdbtConexaoBD.AutoSize = true;
            this.rdbtConexaoBD.Checked = true;
            this.rdbtConexaoBD.Location = new System.Drawing.Point(203, 23);
            this.rdbtConexaoBD.Name = "rdbtConexaoBD";
            this.rdbtConexaoBD.Size = new System.Drawing.Size(128, 20);
            this.rdbtConexaoBD.TabIndex = 1;
            this.rdbtConexaoBD.TabStop = true;
            this.rdbtConexaoBD.Text = "Conexao Padrão";
            this.rdbtConexaoBD.UseVisualStyleBackColor = true;
            // 
            // rdbtConexaoWebApi
            // 
            this.rdbtConexaoWebApi.AutoSize = true;
            this.rdbtConexaoWebApi.Location = new System.Drawing.Point(9, 23);
            this.rdbtConexaoWebApi.Name = "rdbtConexaoWebApi";
            this.rdbtConexaoWebApi.Size = new System.Drawing.Size(132, 20);
            this.rdbtConexaoWebApi.TabIndex = 0;
            this.rdbtConexaoWebApi.TabStop = true;
            this.rdbtConexaoWebApi.Text = "Conexao WebApi";
            this.rdbtConexaoWebApi.UseVisualStyleBackColor = true;
            // 
            // PaginaCatracas
            // 
            this.PaginaCatracas.Controls.Add(this.pnFiltro);
            this.PaginaCatracas.Location = new System.Drawing.Point(4, 25);
            this.PaginaCatracas.Name = "PaginaCatracas";
            this.PaginaCatracas.Size = new System.Drawing.Size(536, 667);
            this.PaginaCatracas.TabIndex = 5;
            this.PaginaCatracas.Text = "Dispositivos";
            this.PaginaCatracas.UseVisualStyleBackColor = true;
            // 
            // pnFiltro
            // 
            this.pnFiltro.Controls.Add(this.flpFiltros);
            this.pnFiltro.Location = new System.Drawing.Point(0, 2);
            this.pnFiltro.Margin = new System.Windows.Forms.Padding(2);
            this.pnFiltro.Name = "pnFiltro";
            this.pnFiltro.Size = new System.Drawing.Size(538, 583);
            this.pnFiltro.TabIndex = 0;
            // 
            // flpFiltros
            // 
            this.flpFiltros.Controls.Add(this.pnCatracaDados);
            this.flpFiltros.Controls.Add(this.dgvCatracas);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFiltros.Location = new System.Drawing.Point(0, 0);
            this.flpFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.Size = new System.Drawing.Size(538, 583);
            this.flpFiltros.TabIndex = 59;
            // 
            // pnCatracaDados
            // 
            this.pnCatracaDados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnCatracaDados.BackColor = System.Drawing.SystemColors.Control;
            this.pnCatracaDados.Controls.Add(this.rdbGiroNormal);
            this.pnCatracaDados.Controls.Add(this.btnEditarCatraca);
            this.pnCatracaDados.Controls.Add(this.rdbGiroInvertido);
            this.pnCatracaDados.Controls.Add(this.label5);
            this.pnCatracaDados.Controls.Add(this.btnCancelarCatraca);
            this.pnCatracaDados.Controls.Add(this.rdbGiroSaida);
            this.pnCatracaDados.Controls.Add(this.btnExcluir);
            this.pnCatracaDados.Controls.Add(this.rdbGiroEntrada);
            this.pnCatracaDados.Controls.Add(this.btnIncluirCatraca);
            this.pnCatracaDados.Controls.Add(this.lblCatracaCodigo);
            this.pnCatracaDados.Controls.Add(this.txtDescricaoCatraca);
            this.pnCatracaDados.Controls.Add(this.txtIPCatraca);
            this.pnCatracaDados.Controls.Add(this.txtCodigoCatraca);
            this.pnCatracaDados.Controls.Add(this.lblCatracaDescricao);
            this.pnCatracaDados.Controls.Add(this.lblCatracaIp);
            this.pnCatracaDados.Controls.Add(this.lblCatracaPorta);
            this.pnCatracaDados.Controls.Add(this.txtPortaCatraca);
            this.pnCatracaDados.Location = new System.Drawing.Point(0, 0);
            this.pnCatracaDados.Margin = new System.Windows.Forms.Padding(0);
            this.pnCatracaDados.Name = "pnCatracaDados";
            this.pnCatracaDados.Size = new System.Drawing.Size(534, 189);
            this.pnCatracaDados.TabIndex = 14;
            // 
            // rdbGiroNormal
            // 
            this.rdbGiroNormal.AutoSize = true;
            this.rdbGiroNormal.Checked = true;
            this.rdbGiroNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGiroNormal.Location = new System.Drawing.Point(170, 97);
            this.rdbGiroNormal.Name = "rdbGiroNormal";
            this.rdbGiroNormal.Size = new System.Drawing.Size(69, 20);
            this.rdbGiroNormal.TabIndex = 72;
            this.rdbGiroNormal.TabStop = true;
            this.rdbGiroNormal.Text = "Ambos";
            this.rdbGiroNormal.UseVisualStyleBackColor = true;
            this.rdbGiroNormal.CheckedChanged += new System.EventHandler(this.rdbGiroNormal_CheckedChanged);
            // 
            // btnEditarCatraca
            // 
            this.btnEditarCatraca.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditarCatraca.BackColor = System.Drawing.Color.White;
            this.btnEditarCatraca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEditarCatraca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnEditarCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnEditarCatraca.Location = new System.Drawing.Point(134, 134);
            this.btnEditarCatraca.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarCatraca.Name = "btnEditarCatraca";
            this.btnEditarCatraca.Size = new System.Drawing.Size(124, 42);
            this.btnEditarCatraca.TabIndex = 22;
            this.btnEditarCatraca.Text = "Editar Dispositivo";
            this.btnEditarCatraca.UseVisualStyleBackColor = false;
            this.btnEditarCatraca.Click += new System.EventHandler(this.btnEditarCatraca_Click);
            // 
            // rdbGiroInvertido
            // 
            this.rdbGiroInvertido.AutoSize = true;
            this.rdbGiroInvertido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGiroInvertido.Location = new System.Drawing.Point(413, 97);
            this.rdbGiroInvertido.Name = "rdbGiroInvertido";
            this.rdbGiroInvertido.Size = new System.Drawing.Size(105, 20);
            this.rdbGiroInvertido.TabIndex = 71;
            this.rdbGiroInvertido.Text = "Giro Invertido";
            this.rdbGiroInvertido.UseVisualStyleBackColor = true;
            this.rdbGiroInvertido.CheckedChanged += new System.EventHandler(this.rdbInverterGiro_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 70;
            this.label5.Text = "Configurar tipo giro:";
            // 
            // btnCancelarCatraca
            // 
            this.btnCancelarCatraca.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelarCatraca.BackColor = System.Drawing.Color.White;
            this.btnCancelarCatraca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelarCatraca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCancelarCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnCancelarCatraca.Location = new System.Drawing.Point(394, 134);
            this.btnCancelarCatraca.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelarCatraca.Name = "btnCancelarCatraca";
            this.btnCancelarCatraca.Size = new System.Drawing.Size(124, 42);
            this.btnCancelarCatraca.TabIndex = 21;
            this.btnCancelarCatraca.Text = "Cancelar";
            this.btnCancelarCatraca.UseVisualStyleBackColor = false;
            this.btnCancelarCatraca.Click += new System.EventHandler(this.btnCancelarCatraca_Click);
            // 
            // rdbGiroSaida
            // 
            this.rdbGiroSaida.AutoSize = true;
            this.rdbGiroSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGiroSaida.Location = new System.Drawing.Point(340, 97);
            this.rdbGiroSaida.Name = "rdbGiroSaida";
            this.rdbGiroSaida.Size = new System.Drawing.Size(62, 20);
            this.rdbGiroSaida.TabIndex = 69;
            this.rdbGiroSaida.Text = "Saída";
            this.rdbGiroSaida.UseVisualStyleBackColor = true;
            this.rdbGiroSaida.CheckedChanged += new System.EventHandler(this.rdbGiroSaida_CheckedChanged);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.ForeColor = System.Drawing.Color.Black;
            this.btnExcluir.Location = new System.Drawing.Point(264, 134);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(124, 42);
            this.btnExcluir.TabIndex = 20;
            this.btnExcluir.Text = "Remover";
            this.btnExcluir.UseVisualStyleBackColor = false;
            // 
            // rdbGiroEntrada
            // 
            this.rdbGiroEntrada.AutoSize = true;
            this.rdbGiroEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGiroEntrada.Location = new System.Drawing.Point(252, 97);
            this.rdbGiroEntrada.Name = "rdbGiroEntrada";
            this.rdbGiroEntrada.Size = new System.Drawing.Size(73, 20);
            this.rdbGiroEntrada.TabIndex = 68;
            this.rdbGiroEntrada.Text = "Entrada";
            this.rdbGiroEntrada.UseVisualStyleBackColor = true;
            this.rdbGiroEntrada.CheckedChanged += new System.EventHandler(this.rdbGiroEntrada_CheckedChanged);
            // 
            // btnIncluirCatraca
            // 
            this.btnIncluirCatraca.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnIncluirCatraca.BackColor = System.Drawing.Color.White;
            this.btnIncluirCatraca.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnIncluirCatraca.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnIncluirCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncluirCatraca.ForeColor = System.Drawing.Color.Black;
            this.btnIncluirCatraca.Location = new System.Drawing.Point(4, 134);
            this.btnIncluirCatraca.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.btnIncluirCatraca.Name = "btnIncluirCatraca";
            this.btnIncluirCatraca.Size = new System.Drawing.Size(124, 42);
            this.btnIncluirCatraca.TabIndex = 19;
            this.btnIncluirCatraca.Text = "Novo Dispositivo";
            this.btnIncluirCatraca.UseVisualStyleBackColor = false;
            this.btnIncluirCatraca.Click += new System.EventHandler(this.btnIncluirCatraca_Click);
            // 
            // lblCatracaCodigo
            // 
            this.lblCatracaCodigo.AutoSize = true;
            this.lblCatracaCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatracaCodigo.Location = new System.Drawing.Point(4, 18);
            this.lblCatracaCodigo.Name = "lblCatracaCodigo";
            this.lblCatracaCodigo.Size = new System.Drawing.Size(146, 16);
            this.lblCatracaCodigo.TabIndex = 0;
            this.lblCatracaCodigo.Text = "Numero do dispositivo:";
            // 
            // txtDescricaoCatraca
            // 
            this.txtDescricaoCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricaoCatraca.Location = new System.Drawing.Point(170, 57);
            this.txtDescricaoCatraca.MaxLength = 20;
            this.txtDescricaoCatraca.Name = "txtDescricaoCatraca";
            this.txtDescricaoCatraca.Size = new System.Drawing.Size(348, 22);
            this.txtDescricaoCatraca.TabIndex = 2;
            this.txtDescricaoCatraca.Leave += new System.EventHandler(this.txtDescricaoCatraca_Leave);
            // 
            // txtIPCatraca
            // 
            this.txtIPCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIPCatraca.Location = new System.Drawing.Point(278, 16);
            this.txtIPCatraca.MaxLength = 15;
            this.txtIPCatraca.Name = "txtIPCatraca";
            this.txtIPCatraca.Size = new System.Drawing.Size(115, 22);
            this.txtIPCatraca.TabIndex = 3;
            this.txtIPCatraca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodigoCatraca
            // 
            this.txtCodigoCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoCatraca.Location = new System.Drawing.Point(170, 16);
            this.txtCodigoCatraca.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.txtCodigoCatraca.Name = "txtCodigoCatraca";
            this.txtCodigoCatraca.Size = new System.Drawing.Size(60, 22);
            this.txtCodigoCatraca.TabIndex = 1;
            this.txtCodigoCatraca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoCatraca.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCatracaDescricao
            // 
            this.lblCatracaDescricao.AutoSize = true;
            this.lblCatracaDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatracaDescricao.Location = new System.Drawing.Point(4, 60);
            this.lblCatracaDescricao.Name = "lblCatracaDescricao";
            this.lblCatracaDescricao.Size = new System.Drawing.Size(160, 16);
            this.lblCatracaDescricao.TabIndex = 2;
            this.lblCatracaDescricao.Text = "Descrição do dispositivo:";
            // 
            // lblCatracaIp
            // 
            this.lblCatracaIp.AutoSize = true;
            this.lblCatracaIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatracaIp.Location = new System.Drawing.Point(249, 18);
            this.lblCatracaIp.Name = "lblCatracaIp";
            this.lblCatracaIp.Size = new System.Drawing.Size(23, 16);
            this.lblCatracaIp.TabIndex = 4;
            this.lblCatracaIp.Text = "IP:";
            // 
            // lblCatracaPorta
            // 
            this.lblCatracaPorta.AutoSize = true;
            this.lblCatracaPorta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatracaPorta.Location = new System.Drawing.Point(410, 18);
            this.lblCatracaPorta.Name = "lblCatracaPorta";
            this.lblCatracaPorta.Size = new System.Drawing.Size(43, 16);
            this.lblCatracaPorta.TabIndex = 6;
            this.lblCatracaPorta.Text = "Porta:";
            // 
            // txtPortaCatraca
            // 
            this.txtPortaCatraca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPortaCatraca.Location = new System.Drawing.Point(458, 16);
            this.txtPortaCatraca.MaxLength = 5;
            this.txtPortaCatraca.Name = "txtPortaCatraca";
            this.txtPortaCatraca.Size = new System.Drawing.Size(60, 22);
            this.txtPortaCatraca.TabIndex = 4;
            this.txtPortaCatraca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvCatracas
            // 
            this.dgvCatracas.Location = new System.Drawing.Point(4, 194);
            this.dgvCatracas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvCatracas.MultiSelect = true;
            this.dgvCatracas.Name = "dgvCatracas";
            this.dgvCatracas.ReadOnly = true;
            this.dgvCatracas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatracas.Size = new System.Drawing.Size(530, 389);
            this.dgvCatracas.TabIndex = 17;
            // 
            // PaginaTeclado
            // 
            this.PaginaTeclado.Controls.Add(this.button1);
            this.PaginaTeclado.Controls.Add(this.chkTecladoIverter);
            this.PaginaTeclado.Controls.Add(this.chkTecladoTodos);
            this.PaginaTeclado.Controls.Add(this.txtTecladoFiltrar);
            this.PaginaTeclado.Controls.Add(this.lblTecladoFiltrar);
            this.PaginaTeclado.Controls.Add(this.lblTecladoSelecione);
            this.PaginaTeclado.Controls.Add(this.cklTecladoPessoas);
            this.PaginaTeclado.Controls.Add(this.cboTecladoTipoPessoa);
            this.PaginaTeclado.Location = new System.Drawing.Point(4, 25);
            this.PaginaTeclado.Name = "PaginaTeclado";
            this.PaginaTeclado.Size = new System.Drawing.Size(536, 667);
            this.PaginaTeclado.TabIndex = 4;
            this.PaginaTeclado.Text = "Uso do Teclado";
            this.PaginaTeclado.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(-24576, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 22);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // chkTecladoIverter
            // 
            this.chkTecladoIverter.AutoSize = true;
            this.chkTecladoIverter.Location = new System.Drawing.Point(4, 134);
            this.chkTecladoIverter.Name = "chkTecladoIverter";
            this.chkTecladoIverter.Size = new System.Drawing.Size(123, 20);
            this.chkTecladoIverter.TabIndex = 7;
            this.chkTecladoIverter.Text = "Inverter seleção";
            this.chkTecladoIverter.UseVisualStyleBackColor = true;
            this.chkTecladoIverter.Click += new System.EventHandler(this.ChkTecladoIverter_Click);
            // 
            // chkTecladoTodos
            // 
            this.chkTecladoTodos.AutoSize = true;
            this.chkTecladoTodos.Location = new System.Drawing.Point(4, 106);
            this.chkTecladoTodos.Name = "chkTecladoTodos";
            this.chkTecladoTodos.Size = new System.Drawing.Size(252, 20);
            this.chkTecladoTodos.TabIndex = 6;
            this.chkTecladoTodos.Text = "Selecionar todos os registros filtrados";
            this.chkTecladoTodos.UseVisualStyleBackColor = true;
            this.chkTecladoTodos.Click += new System.EventHandler(this.ChkTecladoTodos_Click);
            // 
            // txtTecladoFiltrar
            // 
            this.txtTecladoFiltrar.Location = new System.Drawing.Point(4, 79);
            this.txtTecladoFiltrar.Name = "txtTecladoFiltrar";
            this.txtTecladoFiltrar.Size = new System.Drawing.Size(497, 22);
            this.txtTecladoFiltrar.TabIndex = 4;
            this.txtTecladoFiltrar.TextChanged += new System.EventHandler(this.TxtTecladoFiltrar_TextChanged);
            // 
            // lblTecladoFiltrar
            // 
            this.lblTecladoFiltrar.AutoSize = true;
            this.lblTecladoFiltrar.Location = new System.Drawing.Point(4, 56);
            this.lblTecladoFiltrar.Name = "lblTecladoFiltrar";
            this.lblTecladoFiltrar.Size = new System.Drawing.Size(101, 16);
            this.lblTecladoFiltrar.TabIndex = 3;
            this.lblTecladoFiltrar.Text = "Filtrar por nome";
            // 
            // lblTecladoSelecione
            // 
            this.lblTecladoSelecione.AutoSize = true;
            this.lblTecladoSelecione.Location = new System.Drawing.Point(4, 4);
            this.lblTecladoSelecione.Name = "lblTecladoSelecione";
            this.lblTecladoSelecione.Size = new System.Drawing.Size(315, 16);
            this.lblTecladoSelecione.TabIndex = 2;
            this.lblTecladoSelecione.Text = "Selecione as pessoas autorizadas a usar o teclado";
            // 
            // cklTecladoPessoas
            // 
            this.cklTecladoPessoas.CheckOnClick = true;
            this.cklTecladoPessoas.FormattingEnabled = true;
            this.cklTecladoPessoas.Location = new System.Drawing.Point(3, 161);
            this.cklTecladoPessoas.Name = "cklTecladoPessoas";
            this.cklTecladoPessoas.Size = new System.Drawing.Size(498, 463);
            this.cklTecladoPessoas.TabIndex = 1;
            this.cklTecladoPessoas.SelectedIndexChanged += new System.EventHandler(this.CklTecladoPessoas_SelectedIndexChanged);
            // 
            // cboTecladoTipoPessoa
            // 
            this.cboTecladoTipoPessoa.FormattingEnabled = true;
            this.cboTecladoTipoPessoa.Location = new System.Drawing.Point(4, 27);
            this.cboTecladoTipoPessoa.Name = "cboTecladoTipoPessoa";
            this.cboTecladoTipoPessoa.Size = new System.Drawing.Size(497, 24);
            this.cboTecladoTipoPessoa.TabIndex = 0;
            this.cboTecladoTipoPessoa.SelectedIndexChanged += new System.EventHandler(this.CboTecladoTipoPessoa_SelectedIndexChanged);
            // 
            // PaginaOutros
            // 
            this.PaginaOutros.Controls.Add(this.lblNegarAcessoOcorrenciasColaborador);
            this.PaginaOutros.Controls.Add(this.cklNegarOcorrenciasColaborador);
            this.PaginaOutros.Controls.Add(this.chkBloquearAcessoColaboradorInativo);
            this.PaginaOutros.Controls.Add(this.lblNegarAcessoOcorrenciasProfessor);
            this.PaginaOutros.Controls.Add(this.cklBloquearAcessoProfessorComOcorrencias);
            this.PaginaOutros.Controls.Add(this.chkBloquearAcessoProfessorInativo);
            this.PaginaOutros.Controls.Add(this.chkBloquearAcessoAutorizadoSemMatricula);
            this.PaginaOutros.Controls.Add(this.chkBloquearAcessoResponsavelSemMatricula);
            this.PaginaOutros.Location = new System.Drawing.Point(4, 25);
            this.PaginaOutros.Name = "PaginaOutros";
            this.PaginaOutros.Padding = new System.Windows.Forms.Padding(3);
            this.PaginaOutros.Size = new System.Drawing.Size(536, 667);
            this.PaginaOutros.TabIndex = 1;
            this.PaginaOutros.Text = "Outros";
            this.PaginaOutros.UseVisualStyleBackColor = true;
            // 
            // lblNegarAcessoOcorrenciasColaborador
            // 
            this.lblNegarAcessoOcorrenciasColaborador.AutoSize = true;
            this.lblNegarAcessoOcorrenciasColaborador.Location = new System.Drawing.Point(4, 398);
            this.lblNegarAcessoOcorrenciasColaborador.Name = "lblNegarAcessoOcorrenciasColaborador";
            this.lblNegarAcessoOcorrenciasColaborador.Size = new System.Drawing.Size(329, 16);
            this.lblNegarAcessoOcorrenciasColaborador.TabIndex = 6;
            this.lblNegarAcessoOcorrenciasColaborador.Text = "Negar acesso para Profissionais com as ocorrências:";
            // 
            // cklNegarOcorrenciasColaborador
            // 
            this.cklNegarOcorrenciasColaborador.CheckOnClick = true;
            this.cklNegarOcorrenciasColaborador.FormattingEnabled = true;
            this.cklNegarOcorrenciasColaborador.Location = new System.Drawing.Point(5, 421);
            this.cklNegarOcorrenciasColaborador.Name = "cklNegarOcorrenciasColaborador";
            this.cklNegarOcorrenciasColaborador.Size = new System.Drawing.Size(499, 208);
            this.cklNegarOcorrenciasColaborador.TabIndex = 7;
            // 
            // chkBloquearAcessoColaboradorInativo
            // 
            this.chkBloquearAcessoColaboradorInativo.AutoSize = true;
            this.chkBloquearAcessoColaboradorInativo.Location = new System.Drawing.Point(4, 93);
            this.chkBloquearAcessoColaboradorInativo.Name = "chkBloquearAcessoColaboradorInativo";
            this.chkBloquearAcessoColaboradorInativo.Size = new System.Drawing.Size(272, 20);
            this.chkBloquearAcessoColaboradorInativo.TabIndex = 3;
            this.chkBloquearAcessoColaboradorInativo.Text = "Negar acesso para profissionais inativos";
            this.chkBloquearAcessoColaboradorInativo.UseVisualStyleBackColor = true;
            // 
            // lblNegarAcessoOcorrenciasProfessor
            // 
            this.lblNegarAcessoOcorrenciasProfessor.AutoSize = true;
            this.lblNegarAcessoOcorrenciasProfessor.Location = new System.Drawing.Point(4, 137);
            this.lblNegarAcessoOcorrenciasProfessor.Name = "lblNegarAcessoOcorrenciasProfessor";
            this.lblNegarAcessoOcorrenciasProfessor.Size = new System.Drawing.Size(325, 16);
            this.lblNegarAcessoOcorrenciasProfessor.TabIndex = 4;
            this.lblNegarAcessoOcorrenciasProfessor.Text = "Negar acesso para Professores com as ocorrências:";
            // 
            // cklBloquearAcessoProfessorComOcorrencias
            // 
            this.cklBloquearAcessoProfessorComOcorrencias.CheckOnClick = true;
            this.cklBloquearAcessoProfessorComOcorrencias.FormattingEnabled = true;
            this.cklBloquearAcessoProfessorComOcorrencias.Location = new System.Drawing.Point(4, 160);
            this.cklBloquearAcessoProfessorComOcorrencias.Name = "cklBloquearAcessoProfessorComOcorrencias";
            this.cklBloquearAcessoProfessorComOcorrencias.Size = new System.Drawing.Size(499, 208);
            this.cklBloquearAcessoProfessorComOcorrencias.TabIndex = 5;
            // 
            // chkBloquearAcessoProfessorInativo
            // 
            this.chkBloquearAcessoProfessorInativo.AutoSize = true;
            this.chkBloquearAcessoProfessorInativo.Location = new System.Drawing.Point(4, 65);
            this.chkBloquearAcessoProfessorInativo.Name = "chkBloquearAcessoProfessorInativo";
            this.chkBloquearAcessoProfessorInativo.Size = new System.Drawing.Size(268, 20);
            this.chkBloquearAcessoProfessorInativo.TabIndex = 2;
            this.chkBloquearAcessoProfessorInativo.Text = "Negar acesso para professores inativos";
            this.chkBloquearAcessoProfessorInativo.UseVisualStyleBackColor = true;
            // 
            // chkBloquearAcessoAutorizadoSemMatricula
            // 
            this.chkBloquearAcessoAutorizadoSemMatricula.AutoSize = true;
            this.chkBloquearAcessoAutorizadoSemMatricula.Location = new System.Drawing.Point(4, 35);
            this.chkBloquearAcessoAutorizadoSemMatricula.Name = "chkBloquearAcessoAutorizadoSemMatricula";
            this.chkBloquearAcessoAutorizadoSemMatricula.Size = new System.Drawing.Size(373, 20);
            this.chkBloquearAcessoAutorizadoSemMatricula.TabIndex = 1;
            this.chkBloquearAcessoAutorizadoSemMatricula.Text = "Negar acesso de autorizados sem aluno(s) matriculado(s)";
            this.chkBloquearAcessoAutorizadoSemMatricula.UseVisualStyleBackColor = true;
            // 
            // chkBloquearAcessoResponsavelSemMatricula
            // 
            this.chkBloquearAcessoResponsavelSemMatricula.AutoSize = true;
            this.chkBloquearAcessoResponsavelSemMatricula.Location = new System.Drawing.Point(4, 5);
            this.chkBloquearAcessoResponsavelSemMatricula.Name = "chkBloquearAcessoResponsavelSemMatricula";
            this.chkBloquearAcessoResponsavelSemMatricula.Size = new System.Drawing.Size(385, 20);
            this.chkBloquearAcessoResponsavelSemMatricula.TabIndex = 0;
            this.chkBloquearAcessoResponsavelSemMatricula.Text = "Negar acesso de responsáveis sem aluno(s) matriculado(s)";
            this.chkBloquearAcessoResponsavelSemMatricula.UseVisualStyleBackColor = true;
            // 
            // PaginaAlunos
            // 
            this.PaginaAlunos.Controls.Add(this.tbcAlunoAcesso);
            this.PaginaAlunos.Location = new System.Drawing.Point(4, 25);
            this.PaginaAlunos.Name = "PaginaAlunos";
            this.PaginaAlunos.Padding = new System.Windows.Forms.Padding(3);
            this.PaginaAlunos.Size = new System.Drawing.Size(536, 667);
            this.PaginaAlunos.TabIndex = 0;
            this.PaginaAlunos.Text = "Alunos";
            this.PaginaAlunos.UseVisualStyleBackColor = true;
            // 
            // tbcAlunoAcesso
            // 
            this.tbcAlunoAcesso.Controls.Add(this.PaginaAlunoAcessoGeral);
            this.tbcAlunoAcesso.Controls.Add(this.PaginaAlunoAcessoMensagem);
            this.tbcAlunoAcesso.Controls.Add(this.PaginaAcessoAlunoLiberacao);
            this.tbcAlunoAcesso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcAlunoAcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcAlunoAcesso.Location = new System.Drawing.Point(3, 3);
            this.tbcAlunoAcesso.Name = "tbcAlunoAcesso";
            this.tbcAlunoAcesso.SelectedIndex = 0;
            this.tbcAlunoAcesso.Size = new System.Drawing.Size(530, 661);
            this.tbcAlunoAcesso.TabIndex = 0;
            // 
            // PaginaAlunoAcessoGeral
            // 
            this.PaginaAlunoAcessoGeral.Controls.Add(this.tabControl1);
            this.PaginaAlunoAcessoGeral.Location = new System.Drawing.Point(4, 25);
            this.PaginaAlunoAcessoGeral.Name = "PaginaAlunoAcessoGeral";
            this.PaginaAlunoAcessoGeral.Padding = new System.Windows.Forms.Padding(3);
            this.PaginaAlunoAcessoGeral.Size = new System.Drawing.Size(522, 632);
            this.PaginaAlunoAcessoGeral.TabIndex = 0;
            this.PaginaAlunoAcessoGeral.Text = "Acesso";
            this.PaginaAlunoAcessoGeral.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpAlunosAcessoSituacao);
            this.tabControl1.Controls.Add(this.tpAlunosAcessoOcorrencias);
            this.tabControl1.Controls.Add(this.tpAlunosAcessoParticularidades);
            this.tabControl1.Controls.Add(this.tpAlunosAcessoTemporizador);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(516, 626);
            this.tabControl1.TabIndex = 57;
            // 
            // tpAlunosAcessoSituacao
            // 
            this.tpAlunosAcessoSituacao.Controls.Add(this.chkBloquearAcessoAlunoSemMatricula);
            this.tpAlunosAcessoSituacao.Controls.Add(this.chkNegarAlunoPendenteDocumento);
            this.tpAlunosAcessoSituacao.Controls.Add(this.chkBloquearAcessoAlunoComPendenciaMaterial);
            this.tpAlunosAcessoSituacao.Controls.Add(this.chkBloquearAcessoAlunoInadimplente);
            this.tpAlunosAcessoSituacao.Location = new System.Drawing.Point(4, 25);
            this.tpAlunosAcessoSituacao.Name = "tpAlunosAcessoSituacao";
            this.tpAlunosAcessoSituacao.Padding = new System.Windows.Forms.Padding(3);
            this.tpAlunosAcessoSituacao.Size = new System.Drawing.Size(508, 597);
            this.tpAlunosAcessoSituacao.TabIndex = 0;
            this.tpAlunosAcessoSituacao.Text = "Situação";
            this.tpAlunosAcessoSituacao.UseVisualStyleBackColor = true;
            // 
            // chkBloquearAcessoAlunoSemMatricula
            // 
            this.chkBloquearAcessoAlunoSemMatricula.AutoSize = true;
            this.chkBloquearAcessoAlunoSemMatricula.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBloquearAcessoAlunoSemMatricula.Location = new System.Drawing.Point(3, 13);
            this.chkBloquearAcessoAlunoSemMatricula.Margin = new System.Windows.Forms.Padding(0);
            this.chkBloquearAcessoAlunoSemMatricula.Name = "chkBloquearAcessoAlunoSemMatricula";
            this.chkBloquearAcessoAlunoSemMatricula.Size = new System.Drawing.Size(250, 20);
            this.chkBloquearAcessoAlunoSemMatricula.TabIndex = 0;
            this.chkBloquearAcessoAlunoSemMatricula.Text = "Negar acesso para não matriculados";
            this.chkBloquearAcessoAlunoSemMatricula.UseVisualStyleBackColor = true;
            // 
            // chkNegarAlunoPendenteDocumento
            // 
            this.chkNegarAlunoPendenteDocumento.AutoSize = true;
            this.chkNegarAlunoPendenteDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNegarAlunoPendenteDocumento.Location = new System.Drawing.Point(3, 73);
            this.chkNegarAlunoPendenteDocumento.Margin = new System.Windows.Forms.Padding(0);
            this.chkNegarAlunoPendenteDocumento.Name = "chkNegarAlunoPendenteDocumento";
            this.chkNegarAlunoPendenteDocumento.Size = new System.Drawing.Size(323, 20);
            this.chkNegarAlunoPendenteDocumento.TabIndex = 2;
            this.chkNegarAlunoPendenteDocumento.Text = "Negar acesso para pendência de documentação";
            this.chkNegarAlunoPendenteDocumento.UseVisualStyleBackColor = true;
            // 
            // chkBloquearAcessoAlunoComPendenciaMaterial
            // 
            this.chkBloquearAcessoAlunoComPendenciaMaterial.AutoSize = true;
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Location = new System.Drawing.Point(3, 103);
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Margin = new System.Windows.Forms.Padding(0);
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Name = "chkBloquearAcessoAlunoComPendenciaMaterial";
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Size = new System.Drawing.Size(288, 20);
            this.chkBloquearAcessoAlunoComPendenciaMaterial.TabIndex = 3;
            this.chkBloquearAcessoAlunoComPendenciaMaterial.Text = "Negar acesso para pendência de materiais";
            this.chkBloquearAcessoAlunoComPendenciaMaterial.UseVisualStyleBackColor = true;
            // 
            // chkBloquearAcessoAlunoInadimplente
            // 
            this.chkBloquearAcessoAlunoInadimplente.AutoSize = true;
            this.chkBloquearAcessoAlunoInadimplente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBloquearAcessoAlunoInadimplente.Location = new System.Drawing.Point(3, 43);
            this.chkBloquearAcessoAlunoInadimplente.Margin = new System.Windows.Forms.Padding(0);
            this.chkBloquearAcessoAlunoInadimplente.Name = "chkBloquearAcessoAlunoInadimplente";
            this.chkBloquearAcessoAlunoInadimplente.Size = new System.Drawing.Size(231, 20);
            this.chkBloquearAcessoAlunoInadimplente.TabIndex = 1;
            this.chkBloquearAcessoAlunoInadimplente.Text = "Negar acesso para inadimplentes";
            this.chkBloquearAcessoAlunoInadimplente.UseVisualStyleBackColor = true;
            // 
            // tpAlunosAcessoOcorrencias
            // 
            this.tpAlunosAcessoOcorrencias.Controls.Add(this.cklOcorrenciasAluno);
            this.tpAlunosAcessoOcorrencias.Controls.Add(this.lblNegarOcorrenciasAluno);
            this.tpAlunosAcessoOcorrencias.Location = new System.Drawing.Point(4, 25);
            this.tpAlunosAcessoOcorrencias.Name = "tpAlunosAcessoOcorrencias";
            this.tpAlunosAcessoOcorrencias.Padding = new System.Windows.Forms.Padding(3);
            this.tpAlunosAcessoOcorrencias.Size = new System.Drawing.Size(508, 597);
            this.tpAlunosAcessoOcorrencias.TabIndex = 1;
            this.tpAlunosAcessoOcorrencias.Text = "Ocorrências";
            this.tpAlunosAcessoOcorrencias.UseVisualStyleBackColor = true;
            // 
            // cklOcorrenciasAluno
            // 
            this.cklOcorrenciasAluno.CheckOnClick = true;
            this.cklOcorrenciasAluno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklOcorrenciasAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cklOcorrenciasAluno.FormattingEnabled = true;
            this.cklOcorrenciasAluno.Location = new System.Drawing.Point(3, 3);
            this.cklOcorrenciasAluno.Name = "cklOcorrenciasAluno";
            this.cklOcorrenciasAluno.Size = new System.Drawing.Size(502, 591);
            this.cklOcorrenciasAluno.TabIndex = 5;
            // 
            // lblNegarOcorrenciasAluno
            // 
            this.lblNegarOcorrenciasAluno.AutoSize = true;
            this.lblNegarOcorrenciasAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNegarOcorrenciasAluno.Location = new System.Drawing.Point(7, 5);
            this.lblNegarOcorrenciasAluno.Margin = new System.Windows.Forms.Padding(0);
            this.lblNegarOcorrenciasAluno.Name = "lblNegarOcorrenciasAluno";
            this.lblNegarOcorrenciasAluno.Size = new System.Drawing.Size(220, 16);
            this.lblNegarOcorrenciasAluno.TabIndex = 4;
            this.lblNegarOcorrenciasAluno.Text = "Negar acesso para as ocorrências:";
            // 
            // tpAlunosAcessoParticularidades
            // 
            this.tpAlunosAcessoParticularidades.Controls.Add(this.lblAtributoBloqueado);
            this.tpAlunosAcessoParticularidades.Controls.Add(this.lblAtributoPodeSairSozinho);
            this.tpAlunosAcessoParticularidades.Controls.Add(this.cboAtributoPodeSairSozinho);
            this.tpAlunosAcessoParticularidades.Controls.Add(this.cboAtributoBloqueado);
            this.tpAlunosAcessoParticularidades.Location = new System.Drawing.Point(4, 25);
            this.tpAlunosAcessoParticularidades.Name = "tpAlunosAcessoParticularidades";
            this.tpAlunosAcessoParticularidades.Size = new System.Drawing.Size(508, 597);
            this.tpAlunosAcessoParticularidades.TabIndex = 2;
            this.tpAlunosAcessoParticularidades.Text = "Particularidade";
            this.tpAlunosAcessoParticularidades.UseVisualStyleBackColor = true;
            // 
            // lblAtributoBloqueado
            // 
            this.lblAtributoBloqueado.AutoSize = true;
            this.lblAtributoBloqueado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtributoBloqueado.Location = new System.Drawing.Point(3, 60);
            this.lblAtributoBloqueado.Name = "lblAtributoBloqueado";
            this.lblAtributoBloqueado.Size = new System.Drawing.Size(256, 16);
            this.lblAtributoBloqueado.TabIndex = 23;
            this.lblAtributoBloqueado.Text = "Selecione o atributo que bloqueia o aluno";
            // 
            // lblAtributoPodeSairSozinho
            // 
            this.lblAtributoPodeSairSozinho.AutoSize = true;
            this.lblAtributoPodeSairSozinho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtributoPodeSairSozinho.Location = new System.Drawing.Point(3, 7);
            this.lblAtributoPodeSairSozinho.Name = "lblAtributoPodeSairSozinho";
            this.lblAtributoPodeSairSozinho.Size = new System.Drawing.Size(349, 16);
            this.lblAtributoPodeSairSozinho.TabIndex = 6;
            this.lblAtributoPodeSairSozinho.Text = "Selecione o atributo que impedirá o aluno de sair sozinho";
            // 
            // cboAtributoPodeSairSozinho
            // 
            this.cboAtributoPodeSairSozinho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAtributoPodeSairSozinho.FormattingEnabled = true;
            this.cboAtributoPodeSairSozinho.Location = new System.Drawing.Point(3, 29);
            this.cboAtributoPodeSairSozinho.Name = "cboAtributoPodeSairSozinho";
            this.cboAtributoPodeSairSozinho.Size = new System.Drawing.Size(479, 24);
            this.cboAtributoPodeSairSozinho.TabIndex = 7;
            // 
            // cboAtributoBloqueado
            // 
            this.cboAtributoBloqueado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAtributoBloqueado.FormattingEnabled = true;
            this.cboAtributoBloqueado.Location = new System.Drawing.Point(3, 83);
            this.cboAtributoBloqueado.Name = "cboAtributoBloqueado";
            this.cboAtributoBloqueado.Size = new System.Drawing.Size(479, 24);
            this.cboAtributoBloqueado.TabIndex = 24;
            // 
            // tpAlunosAcessoTemporizador
            // 
            this.tpAlunosAcessoTemporizador.BackColor = System.Drawing.SystemColors.Control;
            this.tpAlunosAcessoTemporizador.Controls.Add(this.panel2);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.panel1);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.dgvSelecaoIntervalos);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.lblAcessoAluno);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.chkUmAcessoPorIntervalo);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.lblAcessoSegundos);
            this.tpAlunosAcessoTemporizador.Controls.Add(this.nmSegundoMinimoParaNovoAcesso);
            this.tpAlunosAcessoTemporizador.Location = new System.Drawing.Point(4, 25);
            this.tpAlunosAcessoTemporizador.Margin = new System.Windows.Forms.Padding(2);
            this.tpAlunosAcessoTemporizador.Name = "tpAlunosAcessoTemporizador";
            this.tpAlunosAcessoTemporizador.Size = new System.Drawing.Size(508, 597);
            this.tpAlunosAcessoTemporizador.TabIndex = 4;
            this.tpAlunosAcessoTemporizador.Text = "Temporizadres";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Controls.Add(this.btnCancelarIntervalo);
            this.panel2.Controls.Add(this.btnIntervaloAlterar);
            this.panel2.Controls.Add(this.btnIntervaloNovo);
            this.panel2.Controls.Add(this.btnIntervaloAdicionar);
            this.panel2.Controls.Add(this.btnIntervaloRemover);
            this.panel2.Location = new System.Drawing.Point(361, 194);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(141, 336);
            this.panel2.TabIndex = 70;
            // 
            // btnIntervaloAlterar
            // 
            this.btnIntervaloAlterar.BackColor = System.Drawing.Color.White;
            this.btnIntervaloAlterar.Enabled = false;
            this.btnIntervaloAlterar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntervaloAlterar.Location = new System.Drawing.Point(23, 142);
            this.btnIntervaloAlterar.Name = "btnIntervaloAlterar";
            this.btnIntervaloAlterar.Size = new System.Drawing.Size(94, 49);
            this.btnIntervaloAlterar.TabIndex = 70;
            this.btnIntervaloAlterar.Text = "Alterar Intervalo";
            this.btnIntervaloAlterar.UseVisualStyleBackColor = false;
            // 
            // btnIntervaloNovo
            // 
            this.btnIntervaloNovo.BackColor = System.Drawing.Color.White;
            this.btnIntervaloNovo.Enabled = false;
            this.btnIntervaloNovo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnIntervaloNovo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnIntervaloNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntervaloNovo.ForeColor = System.Drawing.Color.Black;
            this.btnIntervaloNovo.Location = new System.Drawing.Point(23, 25);
            this.btnIntervaloNovo.Name = "btnIntervaloNovo";
            this.btnIntervaloNovo.Size = new System.Drawing.Size(94, 49);
            this.btnIntervaloNovo.TabIndex = 69;
            this.btnIntervaloNovo.Text = "Novo \r\nIntervalo";
            this.btnIntervaloNovo.UseVisualStyleBackColor = false;
            this.btnIntervaloNovo.Click += new System.EventHandler(this.btnIntervaloNovo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.lblIntervalo);
            this.panel1.Controls.Add(this.lbTipoGiro);
            this.panel1.Controls.Add(this.mtbIntervaloHoraInicial);
            this.panel1.Controls.Add(this.rdbIntervaloSaida);
            this.panel1.Controls.Add(this.cboDiaSemana);
            this.panel1.Controls.Add(this.rdbIntervaloEntrada);
            this.panel1.Controls.Add(this.mtbIntervaloHoraFinal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(11, 106);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 71);
            this.panel1.TabIndex = 65;
            // 
            // lblIntervalo
            // 
            this.lblIntervalo.AutoSize = true;
            this.lblIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntervalo.Location = new System.Drawing.Point(3, 10);
            this.lblIntervalo.Name = "lblIntervalo";
            this.lblIntervalo.Size = new System.Drawing.Size(282, 20);
            this.lblIntervalo.TabIndex = 57;
            this.lblIntervalo.Text = "Configuraçoes de intervalos de acesso";
            // 
            // lbTipoGiro
            // 
            this.lbTipoGiro.AutoSize = true;
            this.lbTipoGiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoGiro.Location = new System.Drawing.Point(319, 10);
            this.lbTipoGiro.Name = "lbTipoGiro";
            this.lbTipoGiro.Size = new System.Drawing.Size(168, 20);
            this.lbTipoGiro.TabIndex = 64;
            this.lbTipoGiro.Text = "Configurar tipo acesso";
            // 
            // mtbIntervaloHoraInicial
            // 
            this.mtbIntervaloHoraInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbIntervaloHoraInicial.Location = new System.Drawing.Point(143, 35);
            this.mtbIntervaloHoraInicial.Mask = "00:00";
            this.mtbIntervaloHoraInicial.Name = "mtbIntervaloHoraInicial";
            this.mtbIntervaloHoraInicial.Size = new System.Drawing.Size(48, 26);
            this.mtbIntervaloHoraInicial.TabIndex = 59;
            this.mtbIntervaloHoraInicial.ValidatingType = typeof(System.DateTime);
            // 
            // rdbIntervaloSaida
            // 
            this.rdbIntervaloSaida.AutoSize = true;
            this.rdbIntervaloSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbIntervaloSaida.Location = new System.Drawing.Point(419, 36);
            this.rdbIntervaloSaida.Name = "rdbIntervaloSaida";
            this.rdbIntervaloSaida.Size = new System.Drawing.Size(68, 24);
            this.rdbIntervaloSaida.TabIndex = 63;
            this.rdbIntervaloSaida.Text = "Saída";
            this.rdbIntervaloSaida.UseVisualStyleBackColor = true;
            // 
            // cboDiaSemana
            // 
            this.cboDiaSemana.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDiaSemana.FormattingEnabled = true;
            this.cboDiaSemana.Items.AddRange(new object[] {
            "Domingo",
            "Segunda",
            "Terça",
            "Quarta",
            "Quinta",
            "Sexta",
            "Sábado"});
            this.cboDiaSemana.Location = new System.Drawing.Point(7, 35);
            this.cboDiaSemana.Name = "cboDiaSemana";
            this.cboDiaSemana.Size = new System.Drawing.Size(118, 28);
            this.cboDiaSemana.TabIndex = 58;
            // 
            // rdbIntervaloEntrada
            // 
            this.rdbIntervaloEntrada.AutoSize = true;
            this.rdbIntervaloEntrada.Checked = true;
            this.rdbIntervaloEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbIntervaloEntrada.Location = new System.Drawing.Point(323, 36);
            this.rdbIntervaloEntrada.Name = "rdbIntervaloEntrada";
            this.rdbIntervaloEntrada.Size = new System.Drawing.Size(84, 24);
            this.rdbIntervaloEntrada.TabIndex = 62;
            this.rdbIntervaloEntrada.TabStop = true;
            this.rdbIntervaloEntrada.Text = "Entrada";
            this.rdbIntervaloEntrada.UseVisualStyleBackColor = true;
            // 
            // mtbIntervaloHoraFinal
            // 
            this.mtbIntervaloHoraFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbIntervaloHoraFinal.Location = new System.Drawing.Point(237, 35);
            this.mtbIntervaloHoraFinal.Mask = "00:00";
            this.mtbIntervaloHoraFinal.Name = "mtbIntervaloHoraFinal";
            this.mtbIntervaloHoraFinal.Size = new System.Drawing.Size(48, 26);
            this.mtbIntervaloHoraFinal.TabIndex = 60;
            this.mtbIntervaloHoraFinal.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(197, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 20);
            this.label3.TabIndex = 61;
            this.label3.Text = "Até";
            // 
            // dgvSelecaoIntervalos
            // 
            this.dgvSelecaoIntervalos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvSelecaoIntervalos.BackColor = System.Drawing.Color.DarkGray;
            this.dgvSelecaoIntervalos.Location = new System.Drawing.Point(11, 200);
            this.dgvSelecaoIntervalos.MultiSelect = true;
            this.dgvSelecaoIntervalos.Name = "dgvSelecaoIntervalos";
            this.dgvSelecaoIntervalos.ReadOnly = true;
            this.dgvSelecaoIntervalos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelecaoIntervalos.Size = new System.Drawing.Size(337, 336);
            this.dgvSelecaoIntervalos.TabIndex = 16;
            this.dgvSelecaoIntervalos.Load += new System.EventHandler(this.dgvSelecaoIntervalos_Load);
            this.dgvSelecaoIntervalos.Click += new System.EventHandler(this.dgvSelecaoIntervalos_Click);
            // 
            // lblAcessoAluno
            // 
            this.lblAcessoAluno.AutoSize = true;
            this.lblAcessoAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcessoAluno.Location = new System.Drawing.Point(7, 22);
            this.lblAcessoAluno.Name = "lblAcessoAluno";
            this.lblAcessoAluno.Size = new System.Drawing.Size(296, 20);
            this.lblAcessoAluno.TabIndex = 12;
            this.lblAcessoAluno.Text = "Tempo minimo para realizar novo acesso";
            // 
            // chkUmAcessoPorIntervalo
            // 
            this.chkUmAcessoPorIntervalo.AutoSize = true;
            this.chkUmAcessoPorIntervalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUmAcessoPorIntervalo.Location = new System.Drawing.Point(11, 64);
            this.chkUmAcessoPorIntervalo.Name = "chkUmAcessoPorIntervalo";
            this.chkUmAcessoPorIntervalo.Size = new System.Drawing.Size(394, 24);
            this.chkUmAcessoPorIntervalo.TabIndex = 15;
            this.chkUmAcessoPorIntervalo.Text = "Aluno só pode realizar um acesso em cada intervalo";
            this.chkUmAcessoPorIntervalo.UseVisualStyleBackColor = true;
            this.chkUmAcessoPorIntervalo.CheckedChanged += new System.EventHandler(this.chkUmAcessoPorIntervalo_CheckedChanged);
            // 
            // lblAcessoSegundos
            // 
            this.lblAcessoSegundos.AutoSize = true;
            this.lblAcessoSegundos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcessoSegundos.Location = new System.Drawing.Point(372, 22);
            this.lblAcessoSegundos.Name = "lblAcessoSegundos";
            this.lblAcessoSegundos.Size = new System.Drawing.Size(79, 20);
            this.lblAcessoSegundos.TabIndex = 14;
            this.lblAcessoSegundos.Text = "segundos";
            // 
            // nmSegundoMinimoParaNovoAcesso
            // 
            this.nmSegundoMinimoParaNovoAcesso.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmSegundoMinimoParaNovoAcesso.Location = new System.Drawing.Point(309, 20);
            this.nmSegundoMinimoParaNovoAcesso.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.nmSegundoMinimoParaNovoAcesso.Name = "nmSegundoMinimoParaNovoAcesso";
            this.nmSegundoMinimoParaNovoAcesso.Size = new System.Drawing.Size(57, 26);
            this.nmSegundoMinimoParaNovoAcesso.TabIndex = 13;
            this.nmSegundoMinimoParaNovoAcesso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmSegundoMinimoParaNovoAcesso.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PaginaAlunoAcessoMensagem
            // 
            this.PaginaAlunoAcessoMensagem.Controls.Add(this.chkMsgInadimplentes);
            this.PaginaAlunoAcessoMensagem.Controls.Add(this.lblMsgOcorrencias);
            this.PaginaAlunoAcessoMensagem.Controls.Add(this.chkMsgPendenteDocumento);
            this.PaginaAlunoAcessoMensagem.Controls.Add(this.cklMsgOcorrencias);
            this.PaginaAlunoAcessoMensagem.Controls.Add(this.chkMsgPendenteMateriais);
            this.PaginaAlunoAcessoMensagem.Location = new System.Drawing.Point(4, 25);
            this.PaginaAlunoAcessoMensagem.Name = "PaginaAlunoAcessoMensagem";
            this.PaginaAlunoAcessoMensagem.Padding = new System.Windows.Forms.Padding(3);
            this.PaginaAlunoAcessoMensagem.Size = new System.Drawing.Size(522, 632);
            this.PaginaAlunoAcessoMensagem.TabIndex = 1;
            this.PaginaAlunoAcessoMensagem.Text = "Mensagens";
            this.PaginaAlunoAcessoMensagem.UseVisualStyleBackColor = true;
            // 
            // chkMsgInadimplentes
            // 
            this.chkMsgInadimplentes.AutoSize = true;
            this.chkMsgInadimplentes.Location = new System.Drawing.Point(4, 5);
            this.chkMsgInadimplentes.Name = "chkMsgInadimplentes";
            this.chkMsgInadimplentes.Size = new System.Drawing.Size(318, 20);
            this.chkMsgInadimplentes.TabIndex = 0;
            this.chkMsgInadimplentes.Text = "Emitir mensagem em monitor para inadimplentes";
            this.chkMsgInadimplentes.UseVisualStyleBackColor = true;
            // 
            // lblMsgOcorrencias
            // 
            this.lblMsgOcorrencias.AutoSize = true;
            this.lblMsgOcorrencias.Location = new System.Drawing.Point(4, 97);
            this.lblMsgOcorrencias.Name = "lblMsgOcorrencias";
            this.lblMsgOcorrencias.Size = new System.Drawing.Size(307, 16);
            this.lblMsgOcorrencias.TabIndex = 3;
            this.lblMsgOcorrencias.Text = "Emitir mensagem em monitor para as ocorrências:";
            // 
            // chkMsgPendenteDocumento
            // 
            this.chkMsgPendenteDocumento.AutoSize = true;
            this.chkMsgPendenteDocumento.Location = new System.Drawing.Point(4, 35);
            this.chkMsgPendenteDocumento.Name = "chkMsgPendenteDocumento";
            this.chkMsgPendenteDocumento.Size = new System.Drawing.Size(410, 20);
            this.chkMsgPendenteDocumento.TabIndex = 1;
            this.chkMsgPendenteDocumento.Text = "Emitir mensagem em monitor para pendência de documentação";
            this.chkMsgPendenteDocumento.UseVisualStyleBackColor = true;
            // 
            // cklMsgOcorrencias
            // 
            this.cklMsgOcorrencias.CheckOnClick = true;
            this.cklMsgOcorrencias.FormattingEnabled = true;
            this.cklMsgOcorrencias.Location = new System.Drawing.Point(4, 122);
            this.cklMsgOcorrencias.Name = "cklMsgOcorrencias";
            this.cklMsgOcorrencias.Size = new System.Drawing.Size(439, 463);
            this.cklMsgOcorrencias.TabIndex = 4;
            // 
            // chkMsgPendenteMateriais
            // 
            this.chkMsgPendenteMateriais.AutoSize = true;
            this.chkMsgPendenteMateriais.Location = new System.Drawing.Point(4, 65);
            this.chkMsgPendenteMateriais.Name = "chkMsgPendenteMateriais";
            this.chkMsgPendenteMateriais.Size = new System.Drawing.Size(375, 20);
            this.chkMsgPendenteMateriais.TabIndex = 2;
            this.chkMsgPendenteMateriais.Text = "Emitir mensagem em monitor para pendência de materiais";
            this.chkMsgPendenteMateriais.UseVisualStyleBackColor = true;
            // 
            // PaginaAcessoAlunoLiberacao
            // 
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.chkFormLiberacao);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.lblTempoLiberado);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.cklColaboradores);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.lblTempoLiberadoSegundos);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.cklProfessores);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.chkResponsavelLibera);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.lblSelecionarColaboradores);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.numTempoPassagemAutorizado);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.lblSelecionarProfessores);
            this.PaginaAcessoAlunoLiberacao.Controls.Add(this.chkAutorizadoLibera);
            this.PaginaAcessoAlunoLiberacao.Location = new System.Drawing.Point(4, 25);
            this.PaginaAcessoAlunoLiberacao.Name = "PaginaAcessoAlunoLiberacao";
            this.PaginaAcessoAlunoLiberacao.Size = new System.Drawing.Size(522, 632);
            this.PaginaAcessoAlunoLiberacao.TabIndex = 2;
            this.PaginaAcessoAlunoLiberacao.Text = "Liberação";
            this.PaginaAcessoAlunoLiberacao.UseVisualStyleBackColor = true;
            // 
            // chkFormLiberacao
            // 
            this.chkFormLiberacao.AutoSize = true;
            this.chkFormLiberacao.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkFormLiberacao.Location = new System.Drawing.Point(4, 32);
            this.chkFormLiberacao.Name = "chkFormLiberacao";
            this.chkFormLiberacao.Size = new System.Drawing.Size(208, 20);
            this.chkFormLiberacao.TabIndex = 9;
            this.chkFormLiberacao.Text = "Utilizar formulário de liberação";
            this.chkFormLiberacao.UseVisualStyleBackColor = true;
            // 
            // lblTempoLiberado
            // 
            this.lblTempoLiberado.AutoSize = true;
            this.lblTempoLiberado.Location = new System.Drawing.Point(4, 6);
            this.lblTempoLiberado.Name = "lblTempoLiberado";
            this.lblTempoLiberado.Size = new System.Drawing.Size(220, 16);
            this.lblTempoLiberado.TabIndex = 0;
            this.lblTempoLiberado.Text = "Tempo para aluno liberado, passar";
            // 
            // cklColaboradores
            // 
            this.cklColaboradores.CheckOnClick = true;
            this.cklColaboradores.FormattingEnabled = true;
            this.cklColaboradores.Location = new System.Drawing.Point(4, 400);
            this.cklColaboradores.Name = "cklColaboradores";
            this.cklColaboradores.Size = new System.Drawing.Size(442, 191);
            this.cklColaboradores.TabIndex = 8;
            // 
            // lblTempoLiberadoSegundos
            // 
            this.lblTempoLiberadoSegundos.AutoSize = true;
            this.lblTempoLiberadoSegundos.Location = new System.Drawing.Point(287, 6);
            this.lblTempoLiberadoSegundos.Name = "lblTempoLiberadoSegundos";
            this.lblTempoLiberadoSegundos.Size = new System.Drawing.Size(68, 16);
            this.lblTempoLiberadoSegundos.TabIndex = 2;
            this.lblTempoLiberadoSegundos.Text = "segundos";
            // 
            // cklProfessores
            // 
            this.cklProfessores.CheckOnClick = true;
            this.cklProfessores.FormattingEnabled = true;
            this.cklProfessores.Location = new System.Drawing.Point(4, 158);
            this.cklProfessores.Margin = new System.Windows.Forms.Padding(0);
            this.cklProfessores.Name = "cklProfessores";
            this.cklProfessores.Size = new System.Drawing.Size(442, 191);
            this.cklProfessores.TabIndex = 6;
            // 
            // chkResponsavelLibera
            // 
            this.chkResponsavelLibera.AutoSize = true;
            this.chkResponsavelLibera.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkResponsavelLibera.Location = new System.Drawing.Point(4, 62);
            this.chkResponsavelLibera.Name = "chkResponsavelLibera";
            this.chkResponsavelLibera.Size = new System.Drawing.Size(231, 20);
            this.chkResponsavelLibera.TabIndex = 3;
            this.chkResponsavelLibera.Text = "Responsável pode liberar o aluno";
            this.chkResponsavelLibera.UseVisualStyleBackColor = true;
            // 
            // lblSelecionarColaboradores
            // 
            this.lblSelecionarColaboradores.AutoSize = true;
            this.lblSelecionarColaboradores.Location = new System.Drawing.Point(6, 377);
            this.lblSelecionarColaboradores.Name = "lblSelecionarColaboradores";
            this.lblSelecionarColaboradores.Size = new System.Drawing.Size(365, 16);
            this.lblSelecionarColaboradores.TabIndex = 7;
            this.lblSelecionarColaboradores.Text = "Selecione os colaboradores autorizados a liberar os alunos";
            // 
            // numTempoPassagemAutorizado
            // 
            this.numTempoPassagemAutorizado.Location = new System.Drawing.Point(227, 3);
            this.numTempoPassagemAutorizado.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numTempoPassagemAutorizado.Name = "numTempoPassagemAutorizado";
            this.numTempoPassagemAutorizado.Size = new System.Drawing.Size(57, 22);
            this.numTempoPassagemAutorizado.TabIndex = 1;
            this.numTempoPassagemAutorizado.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // lblSelecionarProfessores
            // 
            this.lblSelecionarProfessores.AutoSize = true;
            this.lblSelecionarProfessores.Location = new System.Drawing.Point(4, 135);
            this.lblSelecionarProfessores.Name = "lblSelecionarProfessores";
            this.lblSelecionarProfessores.Size = new System.Drawing.Size(348, 16);
            this.lblSelecionarProfessores.TabIndex = 5;
            this.lblSelecionarProfessores.Text = "Selecione os professores autorizados a liberar os alunos";
            // 
            // chkAutorizadoLibera
            // 
            this.chkAutorizadoLibera.AutoSize = true;
            this.chkAutorizadoLibera.Location = new System.Drawing.Point(4, 92);
            this.chkAutorizadoLibera.Name = "chkAutorizadoLibera";
            this.chkAutorizadoLibera.Size = new System.Drawing.Size(316, 20);
            this.chkAutorizadoLibera.TabIndex = 4;
            this.chkAutorizadoLibera.Text = "Autorizado a buscar o aluno pode liberar o aluno";
            this.chkAutorizadoLibera.UseVisualStyleBackColor = true;
            // 
            // tbcConteudo
            // 
            this.tbcConteudo.Controls.Add(this.PaginaAlunos);
            this.tbcConteudo.Controls.Add(this.PaginaOutros);
            this.tbcConteudo.Controls.Add(this.PaginaTeclado);
            this.tbcConteudo.Controls.Add(this.PaginaCatracas);
            this.tbcConteudo.Controls.Add(this.PaginaServidor);
            this.tbcConteudo.Controls.Add(this.PaginaCustomizacoes);
            this.tbcConteudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcConteudo.Location = new System.Drawing.Point(0, 0);
            this.tbcConteudo.Name = "tbcConteudo";
            this.tbcConteudo.SelectedIndex = 0;
            this.tbcConteudo.Size = new System.Drawing.Size(544, 696);
            this.tbcConteudo.TabIndex = 0;
            // 
            // PaginaCustomizacoes
            // 
            this.PaginaCustomizacoes.Controls.Add(this.tbcAnaliseCFG);
            this.PaginaCustomizacoes.Location = new System.Drawing.Point(4, 25);
            this.PaginaCustomizacoes.Name = "PaginaCustomizacoes";
            this.PaginaCustomizacoes.Size = new System.Drawing.Size(536, 667);
            this.PaginaCustomizacoes.TabIndex = 7;
            this.PaginaCustomizacoes.Text = "Customizações";
            this.PaginaCustomizacoes.UseVisualStyleBackColor = true;
            // 
            // tbcAnaliseCFG
            // 
            this.tbcAnaliseCFG.Controls.Add(this.PaginaTipoPessoa);
            this.tbcAnaliseCFG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcAnaliseCFG.Location = new System.Drawing.Point(0, 0);
            this.tbcAnaliseCFG.Name = "tbcAnaliseCFG";
            this.tbcAnaliseCFG.SelectedIndex = 0;
            this.tbcAnaliseCFG.Size = new System.Drawing.Size(536, 667);
            this.tbcAnaliseCFG.TabIndex = 2;
            // 
            // PaginaTipoPessoa
            // 
            this.PaginaTipoPessoa.BackColor = System.Drawing.SystemColors.Control;
            this.PaginaTipoPessoa.Controls.Add(this.gbIndentificadorTipoPessoa);
            this.PaginaTipoPessoa.Controls.Add(this.ckbExisteCustomizacaoTipoPessoa);
            this.PaginaTipoPessoa.Location = new System.Drawing.Point(4, 25);
            this.PaginaTipoPessoa.Name = "PaginaTipoPessoa";
            this.PaginaTipoPessoa.Size = new System.Drawing.Size(528, 638);
            this.PaginaTipoPessoa.TabIndex = 4;
            this.PaginaTipoPessoa.Text = "Tipo Pessoa";
            // 
            // gbIndentificadorTipoPessoa
            // 
            this.gbIndentificadorTipoPessoa.Controls.Add(this.lbTipoPessoaAutorizadoBuscaAluno);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.lbTipoPessoaAluno);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.txtTipoPessoaAutorizadoBuscarAluno);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.txtTipoPessoaAluno);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.lbTipoPessoaProfessor);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.txtTipoPessoaResponsavel);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.txtTipoPessoaProfessor);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.lbTipoPessoaResponsavel);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.lbTipoPessoaProfissional);
            this.gbIndentificadorTipoPessoa.Controls.Add(this.txtTipoPessoaProfissional);
            this.gbIndentificadorTipoPessoa.Enabled = false;
            this.gbIndentificadorTipoPessoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIndentificadorTipoPessoa.Location = new System.Drawing.Point(21, 37);
            this.gbIndentificadorTipoPessoa.Name = "gbIndentificadorTipoPessoa";
            this.gbIndentificadorTipoPessoa.Size = new System.Drawing.Size(486, 174);
            this.gbIndentificadorTipoPessoa.TabIndex = 14;
            this.gbIndentificadorTipoPessoa.TabStop = false;
            this.gbIndentificadorTipoPessoa.Text = "Identificador Tipo de Pessoa";
            // 
            // lbTipoPessoaAutorizadoBuscaAluno
            // 
            this.lbTipoPessoaAutorizadoBuscaAluno.AutoSize = true;
            this.lbTipoPessoaAutorizadoBuscaAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPessoaAutorizadoBuscaAluno.Location = new System.Drawing.Point(16, 140);
            this.lbTipoPessoaAutorizadoBuscaAluno.Name = "lbTipoPessoaAutorizadoBuscaAluno";
            this.lbTipoPessoaAutorizadoBuscaAluno.Size = new System.Drawing.Size(294, 16);
            this.lbTipoPessoaAutorizadoBuscaAluno.TabIndex = 11;
            this.lbTipoPessoaAutorizadoBuscaAluno.Text = "( 6 ) Tipo Pessoa Autorizado Buscar Aluno Para:";
            // 
            // lbTipoPessoaAluno
            // 
            this.lbTipoPessoaAluno.AutoSize = true;
            this.lbTipoPessoaAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPessoaAluno.Location = new System.Drawing.Point(16, 31);
            this.lbTipoPessoaAluno.Name = "lbTipoPessoaAluno";
            this.lbTipoPessoaAluno.Size = new System.Drawing.Size(185, 16);
            this.lbTipoPessoaAluno.TabIndex = 1;
            this.lbTipoPessoaAluno.Text = "( 1 )  Tipo Pessoa Aluno Para:";
            // 
            // txtTipoPessoaAutorizadoBuscarAluno
            // 
            this.txtTipoPessoaAutorizadoBuscarAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoPessoaAutorizadoBuscarAluno.Location = new System.Drawing.Point(408, 137);
            this.txtTipoPessoaAutorizadoBuscarAluno.MaxLength = 15;
            this.txtTipoPessoaAutorizadoBuscarAluno.Name = "txtTipoPessoaAutorizadoBuscarAluno";
            this.txtTipoPessoaAutorizadoBuscarAluno.Size = new System.Drawing.Size(30, 22);
            this.txtTipoPessoaAutorizadoBuscarAluno.TabIndex = 12;
            this.txtTipoPessoaAutorizadoBuscarAluno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTipoPessoaAluno
            // 
            this.txtTipoPessoaAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoPessoaAluno.Location = new System.Drawing.Point(408, 25);
            this.txtTipoPessoaAluno.MaxLength = 15;
            this.txtTipoPessoaAluno.Name = "txtTipoPessoaAluno";
            this.txtTipoPessoaAluno.Size = new System.Drawing.Size(30, 22);
            this.txtTipoPessoaAluno.TabIndex = 4;
            this.txtTipoPessoaAluno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbTipoPessoaProfessor
            // 
            this.lbTipoPessoaProfessor.AutoSize = true;
            this.lbTipoPessoaProfessor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPessoaProfessor.Location = new System.Drawing.Point(16, 56);
            this.lbTipoPessoaProfessor.Name = "lbTipoPessoaProfessor";
            this.lbTipoPessoaProfessor.Size = new System.Drawing.Size(209, 16);
            this.lbTipoPessoaProfessor.TabIndex = 5;
            this.lbTipoPessoaProfessor.Text = "( 2 )  Tipo Pessoa Professor Para:";
            // 
            // txtTipoPessoaResponsavel
            // 
            this.txtTipoPessoaResponsavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoPessoaResponsavel.Location = new System.Drawing.Point(408, 109);
            this.txtTipoPessoaResponsavel.MaxLength = 15;
            this.txtTipoPessoaResponsavel.Name = "txtTipoPessoaResponsavel";
            this.txtTipoPessoaResponsavel.Size = new System.Drawing.Size(30, 22);
            this.txtTipoPessoaResponsavel.TabIndex = 10;
            this.txtTipoPessoaResponsavel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTipoPessoaProfessor
            // 
            this.txtTipoPessoaProfessor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoPessoaProfessor.Location = new System.Drawing.Point(408, 53);
            this.txtTipoPessoaProfessor.MaxLength = 15;
            this.txtTipoPessoaProfessor.Name = "txtTipoPessoaProfessor";
            this.txtTipoPessoaProfessor.Size = new System.Drawing.Size(30, 22);
            this.txtTipoPessoaProfessor.TabIndex = 6;
            this.txtTipoPessoaProfessor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbTipoPessoaResponsavel
            // 
            this.lbTipoPessoaResponsavel.AutoSize = true;
            this.lbTipoPessoaResponsavel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPessoaResponsavel.Location = new System.Drawing.Point(16, 112);
            this.lbTipoPessoaResponsavel.Name = "lbTipoPessoaResponsavel";
            this.lbTipoPessoaResponsavel.Size = new System.Drawing.Size(232, 16);
            this.lbTipoPessoaResponsavel.TabIndex = 9;
            this.lbTipoPessoaResponsavel.Text = "( 4 )  Tipo Pessoa Responsavel Para:";
            // 
            // lbTipoPessoaProfissional
            // 
            this.lbTipoPessoaProfissional.AutoSize = true;
            this.lbTipoPessoaProfissional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoPessoaProfissional.Location = new System.Drawing.Point(16, 84);
            this.lbTipoPessoaProfissional.Name = "lbTipoPessoaProfissional";
            this.lbTipoPessoaProfissional.Size = new System.Drawing.Size(218, 16);
            this.lbTipoPessoaProfissional.TabIndex = 7;
            this.lbTipoPessoaProfissional.Text = "( 3 ) Tipo Pessoa Profissional Para:";
            // 
            // txtTipoPessoaProfissional
            // 
            this.txtTipoPessoaProfissional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoPessoaProfissional.Location = new System.Drawing.Point(408, 81);
            this.txtTipoPessoaProfissional.MaxLength = 15;
            this.txtTipoPessoaProfissional.Name = "txtTipoPessoaProfissional";
            this.txtTipoPessoaProfissional.Size = new System.Drawing.Size(30, 22);
            this.txtTipoPessoaProfissional.TabIndex = 8;
            this.txtTipoPessoaProfissional.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ckbExisteCustomizacaoTipoPessoa
            // 
            this.ckbExisteCustomizacaoTipoPessoa.AutoSize = true;
            this.ckbExisteCustomizacaoTipoPessoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbExisteCustomizacaoTipoPessoa.Location = new System.Drawing.Point(8, 7);
            this.ckbExisteCustomizacaoTipoPessoa.Name = "ckbExisteCustomizacaoTipoPessoa";
            this.ckbExisteCustomizacaoTipoPessoa.Size = new System.Drawing.Size(397, 24);
            this.ckbExisteCustomizacaoTipoPessoa.TabIndex = 13;
            this.ckbExisteCustomizacaoTipoPessoa.Text = "Deve Criar Configurações Tipo Pessoa Customizada";
            this.ckbExisteCustomizacaoTipoPessoa.UseVisualStyleBackColor = true;
            this.ckbExisteCustomizacaoTipoPessoa.CheckedChanged += new System.EventHandler(this.ckbExisteCustomizacaoTipoPessoa_CheckedChanged);
            // 
            // txtAtalhos
            // 
            this.txtAtalhos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtAtalhos.BackColor = System.Drawing.SystemColors.Control;
            this.txtAtalhos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAtalhos.ForeColor = System.Drawing.SystemColors.Control;
            this.txtAtalhos.Location = new System.Drawing.Point(550, 672);
            this.txtAtalhos.Name = "txtAtalhos";
            this.txtAtalhos.Size = new System.Drawing.Size(87, 13);
            this.txtAtalhos.TabIndex = 54;
            this.txtAtalhos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmConfiguraAcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 774);
            this.Controls.Add(this.btnFechar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmConfiguraAcesso";
            this.Text = "EMCatraca - Configurações Gerais";
            this.Load += new System.EventHandler(this.FrmConfiguracaoCatracas_Load);
            this.Controls.SetChildIndex(this.btnFechar, 0);
            this.Controls.SetChildIndex(this.pnlConteudo, 0);
            this.pnlConteudo.ResumeLayout(false);
            this.pnlConteudo.PerformLayout();
            this.PaginaServidor.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PaginaCatracas.ResumeLayout(false);
            this.pnFiltro.ResumeLayout(false);
            this.flpFiltros.ResumeLayout(false);
            this.pnCatracaDados.ResumeLayout(false);
            this.pnCatracaDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoCatraca)).EndInit();
            this.PaginaTeclado.ResumeLayout(false);
            this.PaginaTeclado.PerformLayout();
            this.PaginaOutros.ResumeLayout(false);
            this.PaginaOutros.PerformLayout();
            this.PaginaAlunos.ResumeLayout(false);
            this.tbcAlunoAcesso.ResumeLayout(false);
            this.PaginaAlunoAcessoGeral.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpAlunosAcessoSituacao.ResumeLayout(false);
            this.tpAlunosAcessoSituacao.PerformLayout();
            this.tpAlunosAcessoOcorrencias.ResumeLayout(false);
            this.tpAlunosAcessoOcorrencias.PerformLayout();
            this.tpAlunosAcessoParticularidades.ResumeLayout(false);
            this.tpAlunosAcessoParticularidades.PerformLayout();
            this.tpAlunosAcessoTemporizador.ResumeLayout(false);
            this.tpAlunosAcessoTemporizador.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmSegundoMinimoParaNovoAcesso)).EndInit();
            this.PaginaAlunoAcessoMensagem.ResumeLayout(false);
            this.PaginaAlunoAcessoMensagem.PerformLayout();
            this.PaginaAcessoAlunoLiberacao.ResumeLayout(false);
            this.PaginaAcessoAlunoLiberacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempoPassagemAutorizado)).EndInit();
            this.tbcConteudo.ResumeLayout(false);
            this.PaginaCustomizacoes.ResumeLayout(false);
            this.tbcAnaliseCFG.ResumeLayout(false);
            this.PaginaTipoPessoa.ResumeLayout(false);
            this.PaginaTipoPessoa.PerformLayout();
            this.gbIndentificadorTipoPessoa.ResumeLayout(false);
            this.gbIndentificadorTipoPessoa.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip tipConfig;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnGravarGeral;
        private System.Windows.Forms.TabControl tbcConteudo;
        private System.Windows.Forms.TabPage PaginaAlunos;
        private System.Windows.Forms.TabPage PaginaOutros;
        private System.Windows.Forms.Label lblNegarAcessoOcorrenciasColaborador;
        private System.Windows.Forms.CheckedListBox cklNegarOcorrenciasColaborador;
        private System.Windows.Forms.CheckBox chkBloquearAcessoColaboradorInativo;
        private System.Windows.Forms.Label lblNegarAcessoOcorrenciasProfessor;
        private System.Windows.Forms.CheckedListBox cklBloquearAcessoProfessorComOcorrencias;
        private System.Windows.Forms.CheckBox chkBloquearAcessoProfessorInativo;
        private System.Windows.Forms.CheckBox chkBloquearAcessoAutorizadoSemMatricula;
        private System.Windows.Forms.CheckBox chkBloquearAcessoResponsavelSemMatricula;
        private System.Windows.Forms.TabPage PaginaTeclado;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkTecladoIverter;
        private System.Windows.Forms.CheckBox chkTecladoTodos;
        private System.Windows.Forms.TextBox txtTecladoFiltrar;
        private System.Windows.Forms.Label lblTecladoFiltrar;
        private System.Windows.Forms.Label lblTecladoSelecione;
        private System.Windows.Forms.CheckedListBox cklTecladoPessoas;
        private System.Windows.Forms.ComboBox cboTecladoTipoPessoa;
        private System.Windows.Forms.TabPage PaginaCatracas;
        private System.Windows.Forms.TabPage PaginaServidor;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.TextBox txtPortaServidor;
        private System.Windows.Forms.TextBox txtTipoIntegracao;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbtConexaoBD;
        private System.Windows.Forms.RadioButton rdbtConexaoWebApi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.FolderBrowserDialog fbd1;
        private System.Windows.Forms.TabPage PaginaCustomizacoes;
        private System.Windows.Forms.TabControl tbcAnaliseCFG;
        private System.Windows.Forms.TextBox txtAtalhos;
        private System.Windows.Forms.TabPage PaginaTipoPessoa;
        private System.Windows.Forms.TabControl tbcAlunoAcesso;
        private System.Windows.Forms.TabPage PaginaAlunoAcessoGeral;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpAlunosAcessoSituacao;
        private System.Windows.Forms.CheckBox chkBloquearAcessoAlunoSemMatricula;
        private System.Windows.Forms.CheckBox chkNegarAlunoPendenteDocumento;
        private System.Windows.Forms.CheckBox chkBloquearAcessoAlunoComPendenciaMaterial;
        private System.Windows.Forms.CheckBox chkBloquearAcessoAlunoInadimplente;
        private System.Windows.Forms.TabPage tpAlunosAcessoOcorrencias;
        private System.Windows.Forms.CheckedListBox cklOcorrenciasAluno;
        private System.Windows.Forms.Label lblNegarOcorrenciasAluno;
        private System.Windows.Forms.TabPage tpAlunosAcessoParticularidades;
        private System.Windows.Forms.Label lblAtributoBloqueado;
        private System.Windows.Forms.Label lblAtributoPodeSairSozinho;
        private System.Windows.Forms.ComboBox cboAtributoPodeSairSozinho;
        private System.Windows.Forms.ComboBox cboAtributoBloqueado;
        private System.Windows.Forms.TabPage tpAlunosAcessoTemporizador;
        private System.Windows.Forms.TabPage PaginaAlunoAcessoMensagem;
        private System.Windows.Forms.CheckBox chkMsgInadimplentes;
        private System.Windows.Forms.Label lblMsgOcorrencias;
        private System.Windows.Forms.CheckBox chkMsgPendenteDocumento;
        private System.Windows.Forms.CheckedListBox cklMsgOcorrencias;
        private System.Windows.Forms.CheckBox chkMsgPendenteMateriais;
        private System.Windows.Forms.TabPage PaginaAcessoAlunoLiberacao;
        private System.Windows.Forms.CheckBox chkFormLiberacao;
        private System.Windows.Forms.Label lblTempoLiberado;
        private System.Windows.Forms.CheckedListBox cklColaboradores;
        private System.Windows.Forms.Label lblTempoLiberadoSegundos;
        private System.Windows.Forms.CheckedListBox cklProfessores;
        private System.Windows.Forms.CheckBox chkResponsavelLibera;
        private System.Windows.Forms.Label lblSelecionarColaboradores;
        private System.Windows.Forms.NumericUpDown numTempoPassagemAutorizado;
        private System.Windows.Forms.Label lblSelecionarProfessores;
        private System.Windows.Forms.CheckBox chkAutorizadoLibera;
        private ControlesUsuario.DataGridSelecaoCatraca dgvSelecaoIntervalos;
        private System.Windows.Forms.Label lblAcessoAluno;
        private System.Windows.Forms.CheckBox chkUmAcessoPorIntervalo;
        private System.Windows.Forms.Label lblAcessoSegundos;
        private System.Windows.Forms.NumericUpDown nmSegundoMinimoParaNovoAcesso;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIntervalo;
        private System.Windows.Forms.Label lbTipoGiro;
        private System.Windows.Forms.MaskedTextBox mtbIntervaloHoraInicial;
        private System.Windows.Forms.RadioButton rdbIntervaloSaida;
        private System.Windows.Forms.ComboBox cboDiaSemana;
        private System.Windows.Forms.RadioButton rdbIntervaloEntrada;
        private System.Windows.Forms.MaskedTextBox mtbIntervaloHoraFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnIntervaloNovo;
        private System.Windows.Forms.Button btnIntervaloAdicionar;
        private System.Windows.Forms.Button btnIntervaloRemover;
        private System.Windows.Forms.Button btnIntervaloAlterar;
        private System.Windows.Forms.Button btnCancelarIntervalo;
        private System.Windows.Forms.Panel pnFiltro;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Panel pnCatracaDados;
        private System.Windows.Forms.Label lblCatracaCodigo;
        private System.Windows.Forms.TextBox txtDescricaoCatraca;
        private System.Windows.Forms.TextBox txtIPCatraca;
        private System.Windows.Forms.NumericUpDown txtCodigoCatraca;
        private System.Windows.Forms.Label lblCatracaDescricao;
        private System.Windows.Forms.Label lblCatracaIp;
        private System.Windows.Forms.Label lblCatracaPorta;
        private System.Windows.Forms.TextBox txtPortaCatraca;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdbGiroSaida;
        private System.Windows.Forms.RadioButton rdbGiroEntrada;
        private ControlesUsuario.DataGridSelecaoCatraca dgvCatracas;
        private System.Windows.Forms.Button btnIncluirCatraca;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelarCatraca;
        private System.Windows.Forms.Button btnEditarCatraca;
        private System.Windows.Forms.RadioButton rdbGiroInvertido;
        private System.Windows.Forms.RadioButton rdbGiroNormal;
        private System.Windows.Forms.TextBox txtTipoPessoaResponsavel;
        private System.Windows.Forms.Label lbTipoPessoaResponsavel;
        private System.Windows.Forms.TextBox txtTipoPessoaProfissional;
        private System.Windows.Forms.Label lbTipoPessoaProfissional;
        private System.Windows.Forms.TextBox txtTipoPessoaProfessor;
        private System.Windows.Forms.Label lbTipoPessoaProfessor;
        private System.Windows.Forms.TextBox txtTipoPessoaAluno;
        private System.Windows.Forms.Label lbTipoPessoaAluno;
        private System.Windows.Forms.TextBox txtTipoPessoaAutorizadoBuscarAluno;
        private System.Windows.Forms.Label lbTipoPessoaAutorizadoBuscaAluno;
        private System.Windows.Forms.CheckBox ckbExisteCustomizacaoTipoPessoa;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbIndentificadorTipoPessoa;
    }
}