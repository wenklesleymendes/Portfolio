
namespace Formularios.Telas._8_Controles
{
    partial class ucOperadorCard
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
            this.chbSelecionarPaciente = new System.Windows.Forms.CheckBox();
            this.lbSobrenome = new System.Windows.Forms.Label();
            this.lbNascimento = new System.Windows.Forms.Label();
            this.lbCadNome = new System.Windows.Forms.Label();
            this.pbFotoPaciente = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).BeginInit();
            this.SuspendLayout();
            // 
            // chbSelecionarPaciente
            // 
            this.chbSelecionarPaciente.AutoSize = true;
            this.chbSelecionarPaciente.Location = new System.Drawing.Point(9, 7);
            this.chbSelecionarPaciente.Name = "chbSelecionarPaciente";
            this.chbSelecionarPaciente.Size = new System.Drawing.Size(15, 14);
            this.chbSelecionarPaciente.TabIndex = 29;
            this.chbSelecionarPaciente.UseVisualStyleBackColor = true;
            // 
            // lbSobrenome
            // 
            this.lbSobrenome.AutoSize = true;
            this.lbSobrenome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSobrenome.ForeColor = System.Drawing.Color.Black;
            this.lbSobrenome.Location = new System.Drawing.Point(74, 106);
            this.lbSobrenome.Name = "lbSobrenome";
            this.lbSobrenome.Size = new System.Drawing.Size(65, 13);
            this.lbSobrenome.TabIndex = 28;
            this.lbSobrenome.Text = "Whatshapp:";
            // 
            // lbNascimento
            // 
            this.lbNascimento.AutoSize = true;
            this.lbNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNascimento.ForeColor = System.Drawing.Color.Black;
            this.lbNascimento.Location = new System.Drawing.Point(74, 68);
            this.lbNascimento.Name = "lbNascimento";
            this.lbNascimento.Size = new System.Drawing.Size(66, 13);
            this.lbNascimento.TabIndex = 27;
            this.lbNascimento.Text = "Nascimento:";
            // 
            // lbCadNome
            // 
            this.lbCadNome.AutoSize = true;
            this.lbCadNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCadNome.ForeColor = System.Drawing.Color.Black;
            this.lbCadNome.Location = new System.Drawing.Point(74, 33);
            this.lbCadNome.Name = "lbCadNome";
            this.lbCadNome.Size = new System.Drawing.Size(38, 13);
            this.lbCadNome.TabIndex = 26;
            this.lbCadNome.Text = "Nome:";
            // 
            // pbFotoPaciente
            // 
            this.pbFotoPaciente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFotoPaciente.Location = new System.Drawing.Point(9, 30);
            this.pbFotoPaciente.Name = "pbFotoPaciente";
            this.pbFotoPaciente.Size = new System.Drawing.Size(59, 92);
            this.pbFotoPaciente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFotoPaciente.TabIndex = 25;
            this.pbFotoPaciente.TabStop = false;
            // 
            // ucOperadorCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbSelecionarPaciente);
            this.Controls.Add(this.lbSobrenome);
            this.Controls.Add(this.lbNascimento);
            this.Controls.Add(this.lbCadNome);
            this.Controls.Add(this.pbFotoPaciente);
            this.Name = "ucOperadorCard";
            this.Size = new System.Drawing.Size(250, 131);
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbSelecionarPaciente;
        private System.Windows.Forms.Label lbSobrenome;
        private System.Windows.Forms.Label lbNascimento;
        private System.Windows.Forms.Label lbCadNome;
        private System.Windows.Forms.PictureBox pbFotoPaciente;
    }
}
