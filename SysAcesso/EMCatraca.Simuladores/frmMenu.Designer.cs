namespace EMCatraca.Simuladores
{
    partial class frmMenuDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuDebug));
            this.btnNaoGira = new System.Windows.Forms.Button();
            this.btnSimuladorTcip = new System.Windows.Forms.Button();
            this.btnExterno = new System.Windows.Forms.Button();
            this.lbSimuladores = new System.Windows.Forms.Label();
            this.pnSimuladores = new System.Windows.Forms.Panel();
            this.btnConfigurarAcesso = new System.Windows.Forms.Button();
            this.btnSimuladorCatracaTopData = new System.Windows.Forms.Button();
            this.pnSimuladores.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNaoGira
            // 
            this.btnNaoGira.BackColor = System.Drawing.Color.White;
            this.btnNaoGira.ForeColor = System.Drawing.Color.Black;
            this.btnNaoGira.Location = new System.Drawing.Point(7, 82);
            this.btnNaoGira.Margin = new System.Windows.Forms.Padding(0);
            this.btnNaoGira.Name = "btnNaoGira";
            this.btnNaoGira.Size = new System.Drawing.Size(118, 38);
            this.btnNaoGira.TabIndex = 67;
            this.btnNaoGira.Text = "Simulador Monitor\r\nTCIP\r\n";
            this.btnNaoGira.UseVisualStyleBackColor = false;
            // 
            // btnSimuladorTcip
            // 
            this.btnSimuladorTcip.BackColor = System.Drawing.Color.White;
            this.btnSimuladorTcip.ForeColor = System.Drawing.Color.Black;
            this.btnSimuladorTcip.Location = new System.Drawing.Point(7, 36);
            this.btnSimuladorTcip.Margin = new System.Windows.Forms.Padding(0);
            this.btnSimuladorTcip.Name = "btnSimuladorTcip";
            this.btnSimuladorTcip.Size = new System.Drawing.Size(118, 38);
            this.btnSimuladorTcip.TabIndex = 65;
            this.btnSimuladorTcip.Text = "Simulador TCIP \r\nHenry";
            this.btnSimuladorTcip.UseVisualStyleBackColor = false;
            this.btnSimuladorTcip.Click += new System.EventHandler(this.btnSimuladorTcip_Click);
            // 
            // btnExterno
            // 
            this.btnExterno.BackColor = System.Drawing.Color.White;
            this.btnExterno.ForeColor = System.Drawing.Color.Black;
            this.btnExterno.Location = new System.Drawing.Point(130, 36);
            this.btnExterno.Margin = new System.Windows.Forms.Padding(0);
            this.btnExterno.Name = "btnExterno";
            this.btnExterno.Size = new System.Drawing.Size(118, 38);
            this.btnExterno.TabIndex = 68;
            this.btnExterno.Text = "Simulador Externo\r\nNeocoros";
            this.btnExterno.UseVisualStyleBackColor = false;
            this.btnExterno.Click += new System.EventHandler(this.btnExterno_Click);
            // 
            // lbSimuladores
            // 
            this.lbSimuladores.AutoSize = true;
            this.lbSimuladores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSimuladores.Location = new System.Drawing.Point(84, 8);
            this.lbSimuladores.Name = "lbSimuladores";
            this.lbSimuladores.Size = new System.Drawing.Size(97, 20);
            this.lbSimuladores.TabIndex = 69;
            this.lbSimuladores.Text = "Simuladores";
            // 
            // pnSimuladores
            // 
            this.pnSimuladores.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnSimuladores.Controls.Add(this.btnSimuladorCatracaTopData);
            this.pnSimuladores.Controls.Add(this.btnConfigurarAcesso);
            this.pnSimuladores.Controls.Add(this.lbSimuladores);
            this.pnSimuladores.Controls.Add(this.btnNaoGira);
            this.pnSimuladores.Controls.Add(this.btnSimuladorTcip);
            this.pnSimuladores.Controls.Add(this.btnExterno);
            this.pnSimuladores.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSimuladores.Location = new System.Drawing.Point(0, 0);
            this.pnSimuladores.Name = "pnSimuladores";
            this.pnSimuladores.Size = new System.Drawing.Size(387, 126);
            this.pnSimuladores.TabIndex = 71;
            // 
            // btnConfigurarAcesso
            // 
            this.btnConfigurarAcesso.BackColor = System.Drawing.Color.White;
            this.btnConfigurarAcesso.ForeColor = System.Drawing.Color.Black;
            this.btnConfigurarAcesso.Location = new System.Drawing.Point(130, 81);
            this.btnConfigurarAcesso.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfigurarAcesso.Name = "btnConfigurarAcesso";
            this.btnConfigurarAcesso.Size = new System.Drawing.Size(118, 38);
            this.btnConfigurarAcesso.TabIndex = 70;
            this.btnConfigurarAcesso.Text = "Configurar Acesso";
            this.btnConfigurarAcesso.UseVisualStyleBackColor = false;
            this.btnConfigurarAcesso.Click += new System.EventHandler(this.btnConfigurarAcesso_Click);
            // 
            // btnSimuladorCatracaTopData
            // 
            this.btnSimuladorCatracaTopData.BackColor = System.Drawing.Color.White;
            this.btnSimuladorCatracaTopData.ForeColor = System.Drawing.Color.Black;
            this.btnSimuladorCatracaTopData.Location = new System.Drawing.Point(257, 36);
            this.btnSimuladorCatracaTopData.Margin = new System.Windows.Forms.Padding(0);
            this.btnSimuladorCatracaTopData.Name = "btnSimuladorCatracaTopData";
            this.btnSimuladorCatracaTopData.Size = new System.Drawing.Size(119, 38);
            this.btnSimuladorCatracaTopData.TabIndex = 72;
            this.btnSimuladorCatracaTopData.Text = "Simulador Dispositivo TopData";
            this.btnSimuladorCatracaTopData.UseVisualStyleBackColor = false;
            this.btnSimuladorCatracaTopData.Click += new System.EventHandler(this.btnSimuladorCatracaTopData_Click);
            // 
            // frmMenuDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 126);
            this.Controls.Add(this.pnSimuladores);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenuDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Debug";
            this.pnSimuladores.ResumeLayout(false);
            this.pnSimuladores.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnNaoGira;
        private System.Windows.Forms.Button btnSimuladorTcip;
        private System.Windows.Forms.Button btnExterno;
        private System.Windows.Forms.Label lbSimuladores;
        private System.Windows.Forms.Panel pnSimuladores;
        private System.Windows.Forms.Button btnConfigurarAcesso;
        private System.Windows.Forms.Button btnSimuladorCatracaTopData;
    }
}