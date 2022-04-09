
namespace MdPaciente.Visao.Telas._3.Exames
{
    partial class frmCardExame
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
            this.flpCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnControles = new System.Windows.Forms.Panel();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pbEditar = new System.Windows.Forms.PictureBox();
            this.pbRemove = new System.Windows.Forms.PictureBox();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.pbPesquisa = new System.Windows.Forms.PictureBox();
            this.flpCards.SuspendLayout();
            this.pnControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).BeginInit();
            this.SuspendLayout();
            // 
            // flpCards
            // 
            this.flpCards.Controls.Add(this.pnControles);
            this.flpCards.Location = new System.Drawing.Point(-2, 0);
            this.flpCards.Margin = new System.Windows.Forms.Padding(4);
            this.flpCards.Name = "flpCards";
            this.flpCards.Size = new System.Drawing.Size(1106, 626);
            this.flpCards.TabIndex = 2;
            // 
            // pnControles
            // 
            this.pnControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnControles.Controls.Add(this.pictureBox9);
            this.pnControles.Controls.Add(this.pbEditar);
            this.pnControles.Controls.Add(this.pbRemove);
            this.pnControles.Controls.Add(this.pbAdd);
            this.pnControles.Controls.Add(this.txtPesquisa);
            this.pnControles.Controls.Add(this.pbPesquisa);
            this.pnControles.Location = new System.Drawing.Point(4, 4);
            this.pnControles.Margin = new System.Windows.Forms.Padding(4);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(1121, 54);
            this.pnControles.TabIndex = 0;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox9.Image = global::MdPaciente.Properties.Resources.fecharTela32px;
            this.pictureBox9.Location = new System.Drawing.Point(544, 0);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(56, 54);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox9.TabIndex = 5;
            this.pictureBox9.TabStop = false;
            // 
            // pbEditar
            // 
            this.pbEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbEditar.Image = global::MdPaciente.Properties.Resources.editarCadastro32px;
            this.pbEditar.Location = new System.Drawing.Point(472, 0);
            this.pbEditar.Margin = new System.Windows.Forms.Padding(4);
            this.pbEditar.Name = "pbEditar";
            this.pbEditar.Size = new System.Drawing.Size(56, 54);
            this.pbEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbEditar.TabIndex = 2;
            this.pbEditar.TabStop = false;
            // 
            // pbRemove
            // 
            this.pbRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbRemove.Image = global::MdPaciente.Properties.Resources.removerCadastro32px;
            this.pbRemove.Location = new System.Drawing.Point(397, 0);
            this.pbRemove.Margin = new System.Windows.Forms.Padding(4);
            this.pbRemove.Name = "pbRemove";
            this.pbRemove.Size = new System.Drawing.Size(56, 54);
            this.pbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRemove.TabIndex = 4;
            this.pbRemove.TabStop = false;
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbAdd.Image = global::MdPaciente.Properties.Resources.addCadastro_32px;
            this.pbAdd.Location = new System.Drawing.Point(323, 0);
            this.pbAdd.Margin = new System.Windows.Forms.Padding(4);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(56, 54);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAdd.TabIndex = 3;
            this.pbAdd.TabStop = false;
            this.pbAdd.Click += new System.EventHandler(this.pbAdd_Click);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.Location = new System.Drawing.Point(60, 16);
            this.txtPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(247, 23);
            this.txtPesquisa.TabIndex = 2;
            // 
            // pbPesquisa
            // 
            this.pbPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbPesquisa.Image = global::MdPaciente.Properties.Resources.pesquisa_32px;
            this.pbPesquisa.Location = new System.Drawing.Point(0, 0);
            this.pbPesquisa.Margin = new System.Windows.Forms.Padding(4);
            this.pbPesquisa.Name = "pbPesquisa";
            this.pbPesquisa.Size = new System.Drawing.Size(56, 54);
            this.pbPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPesquisa.TabIndex = 1;
            this.pbPesquisa.TabStop = false;
            // 
            // frmCardExame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 625);
            this.Controls.Add(this.flpCards);
            this.Name = "frmCardExame";
            this.Text = "frmCardExame";
            this.flpCards.ResumeLayout(false);
            this.pnControles.ResumeLayout(false);
            this.pnControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpCards;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pbEditar;
        private System.Windows.Forms.PictureBox pbRemove;
        private System.Windows.Forms.PictureBox pbAdd;
        public System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.PictureBox pbPesquisa;
    }
}