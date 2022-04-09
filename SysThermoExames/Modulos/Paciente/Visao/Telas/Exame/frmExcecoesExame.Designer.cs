
namespace MdPaciente.Visao.Telas.Exame
{
    partial class frmExcecoesExame
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
            this.pnControles = new System.Windows.Forms.Panel();
            this.pbInformacoes = new System.Windows.Forms.PictureBox();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            this.txtTextoInformacoes = new System.Windows.Forms.TextBox();
            this.pnControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInformacoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.BackColor = System.Drawing.Color.White;
            this.pnControles.Controls.Add(this.pbAdd);
            this.pnControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnControles.Location = new System.Drawing.Point(0, 0);
            this.pnControles.Margin = new System.Windows.Forms.Padding(4);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(800, 54);
            this.pnControles.TabIndex = 4;
            // 
            // pbInformacoes
            // 
            this.pbInformacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbInformacoes.Image = global::MdPaciente.Properties.Resources.Informacoes;
            this.pbInformacoes.Location = new System.Drawing.Point(98, 62);
            this.pbInformacoes.Margin = new System.Windows.Forms.Padding(4);
            this.pbInformacoes.Name = "pbInformacoes";
            this.pbInformacoes.Size = new System.Drawing.Size(571, 178);
            this.pbInformacoes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInformacoes.TabIndex = 5;
            this.pbInformacoes.TabStop = false;
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.White;
            this.pbAdd.Image = global::MdPaciente.Properties.Resources.addCadastro_32px;
            this.pbAdd.Location = new System.Drawing.Point(355, 0);
            this.pbAdd.Margin = new System.Windows.Forms.Padding(4);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(56, 54);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAdd.TabIndex = 3;
            this.pbAdd.TabStop = false;
            // 
            // txtTextoInformacoes
            // 
            this.txtTextoInformacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextoInformacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTextoInformacoes.Enabled = false;
            this.txtTextoInformacoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextoInformacoes.Location = new System.Drawing.Point(98, 248);
            this.txtTextoInformacoes.Margin = new System.Windows.Forms.Padding(4);
            this.txtTextoInformacoes.Multiline = true;
            this.txtTextoInformacoes.Name = "txtTextoInformacoes";
            this.txtTextoInformacoes.Size = new System.Drawing.Size(571, 143);
            this.txtTextoInformacoes.TabIndex = 6;
            this.txtTextoInformacoes.Text = "         ";
            this.txtTextoInformacoes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmExcecoesExame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTextoInformacoes);
            this.Controls.Add(this.pbInformacoes);
            this.Controls.Add(this.pnControles);
            this.Name = "frmExcecoesExame";
            this.Text = "frmExcecoesExame";
            this.pnControles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbInformacoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.PictureBox pbAdd;
        private System.Windows.Forms.PictureBox pbInformacoes;
        private System.Windows.Forms.TextBox txtTextoInformacoes;
    }
}