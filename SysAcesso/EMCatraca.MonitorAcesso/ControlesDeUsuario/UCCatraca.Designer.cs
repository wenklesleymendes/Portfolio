namespace EMCatraca.MonitorAcesso.ControlesDeUsuario
{
    partial class ucCatraca
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
            this.lblCatraca = new System.Windows.Forms.Label();
            this.lblAcesso = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblComentario = new System.Windows.Forms.Label();
            this.pnlCatraca = new System.Windows.Forms.Panel();
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.imgFoto = new System.Windows.Forms.PictureBox();
            this.pnlCatraca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCatraca
            // 
            this.lblCatraca.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCatraca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCatraca.Font = new System.Drawing.Font("Candara", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatraca.Location = new System.Drawing.Point(0, 0);
            this.lblCatraca.Name = "lblCatraca";
            this.lblCatraca.Size = new System.Drawing.Size(424, 64);
            this.lblCatraca.TabIndex = 1;
            this.lblCatraca.Text = "Dispositivo 1";
            this.lblCatraca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAcesso
            // 
            this.lblAcesso.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAcesso.Font = new System.Drawing.Font("Candara", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcesso.ForeColor = System.Drawing.Color.Green;
            this.lblAcesso.Location = new System.Drawing.Point(0, 630);
            this.lblAcesso.Name = "lblAcesso";
            this.lblAcesso.Size = new System.Drawing.Size(424, 51);
            this.lblAcesso.TabIndex = 2;
            this.lblAcesso.Text = "Acesso liberado!";
            this.lblAcesso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNome
            // 
            this.lblNome.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNome.Font = new System.Drawing.Font("Candara", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.Color.Black;
            this.lblNome.Location = new System.Drawing.Point(0, 591);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(424, 39);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "Maria Clara de Almeida Souza e Silva";
            this.lblNome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblComentario
            // 
            this.lblComentario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblComentario.Font = new System.Drawing.Font("Candara", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentario.ForeColor = System.Drawing.Color.Sienna;
            this.lblComentario.Location = new System.Drawing.Point(0, 681);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(424, 39);
            this.lblComentario.TabIndex = 4;
            this.lblComentario.Text = "Apresente-se na secretaria!";
            this.lblComentario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCatraca
            // 
            this.pnlCatraca.Controls.Add(this.imgStatus);
            this.pnlCatraca.Controls.Add(this.lblCatraca);
            this.pnlCatraca.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCatraca.Location = new System.Drawing.Point(0, 0);
            this.pnlCatraca.Name = "pnlCatraca";
            this.pnlCatraca.Size = new System.Drawing.Size(424, 64);
            this.pnlCatraca.TabIndex = 6;
            // 
            // imgStatus
            // 
            this.imgStatus.BackColor = System.Drawing.Color.White;
            this.imgStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.imgStatus.Image = global::EMCatraca.MonitorAcesso.Properties.Resources.offline;
            this.imgStatus.Location = new System.Drawing.Point(360, 0);
            this.imgStatus.Margin = new System.Windows.Forms.Padding(0);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Size = new System.Drawing.Size(64, 64);
            this.imgStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgStatus.TabIndex = 2;
            this.imgStatus.TabStop = false;
            this.imgStatus.Visible = false;
            // 
            // imgFoto
            // 
            this.imgFoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgFoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgFoto.Image = global::EMCatraca.MonitorAcesso.Properties.Resources.semfoto;
            this.imgFoto.Location = new System.Drawing.Point(0, 64);
            this.imgFoto.Name = "imgFoto";
            this.imgFoto.Size = new System.Drawing.Size(424, 527);
            this.imgFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgFoto.TabIndex = 0;
            this.imgFoto.TabStop = false;
            // 
            // UCCatraca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.imgFoto);
            this.Controls.Add(this.pnlCatraca);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.lblAcesso);
            this.Controls.Add(this.lblComentario);
            this.Name = "UCCatraca";
            this.Size = new System.Drawing.Size(424, 720);
            this.pnlCatraca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgFoto;
        private System.Windows.Forms.Label lblCatraca;
        private System.Windows.Forms.Label lblAcesso;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Panel pnlCatraca;
        private System.Windows.Forms.PictureBox imgStatus;
    }
}
