
namespace EMCatraca.WindowsForms.Configuracoes.ControlesUsuario
{
    partial class DataGridSelecaoCatraca
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
            this.components = new System.ComponentModel.Container();
            this.dgGrid = new System.Windows.Forms.DataGridView();
            this.bsObjetos = new System.Windows.Forms.BindingSource(this.components);
            this.tlpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblItensSelecionados = new System.Windows.Forms.Label();
            this.lblTextoLegenda = new System.Windows.Forms.Label();
            this.lblCorLegenda = new System.Windows.Forms.Label();
            this.btnInverter = new System.Windows.Forms.Button();
            this.btnNenhum = new System.Windows.Forms.Button();
            this.btnTodos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsObjetos)).BeginInit();
            this.tlpLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgGrid
            // 
            this.dgGrid.AllowUserToAddRows = false;
            this.dgGrid.AllowUserToDeleteRows = false;
            this.dgGrid.AutoGenerateColumns = false;
            this.dgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGrid.DataSource = this.bsObjetos;
            this.dgGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGrid.Location = new System.Drawing.Point(0, 0);
            this.dgGrid.Margin = new System.Windows.Forms.Padding(0);
            this.dgGrid.Name = "dgGrid";
            this.dgGrid.ReadOnly = true;
            this.dgGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGrid.Size = new System.Drawing.Size(505, 225);
            this.dgGrid.TabIndex = 0;
            // 
            // tlpLayout
            // 
            this.tlpLayout.ColumnCount = 1;
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.Controls.Add(this.panel1, 0, 1);
            this.tlpLayout.Controls.Add(this.dgGrid, 0, 0);
            this.tlpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLayout.Name = "tlpLayout";
            this.tlpLayout.RowCount = 2;
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLayout.Size = new System.Drawing.Size(505, 253);
            this.tlpLayout.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblItensSelecionados);
            this.panel1.Controls.Add(this.lblTextoLegenda);
            this.panel1.Controls.Add(this.lblCorLegenda);
            this.panel1.Controls.Add(this.btnInverter);
            this.panel1.Controls.Add(this.btnNenhum);
            this.panel1.Controls.Add(this.btnTodos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 225);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 28);
            this.panel1.TabIndex = 1;
            // 
            // lblItensSelecionados
            // 
            this.lblItensSelecionados.AutoSize = true;
            this.lblItensSelecionados.Location = new System.Drawing.Point(230, 7);
            this.lblItensSelecionados.Name = "lblItensSelecionados";
            this.lblItensSelecionados.Size = new System.Drawing.Size(97, 13);
            this.lblItensSelecionados.TabIndex = 2;
            this.lblItensSelecionados.Text = "Itens Selecionados";
            this.lblItensSelecionados.Visible = false;
            // 
            // lblTextoLegenda
            // 
            this.lblTextoLegenda.AutoSize = true;
            this.lblTextoLegenda.Location = new System.Drawing.Point(25, 7);
            this.lblTextoLegenda.Name = "lblTextoLegenda";
            this.lblTextoLegenda.Size = new System.Drawing.Size(85, 13);
            this.lblTextoLegenda.TabIndex = 1;
            this.lblTextoLegenda.Text = "[Texto Legenda]";
            this.lblTextoLegenda.Visible = false;
            // 
            // lblCorLegenda
            // 
            this.lblCorLegenda.BackColor = System.Drawing.Color.LightGray;
            this.lblCorLegenda.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCorLegenda.Location = new System.Drawing.Point(8, 8);
            this.lblCorLegenda.Name = "lblCorLegenda";
            this.lblCorLegenda.Size = new System.Drawing.Size(15, 13);
            this.lblCorLegenda.TabIndex = 0;
            this.lblCorLegenda.Visible = false;
            // 
            // btnInverter
            // 
            this.btnInverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInverter.Location = new System.Drawing.Point(446, 2);
            this.btnInverter.Name = "btnInverter";
            this.btnInverter.Size = new System.Drawing.Size(56, 23);
            this.btnInverter.TabIndex = 5;
            this.btnInverter.Text = "Inverter";
            this.btnInverter.UseVisualStyleBackColor = true;
            // 
            // btnNenhum
            // 
            this.btnNenhum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNenhum.Location = new System.Drawing.Point(389, 2);
            this.btnNenhum.Name = "btnNenhum";
            this.btnNenhum.Size = new System.Drawing.Size(56, 23);
            this.btnNenhum.TabIndex = 4;
            this.btnNenhum.Text = "Nenhum";
            this.btnNenhum.UseVisualStyleBackColor = true;
            // 
            // btnTodos
            // 
            this.btnTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTodos.Location = new System.Drawing.Point(332, 2);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(56, 23);
            this.btnTodos.TabIndex = 3;
            this.btnTodos.Text = "Todos";
            this.btnTodos.UseVisualStyleBackColor = true;
            // 
            // DataGridSelecaoCatraca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpLayout);
            this.Name = "DataGridSelecaoCatraca";
            this.Size = new System.Drawing.Size(505, 253);
            ((System.ComponentModel.ISupportInitialize)(this.dgGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsObjetos)).EndInit();
            this.tlpLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgGrid;
        private System.Windows.Forms.BindingSource bsObjetos;
        private System.Windows.Forms.TableLayoutPanel tlpLayout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblItensSelecionados;
        private System.Windows.Forms.Label lblTextoLegenda;
        private System.Windows.Forms.Label lblCorLegenda;
        private System.Windows.Forms.Button btnInverter;
        private System.Windows.Forms.Button btnNenhum;
        private System.Windows.Forms.Button btnTodos;
    }
}
