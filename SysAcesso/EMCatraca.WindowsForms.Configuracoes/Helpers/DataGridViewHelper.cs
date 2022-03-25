using EM.Infra.Excecoes;
using EMCatraca.WindowsForms.Configuracoes.Utilidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Helpers
{
    public class DataGridViewHelper
    {
        public DataGridView DataGrid { get; private set; }

        public DataGridViewHelper(DataGridView dataGrid)
        {
            DataGrid = dataGrid;
            DataGrid.CellFormatting += dataGrid_CellFormatting;
            FormateDataGrid();
            AjusteGridSuperfast(dataGrid);
            PermitaOrdenacaoNoCliqueDaColuna();
            AjusteEventoMenuContexto();
        }

        /// <summary>
        /// Este método torna a renderização da grid mais rápida e elimina o pisca-pisca na tela
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void AjusteGridSuperfast(DataGridView dataGrid)
        {
            dataGrid.GetType()
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(dataGrid, true, null);
        }

        private void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (!(e.CellStyle.FormatProvider is ICustomFormatter formatter))
                {
                    return;
                }

                e.Value = formatter.Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider);
                e.FormattingApplied = true;
            }
            catch
            {
                e.FormattingApplied = false;
            }
        }

        public void RemovaColunas()
        {
            DataGrid.Columns.Clear();
        }

        public void RemovaLinhas()
        {
            DataGrid.Rows.Clear();
        }

        public DataGridViewColumn AddColumn(string headerText, string dataPropertyName, int width, DataGridViewCellStyle style, int maxLength)
        {
            var coluna = AddColumn(headerText, dataPropertyName, width);
            coluna.DefaultCellStyle = style;
            ((DataGridViewTextBoxColumn)coluna).MaxInputLength = maxLength;
            return coluna;
        }

        public DataGridViewColumn AddColumn(string headerText, string dataPropertyName, int width, DataGridViewCellStyle style)
        {
            var coluna = AddColumn(headerText, dataPropertyName, width);
            coluna.DefaultCellStyle = style;
            return coluna;
        }

        public DataGridViewColumn AddColumn(string headerText, string dataPropertyName, int width)
        {
            var coluna = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = width,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName
            };
            coluna.HeaderCell.ToolTipText = headerText;

            DataGrid.Columns.Add(coluna);

            return coluna;
        }

        public DataGridViewColumn AddColumnRight(string headerText, string dataPropertyName, int width)
        {
            var coluna = AddColumn(headerText, dataPropertyName, width);
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return coluna;
        }

        public DataGridViewColumn AddColumnCheckBox(string headerText, string dataPropertyName, int width)
        {
            var coluna = CrieColunaCheckBox(headerText, dataPropertyName, width);
            DataGrid.Columns.Add(coluna);
            return coluna;
        }

        public DataGridViewCheckBoxColumn CrieColunaCheckBox(string headerText, string dataPropertyName, int width)
        {
            var coluna = new DataGridViewCheckBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Resizable = DataGridViewTriState.False,
                Width = width,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName
            };

            return coluna;
        }

        public DataGridViewColumn AddColumnData(string headerText, string dataPropertyName, int width)
        {
            var coluna = AddColumn(headerText, dataPropertyName, width);
            coluna.DefaultCellStyle.Format = "dd/MM/yyyy";
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return coluna;
        }

        public DataGridViewColumn AddColumnData(string headerText, string dataPropertyName)
        {
            return AddColumnData(headerText, dataPropertyName, 75);
        }

        private bool _naoExibirMenuPadrao = false;
        private ContextMenu _menuContextoPadrao;

        public void NaoExibirMenuPadrao()
        {
            _naoExibirMenuPadrao = true;
        }

        public DataGridViewColumn AddColumnDataEHora(string headerText, string dataPropertyName)
        {
            var coluna = AddColumn(headerText, dataPropertyName, 120);
            coluna.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return coluna;
        }

        public DataGridViewColumn AddColumnLink(string headerText, string dataPropertyName, int width)
        {
            var coluna = new DataGridViewLinkColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Width = width
            };
            DataGrid.Columns.Add(coluna);
            return coluna;
        }

        public DataGridViewColumn AddColumnHoraMinuto(string headerText, string dataPropertyName)
        {
            var coluna = AddColumn(headerText, dataPropertyName, 60);
            coluna.DefaultCellStyle.Format = "hh:mm";
            coluna.DefaultCellStyle.FormatProvider = new TimeSpanFormatter();
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return coluna;
        }

        public DataGridViewColumn AddColumnFlutuante(string headerText, string dataPropertyName)
        {
            return AddColumnFlutuante(headerText, dataPropertyName, 90);
        }

        public DataGridViewColumn AddColumnFlutuante(string headerText, string dataPropertyName, int width)
        {
            var coluna = AddColumn(headerText, dataPropertyName, width);
            coluna.ValueType = typeof(decimal);
            coluna.DefaultCellStyle.Format = "#,###,##0.00";
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return coluna;
        }

        public DataGridViewColumn AddColumnFlutuanteFill(string headerText, string dataPropertyName)
        {
            var coluna = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                HeaderText = headerText
            };
            coluna.DefaultCellStyle.Format = "#,###,##0.00";
            DataGrid.Columns.Add(coluna);
            return coluna;
        }

        public DataGridViewColumn AddColumnCurrency(string headerText, string dataPropertyName, int width)
        {
            var coluna = AddColumnCurrency(headerText, dataPropertyName);
            coluna.Width = width;
            return coluna;
        }

        public DataGridViewColumn AddColumnCurrency(string headerText, string dataPropertyName)
        {
            var coluna = AddColumn(headerText, dataPropertyName, 90);
            coluna.ValueType = typeof(decimal);
            coluna.DefaultCellStyle.Format = "C2";
            coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return coluna;
        }

        public void ExibaDatasource(object datasource)
        {
            if (DataGrid.DataSource is BindingSource)
            {
                ((BindingSource)DataGrid.DataSource).DataSource = datasource;
                ((BindingSource)DataGrid.DataSource).ResetBindings(false);
            }
            else
            {
                DataGrid.DataSource = datasource;
                DataGrid.Update();
            }
        }

        public DataGridViewColumn AddColumnFill(string headerText, string dataPropertyName)
        {
            var coluna = new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName
            };
            DataGrid.Columns.Add(coluna);
            return coluna;
        }

        public void AjusteNomeColuna(int indexColuna, string columnName)
        {
            if (DataGrid.Columns.Count < indexColuna)
            {
                return;
            }

            var coluna = DataGrid.Columns[indexColuna];
            coluna.HeaderText = columnName;
        }

        public void AlinheCentralizado(string columnName)
        {
            var dataGridViewColumn = DataGrid.Columns[columnName];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void AltereMaxLenghtColuna(string columnName, int maxLenght)
        {
            var dataGridViewTextBoxColumn = (DataGridViewTextBoxColumn)DataGrid.Columns[columnName];
            if (dataGridViewTextBoxColumn != null)
            {
                dataGridViewTextBoxColumn.MaxInputLength = maxLenght;
            }
        }

        public void AlinheDireita(string columnName)
        {
            var dataGridViewColumn = DataGrid.Columns[columnName];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        public void FormateDataGrid()
        {
            DataGrid.AllowUserToAddRows = false;
            DataGrid.AllowUserToDeleteRows = false;
            DataGrid.AllowUserToResizeColumns = true;
            DataGrid.AllowUserToResizeRows = false;
            DataGrid.AutoGenerateColumns = false;
            DataGrid.BorderStyle = BorderStyle.None;
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DataGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataGrid.Margin = new Padding(0);
            DataGrid.ReadOnly = true;
            DataGrid.RowHeadersVisible = false;
            DataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DataGrid.ScrollBars = ScrollBars.Vertical;
            DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void PermitaOrdenacaoNoCliqueDaColuna()
        {
            DataGrid.ColumnHeaderMouseClick += ColumnHeaderMouseClick;
        }

        public void RemovaOrdenacaoNoCliqueDaColuna()
        {
            DataGrid.ColumnHeaderMouseClick -= ColumnHeaderMouseClick;
        }

        private void AjusteEventoMenuContexto()
        {
            DataGrid.MouseClick += DataGrid_MouseClick;
        }

        private void DataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (_naoExibirMenuPadrao)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                var currentMouseRow = DataGrid.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseRow < 0)
                {
                    return;
                }

                if (_menuContextoPadrao == null)
                {
                    _menuContextoPadrao = new ContextMenu();
                }

                var item = DataGrid.Rows[currentMouseRow].DataBoundItem;
                if (item != null)
                {
                    _menuContextoPadrao.Show(DataGrid, e.Location);
                }
            }
        }

        private void ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGrid = ((DataGridView)sender);
            if (dataGrid.DataSource == null || e.Button == MouseButtons.Right)
            {
                return;
            }
            var colunaSelecionada = dataGrid.Columns[e.ColumnIndex];
            if (colunaSelecionada.SortMode == DataGridViewColumnSortMode.NotSortable)
            {
                return;
            }

            var sortOrder = colunaSelecionada.HeaderCell.SortGlyphDirection;

            foreach (DataGridViewColumn item in dataGrid.Columns)
            {
                item.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            var dataPropertyName = colunaSelecionada.DataPropertyName;
            if (string.IsNullOrEmpty(dataPropertyName))
            {
                return;
            }

            foreach (DataGridViewColumn item in dataGrid.Columns)
            {
                item.HeaderCell.SortGlyphDirection = SortOrder.None;
                item.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            if (dataGrid.DataSource is BindingSource bindingSource)
            {
                if (bindingSource.DataSource == null)
                {
                    return;
                }

                if (bindingSource.DataSource is DataTable dataTable)
                {
                    dataTable.DefaultView.Sort = dataPropertyName + (sortOrder == SortOrder.Ascending ? " DESC" : string.Empty);
                }
                else
                {
                    var enumerable = (IEnumerable)bindingSource.DataSource;
                    var enumerator = enumerable.GetEnumerator();
                    var objetosDoDataGrid = new ArrayList();

                    while (enumerator.MoveNext())
                    {
                        objetosDoDataGrid.Add(enumerator.Current);
                    }

                    objetosDoDataGrid.Sort(new PropertyComparer(dataPropertyName, sortOrder == SortOrder.Ascending ? CompareOrder.Descending : CompareOrder.Ascending));

                    bindingSource.DataSource = objetosDoDataGrid;
                }
            }
            else
            {
                var objetosDoDataGrid = new ArrayList((ICollection)dataGrid.DataSource);
                objetosDoDataGrid.Sort(new PropertyComparer(dataPropertyName, (sortOrder == SortOrder.Ascending) ? CompareOrder.Descending : CompareOrder.Ascending));

                dataGrid.DataSource = objetosDoDataGrid;
                dataGrid.Invalidate();
                dataGrid.Update();
            }

            colunaSelecionada.HeaderCell.SortGlyphDirection = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
        }

        public void AjusteDataErrorParaEdicao()
        {
            DataGrid.DataError += dataGrid_DataError;
        }

        private void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is FormatException)
            {
                DialogoHelper.Erro($"Valor informado não está corretamente digitado. Por favor, informe novamente o valor do campo '{((DataGridView)sender).Columns[e.ColumnIndex].HeaderText}'.");
                e.ThrowException = false;
            }
            else if (e.Exception is InconsistenciaException)
            {
                DialogoHelper.Atencao(e.Exception.Message);
                e.ThrowException = false;
            }
        }

        public void AjusteScrollBarsParaAmbos()
        {
            DataGrid.ScrollBars = ScrollBars.Both;
        }

        public void OcultarMostrarColuna(string nomeDaColuna, bool mostrar)
        {
            var dataGridViewColumn = DataGrid.Columns[nomeDaColuna];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Visible = mostrar;
            }
        }

        public List<T> ObtenhaTodosObjetos<T>()
        {
            return (from DataGridViewRow linha in DataGrid.Rows select (T)linha.DataBoundItem).ToList();
        }

        public List<T> ObtenhaObjetosSelecionados<T>()
        {
            return ObtenhaObjetosSelecionados<T>(DataGrid);
        }

        public List<T> ObtenhaObjetosQueNaoEstaoSelecionados<T>()
        {
            return ObtenhaObjetosQueNaoEstaoSelecionados<T>(DataGrid);
        }

        public static List<T> ObtenhaObjetosSelecionados<T>(DataGridView dataGrid)
        {
            return (from DataGridViewRow linha in dataGrid.SelectedRows select (T)linha.DataBoundItem).ToList();
        }

        public static List<T> ObtenhaObjetosQueNaoEstaoSelecionados<T>(DataGridView dataGrid)
        {
            return (from DataGridViewRow linha in dataGrid.Rows where !linha.Selected select (T)linha.DataBoundItem).ToList();
        }
    }
}
