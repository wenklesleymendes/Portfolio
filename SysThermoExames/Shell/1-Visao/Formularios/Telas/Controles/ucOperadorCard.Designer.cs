
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
            this.chbSelecionarOperador = new System.Windows.Forms.CheckBox();
            this.lbGrupo = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.lbCadNome = new System.Windows.Forms.Label();
            this.pbFotoPaciente = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).BeginInit();
            this.SuspendLayout();
            // 
            // chbSelecionarOperador
            // 
            this.chbSelecionarOperador.AutoSize = true;
            this.chbSelecionarOperador.Location = new System.Drawing.Point(9, 7);
            this.chbSelecionarOperador.Name = "chbSelecionarOperador";
            this.chbSelecionarOperador.Size = new System.Drawing.Size(15, 14);
            this.chbSelecionarOperador.TabIndex = 29;
            this.chbSelecionarOperador.UseVisualStyleBackColor = true;
            this.chbSelecionarOperador.CheckedChanged += new System.EventHandler(this.chbSelecionarOperador_CheckedChanged);
            // 
            // lbGrupo
            // 
            this.lbGrupo.AutoSize = true;
            this.lbGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrupo.ForeColor = System.Drawing.Color.Black;
            this.lbGrupo.Location = new System.Drawing.Point(74, 106);
            this.lbGrupo.Name = "lbGrupo";
            this.lbGrupo.Size = new System.Drawing.Size(39, 13);
            this.lbGrupo.TabIndex = 28;
            this.lbGrupo.Text = "Grupo:";
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.ForeColor = System.Drawing.Color.Black;
            this.lbLogin.Location = new System.Drawing.Point(74, 68);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(36, 13);
            this.lbLogin.TabIndex = 27;
            this.lbLogin.Text = "Login:";
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
            this.Controls.Add(this.chbSelecionarOperador);
            this.Controls.Add(this.lbGrupo);
            this.Controls.Add(this.lbLogin);
            this.Controls.Add(this.lbCadNome);
            this.Controls.Add(this.pbFotoPaciente);
            this.Name = "ucOperadorCard";
            this.Size = new System.Drawing.Size(250, 131);
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPaciente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbSelecionarOperador;
        private System.Windows.Forms.Label lbGrupo;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Label lbCadNome;
        private System.Windows.Forms.PictureBox pbFotoPaciente;
    }
}
