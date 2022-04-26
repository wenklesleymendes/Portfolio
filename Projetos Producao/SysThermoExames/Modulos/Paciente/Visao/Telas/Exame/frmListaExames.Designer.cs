
using MdPaciente.Dominio;

namespace MdPaciente.Visao
{
    partial class frmListaExames
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
            this.tipoExameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbLstOperadoresTitulo = new System.Windows.Forms.Label();
            this.pnControlesListaExame = new System.Windows.Forms.Panel();
            this.pbEditar = new System.Windows.Forms.PictureBox();
            this.pbRemove = new System.Windows.Forms.PictureBox();
            this.pbAdd = new System.Windows.Forms.PictureBox();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.pbPesquisa = new System.Windows.Forms.PictureBox();
            this.operadorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOperadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examesBindingSource)).BeginInit();
            this.pnControlesListaExame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).BeginInit();
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
            this.tipoExameDataGridViewTextBoxColumn});
            this.dgvLstOperadores.DataSource = this.examesBindingSource;
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
            this.dgvLstOperadores.Location = new System.Drawing.Point(2, 68);
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
            this.dgvLstOperadores.RowHeadersWidth = 51;
            this.dgvLstOperadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstOperadores.Size = new System.Drawing.Size(443, 377);
            this.dgvLstOperadores.TabIndex = 14;
            // 
            // tipoExameDataGridViewTextBoxColumn
            // 
            this.tipoExameDataGridViewTextBoxColumn.DataPropertyName = "TipoExame";
            this.tipoExameDataGridViewTextBoxColumn.HeaderText = "TipoExame";
            this.tipoExameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tipoExameDataGridViewTextBoxColumn.Name = "tipoExameDataGridViewTextBoxColumn";
            this.tipoExameDataGridViewTextBoxColumn.ReadOnly = true;
            this.tipoExameDataGridViewTextBoxColumn.Width = 94;
            // 
            // examesBindingSource
            // 
            this.examesBindingSource.DataSource = typeof(MdPaciente.Dominio.Exames);
            // 
            // lbLstOperadoresTitulo
            // 
            this.lbLstOperadoresTitulo.AutoSize = true;
            this.lbLstOperadoresTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLstOperadoresTitulo.ForeColor = System.Drawing.Color.White;
            this.lbLstOperadoresTitulo.Location = new System.Drawing.Point(3, 3);
            this.lbLstOperadoresTitulo.Name = "lbLstOperadoresTitulo";
            this.lbLstOperadoresTitulo.Size = new System.Drawing.Size(126, 20);
            this.lbLstOperadoresTitulo.TabIndex = 13;
            this.lbLstOperadoresTitulo.Text = "Lista de Exames";
            // 
            // pnControlesListaExame
            // 
            this.pnControlesListaExame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pnControlesListaExame.Controls.Add(this.pbEditar);
            this.pnControlesListaExame.Controls.Add(this.pbRemove);
            this.pnControlesListaExame.Controls.Add(this.pbAdd);
            this.pnControlesListaExame.Controls.Add(this.txtPesquisa);
            this.pnControlesListaExame.Controls.Add(this.pbPesquisa);
            this.pnControlesListaExame.Location = new System.Drawing.Point(2, 24);
            this.pnControlesListaExame.Name = "pnControlesListaExame";
            this.pnControlesListaExame.Size = new System.Drawing.Size(443, 44);
            this.pnControlesListaExame.TabIndex = 19;
            // 
            // pbEditar
            // 
            this.pbEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pbEditar.Image = global::MdPaciente.Properties.Resources.editarCadastro32px;
            this.pbEditar.Location = new System.Drawing.Point(342, 0);
            this.pbEditar.Name = "pbEditar";
            this.pbEditar.Size = new System.Drawing.Size(42, 44);
            this.pbEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbEditar.TabIndex = 2;
            this.pbEditar.TabStop = false;
            // 
            // pbRemove
            // 
            this.pbRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pbRemove.Image = global::MdPaciente.Properties.Resources.removerCadastro32px;
            this.pbRemove.Location = new System.Drawing.Point(290, 0);
            this.pbRemove.Name = "pbRemove";
            this.pbRemove.Size = new System.Drawing.Size(42, 44);
            this.pbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRemove.TabIndex = 4;
            this.pbRemove.TabStop = false;
            // 
            // pbAdd
            // 
            this.pbAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pbAdd.Image = global::MdPaciente.Properties.Resources.addCadastro_32px;
            this.pbAdd.Location = new System.Drawing.Point(238, 0);
            this.pbAdd.Name = "pbAdd";
            this.pbAdd.Size = new System.Drawing.Size(42, 44);
            this.pbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAdd.TabIndex = 3;
            this.pbAdd.TabStop = false;
            this.pbAdd.Click += new System.EventHandler(this.pbAdd_Click);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.txtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.Location = new System.Drawing.Point(45, 13);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(186, 20);
            this.txtPesquisa.TabIndex = 2;
            // 
            // pbPesquisa
            // 
            this.pbPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pbPesquisa.Image = global::MdPaciente.Properties.Resources.pesquisa_32px;
            this.pbPesquisa.Location = new System.Drawing.Point(0, 0);
            this.pbPesquisa.Name = "pbPesquisa";
            this.pbPesquisa.Size = new System.Drawing.Size(42, 44);
            this.pbPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbPesquisa.TabIndex = 1;
            this.pbPesquisa.TabStop = false;
            // 
            // operadorBindingSource
            // 
            this.operadorBindingSource.DataSource = typeof(ModelPrincipal.Entidades.Operador);
            // 
            // frmListaExames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.ClientSize = new System.Drawing.Size(446, 457);
            this.Controls.Add(this.pnControlesListaExame);
            this.Controls.Add(this.dgvLstOperadores);
            this.Controls.Add(this.lbLstOperadoresTitulo);
            this.Name = "frmListaExames";
            this.Text = "frmListaExames";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOperadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examesBindingSource)).EndInit();
            this.pnControlesListaExame.ResumeLayout(false);
            this.pnControlesListaExame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operadorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLstOperadores;
        private System.Windows.Forms.BindingSource operadorBindingSource;
        private System.Windows.Forms.Label lbLstOperadoresTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoExameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anamneseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn laudoDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource examesBindingSource;
        private System.Windows.Forms.Panel pnControlesListaExame;
        private System.Windows.Forms.PictureBox pbEditar;
        private System.Windows.Forms.PictureBox pbRemove;
        private System.Windows.Forms.PictureBox pbAdd;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.PictureBox pbPesquisa;
    }
}