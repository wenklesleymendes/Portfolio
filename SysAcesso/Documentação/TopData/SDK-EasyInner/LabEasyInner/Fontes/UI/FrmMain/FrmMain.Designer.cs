namespace EasyInnerSDK
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tmInnerOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiInnerOffLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tmInnerOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiInnerOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tmInnerBio = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiInnerBio = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiInnerBio6xx = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSobre = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiManual = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmInnerOffline,
            this.tmInnerOnline,
            this.tmInnerBio,
            this.ajudaToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(733, 24);
            this.msMenu.TabIndex = 1;
            this.msMenu.Text = "msMenu";
            // 
            // tmInnerOffline
            // 
            this.tmInnerOffline.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiInnerOffLine});
            this.tmInnerOffline.Name = "tmInnerOffline";
            this.tmInnerOffline.Size = new System.Drawing.Size(88, 20);
            this.tmInnerOffline.Text = "Inner OffLine";
            // 
            // tmiInnerOffLine
            // 
            this.tmiInnerOffLine.Name = "tmiInnerOffLine";
            this.tmiInnerOffLine.Size = new System.Drawing.Size(113, 22);
            this.tmiInnerOffLine.Text = "OffLine";
            this.tmiInnerOffLine.Click += new System.EventHandler(this.tmiInnerOffLine_Click);
            // 
            // tmInnerOnline
            // 
            this.tmInnerOnline.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiInnerOnline});
            this.tmInnerOnline.Name = "tmInnerOnline";
            this.tmInnerOnline.Size = new System.Drawing.Size(87, 20);
            this.tmInnerOnline.Text = "Inner OnLine";
            // 
            // tmiInnerOnline
            // 
            this.tmiInnerOnline.Name = "tmiInnerOnline";
            this.tmiInnerOnline.Size = new System.Drawing.Size(142, 22);
            this.tmiInnerOnline.Text = "Inner OnLine";
            this.tmiInnerOnline.Click += new System.EventHandler(this.tmiInnerOnline_Click);
            // 
            // tmInnerBio
            // 
            this.tmInnerBio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiInnerBio,
            this.tmiInnerBio6xx});
            this.tmInnerBio.Name = "tmInnerBio";
            this.tmInnerBio.Size = new System.Drawing.Size(66, 20);
            this.tmInnerBio.Text = "Inner Bio";
            // 
            // tmiInnerBio
            // 
            this.tmiInnerBio.Name = "tmiInnerBio";
            this.tmiInnerBio.Size = new System.Drawing.Size(140, 22);
            this.tmiInnerBio.Text = "Inner Bio";
            this.tmiInnerBio.Click += new System.EventHandler(this.tmiInnerBio_Click);
            // 
            // tmiInnerBio6xx
            // 
            this.tmiInnerBio6xx.Name = "tmiInnerBio6xx";
            this.tmiInnerBio6xx.Size = new System.Drawing.Size(140, 22);
            this.tmiInnerBio6xx.Text = "Inner Bio 6xx";
            this.tmiInnerBio6xx.Click += new System.EventHandler(this.tmiInnerBio6xx_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiSobre,
            this.tmiManual});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // tmiSobre
            // 
            this.tmiSobre.Name = "tmiSobre";
            this.tmiSobre.Size = new System.Drawing.Size(224, 22);
            this.tmiSobre.Text = "Sobre";
            this.tmiSobre.Click += new System.EventHandler(this.tmiSobre_Click);
            // 
            // tmiManual
            // 
            this.tmiManual.Name = "tmiManual";
            this.tmiManual.Size = new System.Drawing.Size(224, 22);
            this.tmiManual.Text = "Manual de desenvolvimento";
            this.tmiManual.Click += new System.EventHandler(this.tmiManualDesenvolvimento_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 253);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMenu;
            this.Name = "frmMain";
            this.Text = "SDK EasyInner 4.0.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tmInnerOffline;
        private System.Windows.Forms.ToolStripMenuItem tmiInnerOffLine;
        private System.Windows.Forms.ToolStripMenuItem tmInnerOnline;
        private System.Windows.Forms.ToolStripMenuItem tmiInnerOnline;
        private System.Windows.Forms.ToolStripMenuItem tmInnerBio;
        private System.Windows.Forms.ToolStripMenuItem tmiInnerBio;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmiSobre;
        private System.Windows.Forms.ToolStripMenuItem tmiManual;
        private System.Windows.Forms.ToolStripMenuItem tmiInnerBio6xx;
    }
}

