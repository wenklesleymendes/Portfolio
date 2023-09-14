
namespace Formularios.Telas._2_Principal
{
    partial class frmMainOperador
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
            this.pnMainOperadorUm = new System.Windows.Forms.Panel();
            this.pnMainOperadorDois = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnMainOperadorUm
            // 
            this.pnMainOperadorUm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnMainOperadorUm.Location = new System.Drawing.Point(0, 0);
            this.pnMainOperadorUm.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainOperadorUm.Name = "pnMainOperadorUm";
            this.pnMainOperadorUm.Size = new System.Drawing.Size(571, 450);
            this.pnMainOperadorUm.TabIndex = 0;
            // 
            // pnMainOperadorDois
            // 
            this.pnMainOperadorDois.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMainOperadorDois.Location = new System.Drawing.Point(571, 0);
            this.pnMainOperadorDois.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainOperadorDois.Name = "pnMainOperadorDois";
            this.pnMainOperadorDois.Size = new System.Drawing.Size(229, 450);
            this.pnMainOperadorDois.TabIndex = 1;
            // 
            // frmMainOperador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnMainOperadorDois);
            this.Controls.Add(this.pnMainOperadorUm);
            this.Name = "frmMainOperador";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMainPaciente_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnMainOperadorUm;
        public System.Windows.Forms.Panel pnMainOperadorDois;
    }
}