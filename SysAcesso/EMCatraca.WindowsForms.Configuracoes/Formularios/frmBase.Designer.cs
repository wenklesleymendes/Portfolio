namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    partial class FrmBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBase));
            this.pnlConteudo = new System.Windows.Forms.Panel();
            this.lblFuncao = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.slMensagemStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.spbBarraProgresso = new System.Windows.Forms.ToolStripProgressBar();
            this.slNomeFormulario = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbStatus = new System.Windows.Forms.StatusStrip();
            this.slAmbiente = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.sbStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConteudo.Location = new System.Drawing.Point(0, 50);
            this.pnlConteudo.Name = "pnlConteudo";
            this.pnlConteudo.Size = new System.Drawing.Size(644, 289);
            this.pnlConteudo.TabIndex = 8;
            // 
            // lblFuncao
            // 
            this.lblFuncao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.lblFuncao.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuncao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(82)))), ((int)(((byte)(106)))));
            this.lblFuncao.Location = new System.Drawing.Point(302, 1);
            this.lblFuncao.Margin = new System.Windows.Forms.Padding(0);
            this.lblFuncao.Name = "lblFuncao";
            this.lblFuncao.Size = new System.Drawing.Size(342, 45);
            this.lblFuncao.TabIndex = 7;
            this.lblFuncao.Text = "Nome da Função";
            this.lblFuncao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::EMCatraca.Configuracao.Properties.Resources.HeaderFundoAntigo4;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(644, 50);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // slMensagemStatus
            // 
            this.slMensagemStatus.Name = "slMensagemStatus";
            this.slMensagemStatus.Size = new System.Drawing.Size(452, 17);
            this.slMensagemStatus.Spring = true;
            this.slMensagemStatus.Text = "slMensagemStatus";
            this.slMensagemStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.slMensagemStatus.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // spbBarraProgresso
            // 
            this.spbBarraProgresso.Name = "spbBarraProgresso";
            this.spbBarraProgresso.Size = new System.Drawing.Size(100, 16);
            // 
            // slNomeFormulario
            // 
            this.slNomeFormulario.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.slNomeFormulario.Name = "slNomeFormulario";
            this.slNomeFormulario.Size = new System.Drawing.Size(16, 17);
            this.slNomeFormulario.Text = "...";
            // 
            // sbStatus
            // 
            this.sbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slMensagemStatus,
            this.spbBarraProgresso,
            this.slNomeFormulario,
            this.slAmbiente});
            this.sbStatus.Location = new System.Drawing.Point(0, 339);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(644, 22);
            this.sbStatus.SizingGrip = false;
            this.sbStatus.TabIndex = 5;
            this.sbStatus.Text = "statusStrip1";
            // 
            // slAmbiente
            // 
            this.slAmbiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.slAmbiente.Name = "slAmbiente";
            this.slAmbiente.Size = new System.Drawing.Size(59, 17);
            this.slAmbiente.Text = "Ambiente";
            // 
            // frmBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 361);
            this.Controls.Add(this.pnlConteudo);
            this.Controls.Add(this.lblFuncao);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sbStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBase";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmBase_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.sbStatus.ResumeLayout(false);
            this.sbStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.Panel pnlConteudo;
        private System.Windows.Forms.Label lblFuncao;
        private System.Windows.Forms.PictureBox pictureBox1;
        protected internal System.Windows.Forms.ToolStripStatusLabel slMensagemStatus;
        private System.Windows.Forms.ToolStripProgressBar spbBarraProgresso;
        private System.Windows.Forms.ToolStripStatusLabel slNomeFormulario;
        protected internal System.Windows.Forms.StatusStrip sbStatus;
        private System.Windows.Forms.ToolStripStatusLabel slAmbiente;
    }
}