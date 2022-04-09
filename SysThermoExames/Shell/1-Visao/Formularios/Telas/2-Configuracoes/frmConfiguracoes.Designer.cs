//using System;
//using MaterialSkin.Controls;
//using System.Windows.Forms;

//namespace Formularios.Telas._2_Configuracoes
//{
//    partial class frmConfiguracoes
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            //}
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.folderBrowserModulos = new System.Windows.Forms.FolderBrowserDialog();
//            this.txtboxDiretorioModulos = new MaterialSkin.Controls.MaterialTextBox();
//            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
//            this.lbl1 = new MaterialSkin.Controls.MaterialLabel();
//            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
//            this.txtboxDiretorioBD = new MaterialSkin.Controls.MaterialTextBox();
//            this.lblversao = new MaterialSkin.Controls.MaterialLabel();
//            this.folderBrowserBD = new System.Windows.Forms.FolderBrowserDialog();
//            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
//            this.SuspendLayout();
//            // 
//            // txtboxDiretorioModulos
//            // 
//            this.txtboxDiretorioModulos.AnimateReadOnly = false;
//            this.txtboxDiretorioModulos.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.txtboxDiretorioModulos.Depth = 0;
//            this.txtboxDiretorioModulos.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.txtboxDiretorioModulos.LeadingIcon = null;
//            this.txtboxDiretorioModulos.Location = new System.Drawing.Point(172, 76);
//            this.txtboxDiretorioModulos.MaxLength = 50;
//            this.txtboxDiretorioModulos.MouseState = MaterialSkin.MouseState.OUT;
//            this.txtboxDiretorioModulos.Multiline = false;
//            this.txtboxDiretorioModulos.Name = "txtboxDiretorioModulos";
//            this.txtboxDiretorioModulos.Size = new System.Drawing.Size(444, 50);
//            this.txtboxDiretorioModulos.TabIndex = 0;
//            this.txtboxDiretorioModulos.Text = "";
//            this.txtboxDiretorioModulos.TrailingIcon = null;
//            this.txtboxDiretorioModulos.Click += new System.EventHandler(this.txtboxDiretorioModulos_Click);
//            // 
//            // materialLabel1
//            // 
//            this.materialLabel1.AutoSize = true;
//            this.materialLabel1.Depth = 0;
//            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.materialLabel1.Location = new System.Drawing.Point(13, 159);
//            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
//            this.materialLabel1.Name = "materialLabel1";
//            this.materialLabel1.Size = new System.Drawing.Size(174, 19);
//            this.materialLabel1.TabIndex = 1;
//            this.materialLabel1.Text = "Arquivo de configuração";
//            // 
//            // lbl1
//            // 
//            this.lbl1.AutoSize = true;
//            this.lbl1.Depth = 0;
//            this.lbl1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lbl1.Location = new System.Drawing.Point(13, 92);
//            this.lbl1.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lbl1.Name = "lbl1";
//            this.lbl1.Size = new System.Drawing.Size(129, 19);
//            this.lbl1.TabIndex = 2;
//            this.lbl1.Text = "Pasta de módulos";
//            // 
//            // materialLabel2
//            // 
//            this.materialLabel2.AutoSize = true;
//            this.materialLabel2.Depth = 0;
//            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.materialLabel2.Location = new System.Drawing.Point(13, 220);
//            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
//            this.materialLabel2.Name = "materialLabel2";
//            this.materialLabel2.Size = new System.Drawing.Size(50, 19);
//            this.materialLabel2.TabIndex = 3;
//            this.materialLabel2.Text = "Versão";
//            // 
//            // txtboxDiretorioBD
//            // 
//            this.txtboxDiretorioBD.AnimateReadOnly = false;
//            this.txtboxDiretorioBD.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.txtboxDiretorioBD.Depth = 0;
//            this.txtboxDiretorioBD.Enabled = false;
//            this.txtboxDiretorioBD.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.txtboxDiretorioBD.LeadingIcon = null;
//            this.txtboxDiretorioBD.Location = new System.Drawing.Point(172, 148);
//            this.txtboxDiretorioBD.MaxLength = 50;
//            this.txtboxDiretorioBD.MouseState = MaterialSkin.MouseState.OUT;
//            this.txtboxDiretorioBD.Multiline = false;
//            this.txtboxDiretorioBD.Name = "txtboxDiretorioBD";
//            this.txtboxDiretorioBD.Size = new System.Drawing.Size(444, 50);
//            this.txtboxDiretorioBD.TabIndex = 4;
//            this.txtboxDiretorioBD.Text = "";
//            this.txtboxDiretorioBD.TrailingIcon = null;
//            this.txtboxDiretorioBD.Click += new System.EventHandler(this.txtboxDiretorioBD_ClickAsync);
//            // 
//            // lblversao
//            // 
//            this.lblversao.AutoSize = true;
//            this.lblversao.Depth = 0;
//            this.lblversao.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
//            this.lblversao.Location = new System.Drawing.Point(172, 220);
//            this.lblversao.MouseState = MaterialSkin.MouseState.HOVER;
//            this.lblversao.Name = "lblversao";
//            this.lblversao.Size = new System.Drawing.Size(1, 0);
//            this.lblversao.TabIndex = 5;
//            // 
//            // openFileDialog1
//            // 
//            this.openFileDialog1.FileName = "openFileDialog1";
//            // 
//            // frmConfiguracoes
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(634, 259);
//            this.Controls.Add(this.lblversao);
//            this.Controls.Add(this.txtboxDiretorioBD);
//            this.Controls.Add(this.materialLabel2);
//            this.Controls.Add(this.lbl1);
//            this.Controls.Add(this.materialLabel1);
//            this.Controls.Add(this.txtboxDiretorioModulos);
//            this.Name = "frmConfiguracoes";
//            this.Padding = new System.Windows.Forms.Padding(3, 55, 3, 3);
//            this.Text = "frmConfiguracoes";
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private FolderBrowserDialog folderBrowserModulos;
//        private MaterialSkin.Controls.MaterialTextBox txtboxDiretorioModulos;
//        private MaterialSkin.Controls.MaterialLabel materialLabel1;
//        private MaterialSkin.Controls.MaterialLabel lbl1;
//        private MaterialSkin.Controls.MaterialLabel materialLabel2;
//        private MaterialSkin.Controls.MaterialTextBox txtboxDiretorioBD;
//        private MaterialSkin.Controls.MaterialLabel lblversao;
//        private FolderBrowserDialog folderBrowserBD;
//        private OpenFileDialog openFileDialog1;
//    }
//}