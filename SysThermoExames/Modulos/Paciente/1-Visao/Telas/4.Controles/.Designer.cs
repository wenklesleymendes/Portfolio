
namespace MdPaciente._1_Visao
{
    partial class ucPacienteCard
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbSobrenome = new System.Windows.Forms.Label();
            this.lbNascimento = new System.Windows.Forms.Label();
            this.lbCadNome = new System.Windows.Forms.Label();
            this.pbFotoPaciente = new System.Windows.Forms.PictureBox();
            this.chbSelecionarPaciente = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSobrenome
            // 
            this.lbSobrenome.AutoSize = true;
            this.lbSobrenome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSobrenome.ForeColor = System.Drawing.Color.Black;
            this.lbSobrenome.Location = new System.Drawing.Point(70, 105);
            this.lbSobrenome.Name = "lbSobrenome";
            this.lbSobrenome.Size = new System.Drawing.Size(65, 13);
            this.lbSobrenome.TabIndex = 23;
            this.lbSobrenome.Text = "Whatshapp:";
            // 
            // lbNascimento
            // 
            this.lbNascimento.AutoSize = true;
            this.lbNascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNascimento.ForeColor = System.Drawing.Color.Black;
            this.lbNascimento.Location = new System.Drawing.Point(70, 67);
            this.lbNascimento.Name = "lbNascimento";
            this.lbNascimento.Size = new System.Drawing.Size(66, 13);
            this.lbNascimento.TabIndex = 22;
            this.lbNascimento.Text = "Nascimento:";
            // 
            // lbCadNome
            // 
            this.lbCadNome.AutoSize = true;
            this.lbCadNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCadNome.ForeColor = System.Drawing.Color.Black;
            this.lbCadNome.Location = new System.Drawing.Point(70, 32);
            this.lbCadNome.Name = "lbCadNome";
            this.lbCadNome.Size = new System.Drawing.Size(38, 13);
            this.lbCadNome.TabIndex = 21;
            this.lbCadNome.Text = "Nome:";
            // 
            // pbFotoPaciente
            // 
            this.pbFotoPaciente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbFotoPaciente.Image = global::MdPaciente.Properties.Resources.semImagem;
            this.pbFotoPaciente.Location = new System.Drawing.Point(5, 29);
            this.pbFotoPaciente.Name = "pbFotoPaciente";
            this.pbFotoPaciente.Size = new System.Drawing.Size(59, 92);
            this.pbFotoPaciente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFotoPaciente.TabIndex = 12;
            this.pbFotoPaciente.TabStop = false;
            // 
            // chbSelecionarPaciente
            // 
            this.chbSelecionarPaciente.AutoSize = true;
            this.chbSelecionarPaciente.Location = new System.Drawing.Point(5, 6);
            this.chbSelecionarPaciente.Name = "chbSelecionarPaciente";
            this.chbSelecionarPaciente.Size = new System.Drawing.Size(15, 14);
            this.chbSelecionarPaciente.TabIndex = 24;
            this.chbSelecionarPaciente.UseVisualStyleBackColor = true;
            // 
            // ucPacienteCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.chbSelecionarPaciente);
            this.Controls.Add(this.lbSobrenome);
            this.Controls.Add(this.lbNascimento);
            this.Controls.Add(this.lbCadNome);
            this.Controls.Add(this.pbFotoPaciente);
            this.Name = "ucPacienteCard";
            this.Size = new System.Drawing.Size(250, 131);
            this.Click += new System.EventHandler(this.ucPacienteCard_Click);
            this.DoubleClick += new System.EventHandler(this.ucPacienteCard_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFotoPaciente;
        private System.Windows.Forms.Label lbSobrenome;
        private System.Windows.Forms.Label lbNascimento;
        private System.Windows.Forms.Label lbCadNome;
        private System.Windows.Forms.CheckBox chbSelecionarPaciente;
    }
}
