namespace EMCatraca.WindowsForms.Configuracoes.ControlesUsuario
{
    partial class ucDispositivo
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
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.flpControle = new System.Windows.Forms.FlowLayoutPanel();
            this.flpFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.pnCatracaDados = new System.Windows.Forms.Panel();
            this.chkTipoGiroCatraca = new System.Windows.Forms.CheckBox();
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
            this.lbTipoGiro = new System.Windows.Forms.Label();
            this.rdoIntervaloSaida = new System.Windows.Forms.RadioButton();
            this.rdoIntervaloEntrada = new System.Windows.Forms.RadioButton();
            this.gridDispositivo = new EMCatraca.WindowsForms.Configuracoes.ControlesUsuario.DataGridSelecaoCatraca();
            this.pnConteudo = new System.Windows.Forms.Panel();
            this.flpControle.SuspendLayout();
            this.flpFiltros.SuspendLayout();
            this.pnCatracaDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).BeginInit();
            this.pnCatracaLoginSenha.SuspendLayout();
            this.pnConteudo.SuspendLayout();
            this.SuspendLayout();
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
            this.btnSalvar.Location = new System.Drawing.Point(4, 128);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 37);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(4, 87);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 37);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExcluir.BackColor = System.Drawing.Color.White;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnExcluir.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnExcluir.ForeColor = System.Drawing.Color.Black;
            this.btnExcluir.Location = new System.Drawing.Point(4, 46);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(100, 37);
            this.btnExcluir.TabIndex = 12;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnIncluir
            // 
            this.btnIncluir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnIncluir.BackColor = System.Drawing.Color.White;
            this.btnIncluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnIncluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnIncluir.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnIncluir.ForeColor = System.Drawing.Color.Black;
            this.btnIncluir.Location = new System.Drawing.Point(4, 5);
            this.btnIncluir.Margin = new System.Windows.Forms.Padding(3, 5, 3, 2);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(100, 37);
            this.btnIncluir.TabIndex = 10;
            this.btnIncluir.Text = "Incluir";
            this.btnIncluir.UseVisualStyleBackColor = false;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // flpControle
            // 
            this.flpControle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpControle.Controls.Add(this.btnIncluir);
            this.flpControle.Controls.Add(this.btnExcluir);
            this.flpControle.Controls.Add(this.btnCancelar);
            this.flpControle.Controls.Add(this.btnSalvar);
            this.flpControle.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpControle.Location = new System.Drawing.Point(580, 4);
            this.flpControle.Margin = new System.Windows.Forms.Padding(4);
            this.flpControle.Name = "flpControle";
            this.flpControle.Size = new System.Drawing.Size(107, 236);
            this.flpControle.TabIndex = 62;
            // 
            // flpFiltros
            // 
            this.flpFiltros.Controls.Add(this.pnCatracaDados);
            this.flpFiltros.Controls.Add(this.pnCatracaLoginSenha);
            this.flpFiltros.Controls.Add(this.gridDispositivo);
            this.flpFiltros.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpFiltros.Location = new System.Drawing.Point(0, 0);
            this.flpFiltros.Margin = new System.Windows.Forms.Padding(0);
            this.flpFiltros.Name = "flpFiltros";
            this.flpFiltros.Size = new System.Drawing.Size(695, 773);
            this.flpFiltros.TabIndex = 58;
            // 
            // pnCatracaDados
            // 
            this.pnCatracaDados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnCatracaDados.Controls.Add(this.chkTipoGiroCatraca);
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
            this.pnCatracaDados.Size = new System.Drawing.Size(576, 169);
            this.pnCatracaDados.TabIndex = 14;
            // 
            // chkTipoGiroCatraca
            // 
            this.chkTipoGiroCatraca.AutoSize = true;
            this.chkTipoGiroCatraca.Location = new System.Drawing.Point(294, 133);
            this.chkTipoGiroCatraca.Margin = new System.Windows.Forms.Padding(4);
            this.chkTipoGiroCatraca.Name = "chkTipoGiroCatraca";
            this.chkTipoGiroCatraca.Size = new System.Drawing.Size(225, 21);
            this.chkTipoGiroCatraca.TabIndex = 6;
            this.chkTipoGiroCatraca.Text = "Ajustar tipo do giro por catraca";
            this.chkTipoGiroCatraca.UseVisualStyleBackColor = true;
            this.chkTipoGiroCatraca.CheckedChanged += new System.EventHandler(this.chkComunicacaoWebApi_CheckedChanged);
            // 
            // lblCatracaCodigo
            // 
            this.lblCatracaCodigo.AutoSize = true;
            this.lblCatracaCodigo.Location = new System.Drawing.Point(5, 6);
            this.lblCatracaCodigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatracaCodigo.Name = "lblCatracaCodigo";
            this.lblCatracaCodigo.Size = new System.Drawing.Size(129, 17);
            this.lblCatracaCodigo.TabIndex = 0;
            this.lblCatracaCodigo.Text = "Código da Catraca:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(166, 36);
            this.txtDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescricao.MaxLength = 20;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(345, 22);
            this.txtDescricao.TabIndex = 2;
            this.txtDescricao.Leave += new System.EventHandler(this.txtDescricao_Leave);
            // 
            // chkGiroInvertido
            // 
            this.chkGiroInvertido.AutoSize = true;
            this.chkGiroInvertido.Location = new System.Drawing.Point(169, 133);
            this.chkGiroInvertido.Margin = new System.Windows.Forms.Padding(4);
            this.chkGiroInvertido.Name = "chkGiroInvertido";
            this.chkGiroInvertido.Size = new System.Drawing.Size(115, 21);
            this.chkGiroInvertido.TabIndex = 5;
            this.chkGiroInvertido.Text = "Giro invertido";
            this.chkGiroInvertido.UseVisualStyleBackColor = true;
            this.chkGiroInvertido.CheckedChanged += new System.EventHandler(this.chkGiroInvertido_CheckedChanged);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(166, 66);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.MaxLength = 15;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(345, 22);
            this.txtIP.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(166, 6);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(80, 22);
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
            this.lblCatracaDescricao.Location = new System.Drawing.Point(5, 36);
            this.lblCatracaDescricao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatracaDescricao.Name = "lblCatracaDescricao";
            this.lblCatracaDescricao.Size = new System.Drawing.Size(75, 17);
            this.lblCatracaDescricao.TabIndex = 2;
            this.lblCatracaDescricao.Text = "Descrição:";
            // 
            // lblCatracaIp
            // 
            this.lblCatracaIp.AutoSize = true;
            this.lblCatracaIp.Location = new System.Drawing.Point(5, 66);
            this.lblCatracaIp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatracaIp.Name = "lblCatracaIp";
            this.lblCatracaIp.Size = new System.Drawing.Size(24, 17);
            this.lblCatracaIp.TabIndex = 4;
            this.lblCatracaIp.Text = "IP:";
            // 
            // lblCatracaPorta
            // 
            this.lblCatracaPorta.AutoSize = true;
            this.lblCatracaPorta.Location = new System.Drawing.Point(4, 98);
            this.lblCatracaPorta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCatracaPorta.Name = "lblCatracaPorta";
            this.lblCatracaPorta.Size = new System.Drawing.Size(46, 17);
            this.lblCatracaPorta.TabIndex = 6;
            this.lblCatracaPorta.Text = "Porta:";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(166, 98);
            this.txtPorta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorta.MaxLength = 5;
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(345, 22);
            this.txtPorta.TabIndex = 4;
            // 
            // pnCatracaLoginSenha
            // 
            this.pnCatracaLoginSenha.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnCatracaLoginSenha.Controls.Add(this.lbTipoGiro);
            this.pnCatracaLoginSenha.Controls.Add(this.rdoIntervaloSaida);
            this.pnCatracaLoginSenha.Controls.Add(this.rdoIntervaloEntrada);
            this.pnCatracaLoginSenha.Location = new System.Drawing.Point(0, 169);
            this.pnCatracaLoginSenha.Margin = new System.Windows.Forms.Padding(0);
            this.pnCatracaLoginSenha.Name = "pnCatracaLoginSenha";
            this.pnCatracaLoginSenha.Size = new System.Drawing.Size(576, 38);
            this.pnCatracaLoginSenha.TabIndex = 15;
            this.pnCatracaLoginSenha.Visible = false;
            // 
            // lbTipoGiro
            // 
            this.lbTipoGiro.AutoSize = true;
            this.lbTipoGiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lbTipoGiro.Location = new System.Drawing.Point(4, 11);
            this.lbTipoGiro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTipoGiro.Name = "lbTipoGiro";
            this.lbTipoGiro.Size = new System.Drawing.Size(150, 17);
            this.lbTipoGiro.TabIndex = 70;
            this.lbTipoGiro.Text = "Configurar tipo acesso";
            // 
            // rdoIntervaloSaida
            // 
            this.rdoIntervaloSaida.AutoSize = true;
            this.rdoIntervaloSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.rdoIntervaloSaida.Location = new System.Drawing.Point(269, 11);
            this.rdoIntervaloSaida.Margin = new System.Windows.Forms.Padding(4);
            this.rdoIntervaloSaida.Name = "rdoIntervaloSaida";
            this.rdoIntervaloSaida.Size = new System.Drawing.Size(65, 21);
            this.rdoIntervaloSaida.TabIndex = 69;
            this.rdoIntervaloSaida.Text = "Saída";
            this.rdoIntervaloSaida.UseVisualStyleBackColor = true;
            // 
            // rdoIntervaloEntrada
            // 
            this.rdoIntervaloEntrada.AutoSize = true;
            this.rdoIntervaloEntrada.Checked = true;
            this.rdoIntervaloEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.rdoIntervaloEntrada.Location = new System.Drawing.Point(169, 11);
            this.rdoIntervaloEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.rdoIntervaloEntrada.Name = "rdoIntervaloEntrada";
            this.rdoIntervaloEntrada.Size = new System.Drawing.Size(79, 21);
            this.rdoIntervaloEntrada.TabIndex = 68;
            this.rdoIntervaloEntrada.TabStop = true;
            this.rdoIntervaloEntrada.Text = "Entrada";
            this.rdoIntervaloEntrada.UseVisualStyleBackColor = true;
            // 
            // gridDispositivo
            // 
            this.gridDispositivo.Location = new System.Drawing.Point(5, 212);
            this.gridDispositivo.Margin = new System.Windows.Forms.Padding(5);
            this.gridDispositivo.MultiSelect = true;
            this.gridDispositivo.Name = "gridDispositivo";
            this.gridDispositivo.ReadOnly = true;
            this.gridDispositivo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridDispositivo.Size = new System.Drawing.Size(572, 561);
            this.gridDispositivo.TabIndex = 17;
            this.gridDispositivo.Load += new System.EventHandler(this.ControleDispositivo_Load);
            // 
            // pnConteudo
            // 
            this.pnConteudo.Controls.Add(this.flpFiltros);
            this.pnConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnConteudo.Location = new System.Drawing.Point(0, 0);
            this.pnConteudo.Margin = new System.Windows.Forms.Padding(4);
            this.pnConteudo.Name = "pnConteudo";
            this.pnConteudo.Size = new System.Drawing.Size(695, 773);
            this.pnConteudo.TabIndex = 61;
            // 
            // ucDispositivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpControle);
            this.Controls.Add(this.pnConteudo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucDispositivo";
            this.Size = new System.Drawing.Size(695, 773);
            this.flpControle.ResumeLayout(false);
            this.flpFiltros.ResumeLayout(false);
            this.pnCatracaDados.ResumeLayout(false);
            this.pnCatracaDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).EndInit();
            this.pnCatracaLoginSenha.ResumeLayout(false);
            this.pnCatracaLoginSenha.PerformLayout();
            this.pnConteudo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.FlowLayoutPanel flpControle;
        private System.Windows.Forms.FlowLayoutPanel flpFiltros;
        private System.Windows.Forms.Panel pnCatracaDados;
        private System.Windows.Forms.CheckBox chkTipoGiroCatraca;
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
        private System.Windows.Forms.Panel pnConteudo;
        private DataGridSelecaoCatraca gridDispositivo;
        private System.Windows.Forms.Label lbTipoGiro;
        private System.Windows.Forms.RadioButton rdoIntervaloSaida;
        private System.Windows.Forms.RadioButton rdoIntervaloEntrada;
    }
}
