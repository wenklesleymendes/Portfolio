
namespace MdPaciente.Visao.Telas._2.Pacientes
{
    partial class frmExecoes
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
            this.pbInformacoes = new System.Windows.Forms.PictureBox();
            this.txtTextoInformacoes = new System.Windows.Forms.TextBox();
            this.pnControles = new System.Windows.Forms.Panel();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbInformacoes)).BeginInit();
            this.pnControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // pbInformacoes
            // 
            this.pbInformacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInformacoes.Image = global::MdPaciente.Properties.Resources.Informacoes;
            this.pbInformacoes.Location = new System.Drawing.Point(12, 56);
            this.pbInformacoes.Name = "pbInformacoes";
            this.pbInformacoes.Size = new System.Drawing.Size(428, 145);
            this.pbInformacoes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInformacoes.TabIndex = 0;
            this.pbInformacoes.TabStop = false;
            // 
            // txtTextoInformacoes
            // 
            this.txtTextoInformacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextoInformacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTextoInformacoes.Enabled = false;
            this.txtTextoInformacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextoInformacoes.Location = new System.Drawing.Point(12, 207);
            this.txtTextoInformacoes.Multiline = true;
            this.txtTextoInformacoes.Name = "txtTextoInformacoes";
            this.txtTextoInformacoes.Size = new System.Drawing.Size(428, 116);
            this.txtTextoInformacoes.TabIndex = 2;
            this.txtTextoInformacoes.Text = "         ";
            this.txtTextoInformacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnControles
            // 
            this.pnControles.BackColor = System.Drawing.Color.White;
            this.pnControles.Controls.Add(this.pbAdd);
            this.pnControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnControles.Location = new System.Drawing.Point(0, 0);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(452, 44);
            this.pnControles.TabIndex = 3;
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.White;
            this.pbAdd.Image = global::MdPaciente.Properties.Resources.addCadastro_32px;
            this.pbAdd.Location = new System.Drawing.Point(202, 0);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(42, 44);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAdd.TabIndex = 3;
            this.pbAdd.TabStop = false;
            this.pbAdd.Click += new System.EventHandler(this.pbAdd_Click);
            // 
            // frmExecoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 338);
            this.Controls.Add(this.pnControles);
            this.Controls.Add(this.txtTextoInformacoes);
            this.Controls.Add(this.pbInformacoes);
            this.Name = "frmExecoes";
            this.Text = "frmExecoes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbInformacoes)).EndInit();
            this.pnControles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbInformacoes;
        private System.Windows.Forms.TextBox txtTextoInformacoes;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.PictureBox pbAdd;
    }
}