namespace EMCatraca.Simuladores.ControliD.View
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.exemploDeConversãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acessoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catracaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirPortaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizarDataEHoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilidades = new System.Windows.Forms.ToolStripMenuItem();
            this.nomeExibiçãoDoAcessoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setarOfflineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setarOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formasdeconexao = new System.Windows.Forms.ToolStripMenuItem();
            this.logsDeAcessoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosCadastradosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultas = new System.Windows.Forms.ToolStripMenuItem();
            this.listarCartõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarUsuárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastros = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 517);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 37);
            this.button1.TabIndex = 23;
            this.button1.Text = "Limpar Logs";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // exemploDeConversãoToolStripMenuItem
            // 
            this.exemploDeConversãoToolStripMenuItem.Name = "exemploDeConversãoToolStripMenuItem";
            this.exemploDeConversãoToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.exemploDeConversãoToolStripMenuItem.Text = "Conversão Wiegand";
            // 
            // acessoToolStripMenuItem
            // 
            this.acessoToolStripMenuItem.Name = "acessoToolStripMenuItem";
            this.acessoToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.acessoToolStripMenuItem.Text = "Acesso";
            // 
            // catracaToolStripMenuItem
            // 
            this.catracaToolStripMenuItem.Name = "catracaToolStripMenuItem";
            this.catracaToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.catracaToolStripMenuItem.Text = "Dispositivo";
            // 
            // abrirPortaToolStripMenuItem
            // 
            this.abrirPortaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catracaToolStripMenuItem,
            this.acessoToolStripMenuItem});
            this.abrirPortaToolStripMenuItem.Name = "abrirPortaToolStripMenuItem";
            this.abrirPortaToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.abrirPortaToolStripMenuItem.Text = "Liberar Acesso";
            // 
            // atualizarDataEHoraToolStripMenuItem
            // 
            this.atualizarDataEHoraToolStripMenuItem.Name = "atualizarDataEHoraToolStripMenuItem";
            this.atualizarDataEHoraToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.atualizarDataEHoraToolStripMenuItem.Text = "Atualizar Data e Hora";
            // 
            // utilidades
            // 
            this.utilidades.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atualizarDataEHoraToolStripMenuItem,
            this.abrirPortaToolStripMenuItem,
            this.exemploDeConversãoToolStripMenuItem,
            this.nomeExibiçãoDoAcessoToolStripMenuItem});
            this.utilidades.Name = "utilidades";
            this.utilidades.Size = new System.Drawing.Size(71, 20);
            this.utilidades.Text = "Utilidades";
            // 
            // nomeExibiçãoDoAcessoToolStripMenuItem
            // 
            this.nomeExibiçãoDoAcessoToolStripMenuItem.Name = "nomeExibiçãoDoAcessoToolStripMenuItem";
            this.nomeExibiçãoDoAcessoToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.nomeExibiçãoDoAcessoToolStripMenuItem.Text = "Nome exibição do Acesso";
            // 
            // setarOfflineToolStripMenuItem
            // 
            this.setarOfflineToolStripMenuItem.Name = "setarOfflineToolStripMenuItem";
            this.setarOfflineToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.setarOfflineToolStripMenuItem.Text = "Setar Offline";
            // 
            // setarOnlineToolStripMenuItem
            // 
            this.setarOnlineToolStripMenuItem.Name = "setarOnlineToolStripMenuItem";
            this.setarOnlineToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.setarOnlineToolStripMenuItem.Text = "Setar Online";
            // 
            // formasdeconexao
            // 
            this.formasdeconexao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setarOnlineToolStripMenuItem,
            this.setarOfflineToolStripMenuItem});
            this.formasdeconexao.Name = "formasdeconexao";
            this.formasdeconexao.Size = new System.Drawing.Size(124, 20);
            this.formasdeconexao.Text = "Formas de Conexao";
            // 
            // logsDeAcessoToolStripMenuItem
            // 
            this.logsDeAcessoToolStripMenuItem.Name = "logsDeAcessoToolStripMenuItem";
            this.logsDeAcessoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.logsDeAcessoToolStripMenuItem.Text = "Listar Logs";
            // 
            // usuariosCadastradosToolStripMenuItem
            // 
            this.usuariosCadastradosToolStripMenuItem.Name = "usuariosCadastradosToolStripMenuItem";
            this.usuariosCadastradosToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.usuariosCadastradosToolStripMenuItem.Text = "Listar Usuarios";
            // 
            // consultas
            // 
            this.consultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuariosCadastradosToolStripMenuItem,
            this.logsDeAcessoToolStripMenuItem,
            this.listarCartõesToolStripMenuItem});
            this.consultas.Name = "consultas";
            this.consultas.Size = new System.Drawing.Size(71, 20);
            this.consultas.Text = "Consultas";
            // 
            // listarCartõesToolStripMenuItem
            // 
            this.listarCartõesToolStripMenuItem.Name = "listarCartõesToolStripMenuItem";
            this.listarCartõesToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.listarCartõesToolStripMenuItem.Text = "Listar Cartões";
            // 
            // terminalToolStripMenuItem
            // 
            this.terminalToolStripMenuItem.Name = "terminalToolStripMenuItem";
            this.terminalToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.terminalToolStripMenuItem.Text = "Terminal";
            // 
            // cadastrarUsuárioToolStripMenuItem
            // 
            this.cadastrarUsuárioToolStripMenuItem.Name = "cadastrarUsuárioToolStripMenuItem";
            this.cadastrarUsuárioToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.cadastrarUsuárioToolStripMenuItem.Text = "Usuário";
            // 
            // cadastros
            // 
            this.cadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarUsuárioToolStripMenuItem,
            this.terminalToolStripMenuItem});
            this.cadastros.Name = "cadastros";
            this.cadastros.Size = new System.Drawing.Size(71, 20);
            this.cadastros.Text = "Cadastros";
            // 
            // txtLogs
            // 
            this.txtLogs.Enabled = false;
            this.txtLogs.Location = new System.Drawing.Point(12, 61);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(450, 450);
            this.txtLogs.TabIndex = 21;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastros,
            this.consultas,
            this.formasdeconexao,
            this.utilidades});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(475, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Logs de Conexão:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 560);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem exemploDeConversãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acessoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catracaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirPortaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atualizarDataEHoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilidades;
        private System.Windows.Forms.ToolStripMenuItem nomeExibiçãoDoAcessoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setarOfflineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setarOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formasdeconexao;
        private System.Windows.Forms.ToolStripMenuItem logsDeAcessoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosCadastradosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultas;
        private System.Windows.Forms.ToolStripMenuItem listarCartõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarUsuárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastros;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label2;
    }
}