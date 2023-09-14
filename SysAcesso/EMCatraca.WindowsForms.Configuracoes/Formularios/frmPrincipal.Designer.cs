namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.tsBarraFerramentas = new System.Windows.Forms.ToolStrip();
            this.tlsConfiguracao = new System.Windows.Forms.ToolStripButton();
            this.tlsAcesso = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsSair = new System.Windows.Forms.ToolStripButton();
            this.tsBarraFerramentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsBarraFerramentas
            // 
            this.tsBarraFerramentas.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBarraFerramentas.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsBarraFerramentas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsConfiguracao,
            this.tlsAcesso,
            this.toolStripSeparator1,
            this.tlsSair});
            this.tsBarraFerramentas.Location = new System.Drawing.Point(0, 0);
            this.tsBarraFerramentas.Name = "tsBarraFerramentas";
            this.tsBarraFerramentas.Size = new System.Drawing.Size(1067, 39);
            this.tsBarraFerramentas.TabIndex = 0;
            this.tsBarraFerramentas.Text = "toolStrip1";
            // 
            // tlsConfiguracao
            // 
            this.tlsConfiguracao.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsConfiguracao.Image = ((System.Drawing.Image)(resources.GetObject("tlsConfiguracao.Image")));
            this.tlsConfiguracao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsConfiguracao.Name = "tlsConfiguracao";
            this.tlsConfiguracao.Size = new System.Drawing.Size(36, 36);
            this.tlsConfiguracao.Text = "toolStripButton1";
            this.tlsConfiguracao.ToolTipText = "Configurações de Acesso";
            this.tlsConfiguracao.Click += new System.EventHandler(this.TlsConfiguracao_Click);
            // 
            // tlsAcesso
            // 
            this.tlsAcesso.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsAcesso.Image = ((System.Drawing.Image)(resources.GetObject("tlsAcesso.Image")));
            this.tlsAcesso.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsAcesso.Name = "tlsAcesso";
            this.tlsAcesso.Size = new System.Drawing.Size(36, 36);
            this.tlsAcesso.Text = "toolStripButton2";
            this.tlsAcesso.ToolTipText = "Liberação de Acesso";
            this.tlsAcesso.Click += new System.EventHandler(this.TlsAcesso_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsSair
            // 
            this.tlsSair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsSair.Image = ((System.Drawing.Image)(resources.GetObject("tlsSair.Image")));
            this.tlsSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsSair.Name = "tlsSair";
            this.tlsSair.Size = new System.Drawing.Size(36, 36);
            this.tlsSair.Text = "toolStripButton3";
            this.tlsSair.ToolTipText = "Sair do Sistema";
            this.tlsSair.Click += new System.EventHandler(this.TlsSair_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tsBarraFerramentas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPrincipal";
            this.Text = "Escolar Manager - Controle de Acesso";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tsBarraFerramentas.ResumeLayout(false);
            this.tsBarraFerramentas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsBarraFerramentas;
        private System.Windows.Forms.ToolStripButton tlsConfiguracao;
        private System.Windows.Forms.ToolStripButton tlsAcesso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlsSair;
    }
}