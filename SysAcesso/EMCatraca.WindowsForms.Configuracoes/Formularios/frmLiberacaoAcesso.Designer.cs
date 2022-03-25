namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    partial class frmLiberacaoAcesso
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLiberacaoAcesso));
            this.radAlunos = new System.Windows.Forms.RadioButton();
            this.radSerieTurma = new System.Windows.Forms.RadioButton();
            this.cboAlunos = new System.Windows.Forms.ComboBox();
            this.cboSerie = new System.Windows.Forms.ComboBox();
            this.cboTurma = new System.Windows.Forms.ComboBox();
            this.dgvLiberados = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.lblLiberados = new System.Windows.Forms.Label();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.lblTempo1 = new System.Windows.Forms.Label();
            this.numTempoAcesso = new System.Windows.Forms.NumericUpDown();
            this.lblTempo2 = new System.Windows.Forms.Label();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.pnlConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiberados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempoAcesso)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.Controls.Add(this.btnSair);
            this.pnlConteudo.Controls.Add(this.btnRemover);
            this.pnlConteudo.Controls.Add(this.lblTempo2);
            this.pnlConteudo.Controls.Add(this.numTempoAcesso);
            this.pnlConteudo.Controls.Add(this.lblTempo1);
            this.pnlConteudo.Controls.Add(this.btnLiberar);
            this.pnlConteudo.Controls.Add(this.lblLiberados);
            this.pnlConteudo.Controls.Add(this.lblMotivo);
            this.pnlConteudo.Controls.Add(this.txtMotivo);
            this.pnlConteudo.Controls.Add(this.dgvLiberados);
            this.pnlConteudo.Controls.Add(this.cboTurma);
            this.pnlConteudo.Controls.Add(this.cboSerie);
            this.pnlConteudo.Controls.Add(this.cboAlunos);
            this.pnlConteudo.Controls.Add(this.radSerieTurma);
            this.pnlConteudo.Controls.Add(this.radAlunos);
            this.pnlConteudo.ForeColor = System.Drawing.Color.Black;
            this.pnlConteudo.Size = new System.Drawing.Size(736, 662);
            this.pnlConteudo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlConteudo_MouseMove);
            // 
            // radAlunos
            // 
            this.radAlunos.AutoSize = true;
            this.radAlunos.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAlunos.Location = new System.Drawing.Point(3, 10);
            this.radAlunos.Name = "radAlunos";
            this.radAlunos.Size = new System.Drawing.Size(117, 24);
            this.radAlunos.TabIndex = 0;
            this.radAlunos.Text = "Lista de alunos";
            this.radAlunos.UseVisualStyleBackColor = true;
            this.radAlunos.CheckedChanged += new System.EventHandler(this.RadAlunos_CheckedChanged);
            // 
            // radSerieTurma
            // 
            this.radSerieTurma.AutoSize = true;
            this.radSerieTurma.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSerieTurma.Location = new System.Drawing.Point(3, 47);
            this.radSerieTurma.Name = "radSerieTurma";
            this.radSerieTurma.Size = new System.Drawing.Size(99, 24);
            this.radSerieTurma.TabIndex = 1;
            this.radSerieTurma.Text = "Série/Turma";
            this.radSerieTurma.UseVisualStyleBackColor = true;
            this.radSerieTurma.CheckedChanged += new System.EventHandler(this.RadSerieTurma_CheckedChanged);
            // 
            // cboAlunos
            // 
            this.cboAlunos.Enabled = false;
            this.cboAlunos.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAlunos.FormattingEnabled = true;
            this.cboAlunos.Location = new System.Drawing.Point(144, 9);
            this.cboAlunos.Name = "cboAlunos";
            this.cboAlunos.Size = new System.Drawing.Size(580, 28);
            this.cboAlunos.TabIndex = 2;
            this.cboAlunos.TextUpdate += new System.EventHandler(this.cboAlunos_TextUpdate);
            this.cboAlunos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboAlunos_KeyPress);
            // 
            // cboSerie
            // 
            this.cboSerie.Enabled = false;
            this.cboSerie.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSerie.FormattingEnabled = true;
            this.cboSerie.Location = new System.Drawing.Point(144, 47);
            this.cboSerie.Name = "cboSerie";
            this.cboSerie.Size = new System.Drawing.Size(402, 28);
            this.cboSerie.TabIndex = 3;
            this.cboSerie.SelectedIndexChanged += new System.EventHandler(this.CboSerie_SelectedIndexChanged);
            this.cboSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboSerie_KeyPress);
            // 
            // cboTurma
            // 
            this.cboTurma.Enabled = false;
            this.cboTurma.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTurma.FormattingEnabled = true;
            this.cboTurma.Location = new System.Drawing.Point(552, 47);
            this.cboTurma.Name = "cboTurma";
            this.cboTurma.Size = new System.Drawing.Size(172, 28);
            this.cboTurma.TabIndex = 4;
            this.cboTurma.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CboTurma_KeyPress);
            // 
            // dgvLiberados
            // 
            this.dgvLiberados.AllowUserToAddRows = false;
            this.dgvLiberados.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLiberados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLiberados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLiberados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nome,
            this.Tempo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLiberados.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLiberados.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLiberados.Location = new System.Drawing.Point(3, 183);
            this.dgvLiberados.MultiSelect = false;
            this.dgvLiberados.Name = "dgvLiberados";
            this.dgvLiberados.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLiberados.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLiberados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLiberados.Size = new System.Drawing.Size(584, 476);
            this.dgvLiberados.TabIndex = 5;
            this.dgvLiberados.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLiberados_CellMouseMove);
            // 
            // Id
            // 
            this.Id.HeaderText = "Matrícula";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Nome
            // 
            this.Nome.HeaderText = "Alunos";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 360;
            // 
            // Tempo
            // 
            this.Tempo.HeaderText = "Espera";
            this.Tempo.Name = "Tempo";
            this.Tempo.ReadOnly = true;
            this.Tempo.Width = 80;
            // 
            // txtMotivo
            // 
            this.txtMotivo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivo.Location = new System.Drawing.Point(144, 85);
            this.txtMotivo.MaxLength = 1000;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(580, 26);
            this.txtMotivo.TabIndex = 6;
            this.txtMotivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtMotivo_KeyPress);
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivo.Location = new System.Drawing.Point(3, 87);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(131, 20);
            this.lblMotivo.TabIndex = 7;
            this.lblMotivo.Text = "Motivo da liberação:";
            // 
            // lblLiberados
            // 
            this.lblLiberados.AutoSize = true;
            this.lblLiberados.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.lblLiberados.Location = new System.Drawing.Point(3, 160);
            this.lblLiberados.Name = "lblLiberados";
            this.lblLiberados.Size = new System.Drawing.Size(73, 20);
            this.lblLiberados.TabIndex = 8;
            this.lblLiberados.Text = "Liberados:";
            // 
            // btnLiberar
            // 
            this.btnLiberar.BackColor = System.Drawing.Color.White;
            this.btnLiberar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLiberar.Location = new System.Drawing.Point(600, 183);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(124, 37);
            this.btnLiberar.TabIndex = 9;
            this.btnLiberar.Text = "Liberar";
            this.btnLiberar.UseVisualStyleBackColor = false;
            this.btnLiberar.Click += new System.EventHandler(this.BtnLiberar_Click);
            // 
            // lblTempo1
            // 
            this.lblTempo1.AutoSize = true;
            this.lblTempo1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo1.Location = new System.Drawing.Point(3, 124);
            this.lblTempo1.Name = "lblTempo1";
            this.lblTempo1.Size = new System.Drawing.Size(140, 20);
            this.lblTempo1.TabIndex = 10;
            this.lblTempo1.Text = "Realizar o acesso em";
            // 
            // numTempoAcesso
            // 
            this.numTempoAcesso.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTempoAcesso.Location = new System.Drawing.Point(146, 121);
            this.numTempoAcesso.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numTempoAcesso.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTempoAcesso.Name = "numTempoAcesso";
            this.numTempoAcesso.Size = new System.Drawing.Size(52, 26);
            this.numTempoAcesso.TabIndex = 12;
            this.numTempoAcesso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTempoAcesso.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblTempo2
            // 
            this.lblTempo2.AutoSize = true;
            this.lblTempo2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTempo2.Location = new System.Drawing.Point(201, 125);
            this.lblTempo2.Name = "lblTempo2";
            this.lblTempo2.Size = new System.Drawing.Size(55, 20);
            this.lblTempo2.TabIndex = 13;
            this.lblTempo2.Text = "minutos";
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.White;
            this.btnRemover.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnRemover.Location = new System.Drawing.Point(600, 229);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(124, 37);
            this.btnRemover.TabIndex = 14;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.BtnRemover_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.White;
            this.btnSair.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.btnSair.Location = new System.Drawing.Point(600, 275);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(124, 37);
            this.btnSair.TabIndex = 15;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.BtnFechar_Click);
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
            this.btnFechar.Location = new System.Drawing.Point(691, 2);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(43, 44);
            this.btnFechar.TabIndex = 9;
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click_1);
            // 
            // frmLiberacaoAcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 734);
            this.Controls.Add(this.btnFechar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLiberacaoAcesso";
            this.Text = "frmLiberacaoAcesso";
            this.Load += new System.EventHandler(this.FrmLiberacaoAcesso_Load);
            this.Controls.SetChildIndex(this.pnlConteudo, 0);
            this.Controls.SetChildIndex(this.btnFechar, 0);
            this.pnlConteudo.ResumeLayout(false);
            this.pnlConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLiberados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempoAcesso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLiberados;
        private System.Windows.Forms.ComboBox cboTurma;
        private System.Windows.Forms.ComboBox cboSerie;
        private System.Windows.Forms.ComboBox cboAlunos;
        private System.Windows.Forms.RadioButton radSerieTurma;
        private System.Windows.Forms.RadioButton radAlunos;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.Label lblLiberados;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label lblTempo2;
        private System.Windows.Forms.NumericUpDown numTempoAcesso;
        private System.Windows.Forms.Label lblTempo1;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tempo;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnFechar;
    }
}