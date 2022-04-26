
namespace Formularios.Telas.Login
{
    partial class frmListaOperadores
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLstOperadores = new System.Windows.Forms.DataGridView();
            this.lbLstOperadoresTitulo = new System.Windows.Forms.Label();
            this.btnLstOperdoresNovo = new System.Windows.Forms.Button();
            this.btnLstOperadoresExcluir = new System.Windows.Forms.Button();
            this.btnLstOperadorEditar = new System.Windows.Forms.Button();
            this.BtnLstOperadoresFechar = new System.Windows.Forms.Button();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nomeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ehAdministradorDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataDeCadastroDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operadorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOperadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operadorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLstOperadores
            // 
            this.dgvLstOperadores.AllowUserToAddRows = false;
            this.dgvLstOperadores.AllowUserToDeleteRows = false;
            this.dgvLstOperadores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLstOperadores.AutoGenerateColumns = false;
            this.dgvLstOperadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLstOperadores.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.dgvLstOperadores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLstOperadores.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLstOperadores.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstOperadores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLstOperadores.ColumnHeadersHeight = 30;
            this.dgvLstOperadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.nomeDataGridViewTextBoxColumn,
            this.cPFDataGridViewTextBoxColumn,
            this.loginDataGridViewTextBoxColumn,
            this.ehAdministradorDataGridViewCheckBoxColumn,
            this.dataDeCadastroDataGridViewTextBoxColumn});
            this.dgvLstOperadores.DataSource = this.operadorBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLstOperadores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLstOperadores.EnableHeadersVisualStyles = false;
            this.dgvLstOperadores.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.dgvLstOperadores.Location = new System.Drawing.Point(35, 68);
            this.dgvLstOperadores.Name = "dgvLstOperadores";
            this.dgvLstOperadores.ReadOnly = true;
            this.dgvLstOperadores.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.PaleVioletRed;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstOperadores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLstOperadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstOperadores.Size = new System.Drawing.Size(711, 377);
            this.dgvLstOperadores.TabIndex = 8;
            this.dgvLstOperadores.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLstOperadores_CellContentDoubleClick);
            // 
            // lbLstOperadoresTitulo
            // 
            this.lbLstOperadoresTitulo.AutoSize = true;
            this.lbLstOperadoresTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLstOperadoresTitulo.ForeColor = System.Drawing.Color.White;
            this.lbLstOperadoresTitulo.Location = new System.Drawing.Point(60, 24);
            this.lbLstOperadoresTitulo.Name = "lbLstOperadoresTitulo";
            this.lbLstOperadoresTitulo.Size = new System.Drawing.Size(150, 20);
            this.lbLstOperadoresTitulo.TabIndex = 7;
            this.lbLstOperadoresTitulo.Text = "Lista de operadores";
            // 
            // btnLstOperdoresNovo
            // 
            this.btnLstOperdoresNovo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLstOperdoresNovo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperdoresNovo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLstOperdoresNovo.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnLstOperdoresNovo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnLstOperdoresNovo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperdoresNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLstOperdoresNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLstOperdoresNovo.ForeColor = System.Drawing.Color.Silver;
            this.btnLstOperdoresNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLstOperdoresNovo.Location = new System.Drawing.Point(760, 68);
            this.btnLstOperdoresNovo.Name = "btnLstOperdoresNovo";
            this.btnLstOperdoresNovo.Size = new System.Drawing.Size(100, 30);
            this.btnLstOperdoresNovo.TabIndex = 12;
            this.btnLstOperdoresNovo.Text = "Novo";
            this.btnLstOperdoresNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLstOperdoresNovo.UseVisualStyleBackColor = false;
            this.btnLstOperdoresNovo.Click += new System.EventHandler(this.btnLstOperdoresNovo_Click);
            // 
            // btnLstOperadoresExcluir
            // 
            this.btnLstOperadoresExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLstOperadoresExcluir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperadoresExcluir.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLstOperadoresExcluir.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnLstOperadoresExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnLstOperadoresExcluir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperadoresExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLstOperadoresExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLstOperadoresExcluir.ForeColor = System.Drawing.Color.Silver;
            this.btnLstOperadoresExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLstOperadoresExcluir.Location = new System.Drawing.Point(760, 140);
            this.btnLstOperadoresExcluir.Name = "btnLstOperadoresExcluir";
            this.btnLstOperadoresExcluir.Size = new System.Drawing.Size(100, 30);
            this.btnLstOperadoresExcluir.TabIndex = 11;
            this.btnLstOperadoresExcluir.Text = "Excluir";
            this.btnLstOperadoresExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLstOperadoresExcluir.UseVisualStyleBackColor = false;
            this.btnLstOperadoresExcluir.Click += new System.EventHandler(this.btnLstOperadoresExcluir_Click);
            // 
            // btnLstOperadorEditar
            // 
            this.btnLstOperadorEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLstOperadorEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperadorEditar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLstOperadorEditar.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnLstOperadorEditar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnLstOperadorEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.btnLstOperadorEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLstOperadorEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLstOperadorEditar.ForeColor = System.Drawing.Color.Silver;
            this.btnLstOperadorEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLstOperadorEditar.Location = new System.Drawing.Point(760, 104);
            this.btnLstOperadorEditar.Name = "btnLstOperadorEditar";
            this.btnLstOperadorEditar.Size = new System.Drawing.Size(100, 30);
            this.btnLstOperadorEditar.TabIndex = 10;
            this.btnLstOperadorEditar.Text = "Editar";
            this.btnLstOperadorEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLstOperadorEditar.UseVisualStyleBackColor = false;
            this.btnLstOperadorEditar.Click += new System.EventHandler(this.btnLstOperadorEditar_Click);
            // 
            // BtnLstOperadoresFechar
            // 
            this.BtnLstOperadoresFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLstOperadoresFechar.FlatAppearance.BorderSize = 0;
            this.BtnLstOperadoresFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLstOperadoresFechar.Image = global::Formularios.Properties.Resources.Close;
            this.BtnLstOperadoresFechar.Location = new System.Drawing.Point(11, 12);
            this.BtnLstOperadoresFechar.Name = "BtnLstOperadoresFechar";
            this.BtnLstOperadoresFechar.Size = new System.Drawing.Size(43, 43);
            this.BtnLstOperadoresFechar.TabIndex = 9;
            this.BtnLstOperadoresFechar.UseVisualStyleBackColor = true;
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            this.codigoDataGridViewTextBoxColumn.Width = 70;
            // 
            // nomeDataGridViewTextBoxColumn
            // 
            this.nomeDataGridViewTextBoxColumn.DataPropertyName = "Nome";
            this.nomeDataGridViewTextBoxColumn.HeaderText = "Nome";
            this.nomeDataGridViewTextBoxColumn.Name = "nomeDataGridViewTextBoxColumn";
            this.nomeDataGridViewTextBoxColumn.ReadOnly = true;
            this.nomeDataGridViewTextBoxColumn.Width = 65;
            // 
            // cPFDataGridViewTextBoxColumn
            // 
            this.cPFDataGridViewTextBoxColumn.DataPropertyName = "CPF";
            this.cPFDataGridViewTextBoxColumn.HeaderText = "CPF";
            this.cPFDataGridViewTextBoxColumn.Name = "cPFDataGridViewTextBoxColumn";
            this.cPFDataGridViewTextBoxColumn.ReadOnly = true;
            this.cPFDataGridViewTextBoxColumn.Width = 54;
            // 
            // loginDataGridViewTextBoxColumn
            // 
            this.loginDataGridViewTextBoxColumn.DataPropertyName = "Login";
            this.loginDataGridViewTextBoxColumn.HeaderText = "Login";
            this.loginDataGridViewTextBoxColumn.Name = "loginDataGridViewTextBoxColumn";
            this.loginDataGridViewTextBoxColumn.ReadOnly = true;
            this.loginDataGridViewTextBoxColumn.Width = 62;
            // 
            // ehAdministradorDataGridViewCheckBoxColumn
            // 
            this.ehAdministradorDataGridViewCheckBoxColumn.DataPropertyName = "EhAdministrador";
            this.ehAdministradorDataGridViewCheckBoxColumn.HeaderText = "EhAdministrador";
            this.ehAdministradorDataGridViewCheckBoxColumn.Name = "ehAdministradorDataGridViewCheckBoxColumn";
            this.ehAdministradorDataGridViewCheckBoxColumn.ReadOnly = true;
            this.ehAdministradorDataGridViewCheckBoxColumn.Width = 103;
            // 
            // dataDeCadastroDataGridViewTextBoxColumn
            // 
            this.dataDeCadastroDataGridViewTextBoxColumn.DataPropertyName = "DataDeCadastro";
            this.dataDeCadastroDataGridViewTextBoxColumn.HeaderText = "DataDeCadastro";
            this.dataDeCadastroDataGridViewTextBoxColumn.Name = "dataDeCadastroDataGridViewTextBoxColumn";
            this.dataDeCadastroDataGridViewTextBoxColumn.ReadOnly = true;
            this.dataDeCadastroDataGridViewTextBoxColumn.Width = 122;
            // 
            // operadorBindingSource
            // 
            this.operadorBindingSource.DataSource = typeof(ModelPrincipal.Entidades.Operador);
            // 
            // frmListaOperadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.ClientSize = new System.Drawing.Size(870, 457);
            this.Controls.Add(this.dgvLstOperadores);
            this.Controls.Add(this.lbLstOperadoresTitulo);
            this.Controls.Add(this.btnLstOperdoresNovo);
            this.Controls.Add(this.btnLstOperadoresExcluir);
            this.Controls.Add(this.btnLstOperadorEditar);
            this.Controls.Add(this.BtnLstOperadoresFechar);
            this.Name = "frmListaOperadores";
            this.Text = "frmListaOperadores";
            this.Load += new System.EventHandler(this.frmListaOperadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOperadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operadorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLstOperadores;
        private System.Windows.Forms.Label lbLstOperadoresTitulo;
        private System.Windows.Forms.Button btnLstOperdoresNovo;
        private System.Windows.Forms.Button btnLstOperadoresExcluir;
        private System.Windows.Forms.Button btnLstOperadorEditar;
        private System.Windows.Forms.Button BtnLstOperadoresFechar;
        private System.Windows.Forms.BindingSource operadorBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loginDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ehAdministradorDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDeCadastroDataGridViewTextBoxColumn;
    }
}