
namespace MdPaciente.Visao
{
    partial class frmCard
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
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pbEditar = new System.Windows.Forms.PictureBox();
            this.pbRemove = new System.Windows.Forms.PictureBox();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.pbPesquisa = new System.Windows.Forms.PictureBox();
            this.flpCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).BeginInit();
            this.SuspendLayout();
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
            this.pnControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnControles.Location = new System.Drawing.Point(0, 0);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(673, 44);
            this.pnControles.TabIndex = 0;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox9.Image = global::MdPaciente.Properties.Resources.fecharTela32px;
            this.pictureBox9.Location = new System.Drawing.Point(408, 0);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(42, 44);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox9.TabIndex = 5;
            this.pictureBox9.TabStop = false;
            // 
            // pbEditar
            // 
            this.pbEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbEditar.Image = global::MdPaciente.Properties.Resources.editarCadastro32px;
            this.pbEditar.Location = new System.Drawing.Point(354, 0);
            this.pbEditar.Name = "pbEditar";
            this.pbEditar.Size = new System.Drawing.Size(42, 44);
            this.pbEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbEditar.TabIndex = 2;
            this.pbEditar.TabStop = false;
            // 
            // pbRemove
            // 
            this.pbRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbRemove.Image = global::MdPaciente.Properties.Resources.removerCadastro32px;
            this.pbRemove.Location = new System.Drawing.Point(298, 0);
            this.pbRemove.Name = "pbRemove";
            this.pbRemove.Size = new System.Drawing.Size(42, 44);
            this.pbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRemove.TabIndex = 4;
            this.pbRemove.TabStop = false;
            this.pbRemove.Click += new System.EventHandler(this.pbRemove_Click);
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbAdd.Image = global::MdPaciente.Properties.Resources.addCadastro_32px;
            this.pbAdd.Location = new System.Drawing.Point(242, 0);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(42, 44);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAdd.TabIndex = 3;
            this.pbAdd.TabStop = false;
            this.pbAdd.Click += new System.EventHandler(this.pbAdd_Click);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.Location = new System.Drawing.Point(45, 13);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(186, 20);
            this.txtPesquisa.TabIndex = 2;
            // 
            // pbPesquisa
            // 
            this.pbPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pbPesquisa.Image = global::MdPaciente.Properties.Resources.pesquisa_32px;
            this.pbPesquisa.Location = new System.Drawing.Point(0, 0);
            this.pbPesquisa.Name = "pbPesquisa";
            this.pbPesquisa.Size = new System.Drawing.Size(42, 44);
            this.pbPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPesquisa.TabIndex = 1;
            this.pbPesquisa.TabStop = false;
            // 
            // flpCards
            // 
            this.flpCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCards.Location = new System.Drawing.Point(0, 44);
            this.flpCards.Name = "flpCards";
            this.flpCards.Size = new System.Drawing.Size(673, 425);
            this.flpCards.TabIndex = 1;
            // 
            // frmCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(673, 469);
            this.Controls.Add(this.flpCards);
            this.Controls.Add(this.pnControles);
            this.Name = "frmCard";
            this.Text = "frmCard";
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

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.PictureBox pbPesquisa;
        public System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.PictureBox pbEditar;
        private System.Windows.Forms.PictureBox pbRemove;
        private System.Windows.Forms.PictureBox pbAdd;
        private System.Windows.Forms.FlowLayoutPanel flpCards;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}