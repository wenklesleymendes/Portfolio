namespace EasyInnerSDK.UI.FrmSobre
{
    partial class FrmSobre
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
            System.Windows.Forms.GroupBox groupBox1;
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tbnSair = new System.Windows.Forms.Button();
            this.lblData = new System.Windows.Forms.Label();
            this.lblVersão = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.label1);
            groupBox1.Controls.Add(this.listBox1);
            groupBox1.Location = new System.Drawing.Point(12, 98);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(452, 351);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Conteúdo:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "V-1.0.1",
            "- Exemplos de Acionamentos Diretos.",
            "- Exemplos de Configuração de Relógio.",
            "- Exemplo de Configuração de InnerBIO. (Menu Online\\InnerBIO Online)",
            "- Exemplo de Manutenção de Usuários em InnerBIO. (Menu Online\\InnerBIO Online)",
            "- Exemplo Online InnerBIO/ Inner Net",
            "",
            "V-1.0.2",
            "-Adicionado Verificação de contagem de Ping Online , reconecta caso falhe 3 vezes" +
                ".",
            "-Alterado Passos da Maquina de estado.",
            "",
            "V-1.0.3",
            "-Removido Mensagem Temporária Online, que foi descontinnuada do SDK.",
            "",
            "V- 1.0.4",
            "-Alterado forma de Reconexão Serial.",
            "",
            "V-1.0.5",
            "-Alterado métodos de Conexão para conectar em qualquer número de porta COM.",
            "",
            "V-1.0.6",
            "-Alterado maquina de estados para catraca Serial.",
            "",
            "V-2.0",
            "-Revisão fontes.",
            "",
            "V-2.02",
            "-Multi - Inners.",
            "",
            "V-2.04",
            "-Compatibilidade total com Inner Acesso.",
            "",
            "V-2.06",
            "-Alterado forma de conexão",
            "",
            "V- 3.0.1",
            "-Usando nova Dll Inner.",
            "-Implementado o comando \"IncluirUsuarioListaBioInnerAcesso\"",
            "-Implementado o comando \"EnviarListaBioInnerAcesso\" lista que suporta até 8000 us" +
                "uários.",
            "-Implementado os comandos \"LigarLedVerde\" e \"DesligarLedVerde\".",
            "-Implementado ajustes para usar placa fim 6060."});
            this.listBox1.Location = new System.Drawing.Point(6, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(440, 303);
            this.listBox1.TabIndex = 0;
            // 
            // tbnSair
            // 
            this.tbnSair.Location = new System.Drawing.Point(389, 455);
            this.tbnSair.Name = "tbnSair";
            this.tbnSair.Size = new System.Drawing.Size(75, 23);
            this.tbnSair.TabIndex = 9;
            this.tbnSair.Text = "Sair";
            this.tbnSair.UseVisualStyleBackColor = true;
            this.tbnSair.Click += new System.EventHandler(this.tbnSair_Click);
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(139, 79);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(48, 13);
            this.lblData.TabIndex = 8;
            this.lblData.Text = "03/2016";
            this.lblData.Click += new System.EventHandler(this.lblData_Click);
            // 
            // lblVersão
            // 
            this.lblVersão.AutoSize = true;
            this.lblVersão.Location = new System.Drawing.Point(139, 43);
            this.lblVersão.Name = "lblVersão";
            this.lblVersão.Size = new System.Drawing.Size(70, 13);
            this.lblVersão.TabIndex = 7;
            this.lblVersão.Text = "Versão: 3.0.1";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(139, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(114, 17);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "LAB EasyInner";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::EasyInnerSDK.Properties.Resources.TopDataLogo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 80);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FrmSobre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 490);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.tbnSair);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblVersão);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSobre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobre";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSobre_FormClosed);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button tbnSair;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblVersão;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}