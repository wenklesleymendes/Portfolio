
namespace MdPaciente.Visao
{
    partial class frmMainMdPaciente
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
            this.pnMainPacienteUm = new System.Windows.Forms.Panel();
            this.pnMainPacienteDois = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnMainPacienteUm
            // 
            this.pnMainPacienteUm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnMainPacienteUm.Location = new System.Drawing.Point(0, 0);
            this.pnMainPacienteUm.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainPacienteUm.Name = "pnMainPacienteUm";
            this.pnMainPacienteUm.Size = new System.Drawing.Size(571, 450);
            this.pnMainPacienteUm.TabIndex = 0;
            // 
            // pnMainPacienteDois
            // 
            this.pnMainPacienteDois.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMainPacienteDois.Location = new System.Drawing.Point(571, 0);
            this.pnMainPacienteDois.Margin = new System.Windows.Forms.Padding(0);
            this.pnMainPacienteDois.Name = "pnMainPacienteDois";
            this.pnMainPacienteDois.Size = new System.Drawing.Size(229, 450);
            this.pnMainPacienteDois.TabIndex = 1;
            // 
            // frmMainPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnMainPacienteDois);
            this.Controls.Add(this.pnMainPacienteUm);
            this.Name = "frmMainPaciente";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMainPaciente_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMainPacienteUm;
        private System.Windows.Forms.Panel pnMainPacienteDois;
    }
}