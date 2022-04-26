using EMCatraca.WindowsForms.Configuracoes.Helpers;
using EMCatraca.WindowsForms.Configuracoes.MetodosDeExtensao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.ControlesUsuario
{
    public partial class DataGridSelecaoCatraca : UserControl
    {
        private bool _estaExibidoCheckBoxSelecao = false;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Action AoMudarSelecao;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Action AoClicarTodos = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event Action AoClicarNenhum = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ExibirQuantidadeSelecionados { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridViewHelper Helper { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridView DataGridView => dgGrid;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingSource BindingSource => bsObjetos;

        public DataGridSelecaoCatraca()
        {
            InitializeComponent();

            Helper = new DataGridViewHelper(dgGrid);
            AdicioneEventoDeSelecao();
            dgGrid.ScrollBars = ScrollBars.Both;
        }

        private void AdicioneEventoDeSelecao()
        {
            dgGrid.SelectionChanged += dgGrid_SelectionChanged;
        }

        private void RemovaEventoDeSelecao()
        {
            dgGrid.SelectionChanged -= dgGrid_SelectionChanged;
        }

        private void dgGrid_SelectionChanged(object sender, EventArgs e)
        {
            AoMudarSelecao?.Invoke();
        }

        public DataGridViewColumn AddColumn(string headerText, string dataPropertyName, int width)
        {
            return Helper.AddColumn(headerText, dataPropertyName, width);
        }

        public DataGridViewColumn AddColumnRight(string headerText, string dataPropertyName, int width)
        {
            return Helper.AddColumnRight(headerText, dataPropertyName, width);
        }

        public DataGridViewColumn AddColumnFill(string headerText, string dataPropertyName)
        {
            return AddColumnFill(headerText, dataPropertyName, 0);
        }

        public DataGridViewColumn AddColumnFill(string headerText, string dataPropertyName, int width)
        {
            var coluna = Helper.AddColumnFill(headerText, dataPropertyName);
            coluna.Width = width;
            return coluna;
        }

        public DataGridViewColumn AddColumnFlutuante(string headerText, string dataPropertyName)
        {
            return Helper.AddColumnFlutuante(headerText, dataPropertyName);
        }

        public DataGridViewColumn AddColumnData(string headerText, string dataPropertyName, int width)
        {
            return Helper.AddColumnData(headerText, dataPropertyName, width);
        }

        public DataGridViewColumn AddColumnData(string headerText, string dataPropertyName)
        {
            return Helper.AddColumnData(headerText, dataPropertyName);
        }

        public DataGridViewColumn AddColumnCurrency(string headerText, string dataPropertyName)
        {
            return Helper.AddColumnCurrency(headerText, dataPropertyName);
        }

        public DataGridViewColumn AddColumnCheckBox(string headerText, string dataPropertyName, int width)
        {
            return Helper.AddColumnCheckBox(headerText, dataPropertyName, width);
        }

        public void AjusteDataErrorParaEdicao()
        {
            Helper.AjusteDataErrorParaEdicao();
        }

        public void AlinheCentralizado(string columnName)
        {
            Helper.AlinheCentralizado(columnName);
        }

        public void AlinheDireita(string columnName)
        {
            Helper.AlinheDireita(columnName);
        }

        public void Exiba(object dataSource)
        {
            bsObjetos.DataSource = dataSource;
            bsObjetos.ResetBindings(false);
        }

        public void SelecioneTodas()
        {
            SelecaoLinhas(true);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            SelecaoLinhas(true);
            AoClicarTodos?.Invoke();
        }

        private void btnNenhum_Click(object sender, EventArgs e)
        {
            LimpeSelecao();
        }

        public void LimpeSelecao()
        {
            SelecaoLinhas(false);
            AoClicarNenhum?.Invoke();
        }

        private void btnInverter_Click(object sender, EventArgs e)
        {
            if (_estaExibidoCheckBoxSelecao)
            {
                foreach (DataGridViewRow linha in dgGrid.Rows)
                {
                    InvertaLinhaCheckBox(linha);
                }
            }
            else
            {
                RemovaEventoDeSelecao();
                foreach (DataGridViewRow linha in dgGrid.Rows)
                {
                    linha.Selected = !linha.Selected;
                }
                AdicioneEventoDeSelecao();
            }

            AoMudarSelecao?.Invoke();
        }

        public void AdicioneItem(object item)
        {
            bsObjetos.Add(item);
            bsObjetos.ResetBindings(false);
        }

        public void AdicioneItensSemDuplicidades(object item)
        {
            if (bsObjetos.Contains(item))
            {
                return;
            }

            AdicioneItem(item);
        }

        public void RemovaItens(IEnumerable<object> itens)
        {
            foreach (var item in itens)
            {
                bsObjetos.Remove(item);
            }
            bsObjetos.ResetBindings(false);
        }

        public void RemovaItensSelecionados()
        {
            var itensSelecionados = ObtenhaObjetosSelecionados<object>();
            if (!itensSelecionados.Any())
            {
                return;
            }

            RemovaItens(itensSelecionados);
        }

        private void SelecaoLinhas(bool flag)
        {
            if (_estaExibidoCheckBoxSelecao)
            {
                SelecioneLinhasCheckBox(flag);
            }
            else
            {
                RemovaEventoDeSelecao();
                foreach (DataGridViewRow linha in dgGrid.Rows)
                {
                    linha.Selected = flag;
                }
                AdicioneEventoDeSelecao();
            }

            AoMudarSelecao?.Invoke();
        }

        public List<T> ObtenhaObjetosSelecionados<T>()
        {
            var objetosSelecionados = new List<T>();

            if (_estaExibidoCheckBoxSelecao)
            {
                foreach (DataGridViewRow linha in dgGrid.Rows)
                {
                    if (EstaSelecionada((DataGridViewCheckBoxCell)linha.Cells[0]))
                    {
                        if (linha.DataBoundItem is T item)
                        {
                            objetosSelecionados.Add(item);
                        }
                    }
                }
                return objetosSelecionados;
            }


            if (DataGridView.SelectionMode == DataGridViewSelectionMode.CellSelect)
            {
                foreach (DataGridViewCell celula in dgGrid.SelectedCells)
                {
                    if (dgGrid.Rows[celula.RowIndex].DataBoundItem is T item)
                    {
                        objetosSelecionados.AddSemDuplicidade(item);
                    }
                }
            }
            else
            {
                objetosSelecionados.AddRange(from DataGridViewRow linha in dgGrid.SelectedRows select (T)linha.DataBoundItem);
            }

            return objetosSelecionados;
        }

        public List<T> ObtenhaObjetosQueNaoEstaoSelecionados<T>()
        {
            var objetos = ObtenhaTodosObjetos<T>();
            var selecionados = ObtenhaObjetosSelecionados<T>();
            return objetos.Except(selecionados).ToList();
        }

        public void AtualizeDesenhoGrid()
        {
            bsObjetos.ResetBindings(false);
        }

        public void OculteBotoes()
        {
            VisibilidadeBotoes(false);
        }

        public void ExibaBotoes()
        {
            VisibilidadeBotoes(true);
        }

        public void ExibaSomenteLegenda()
        {
            VisibilidadeBotoes(true);
            btnInverter.Visible = false;
            btnNenhum.Visible = false;
            btnTodos.Visible = false;
        }

        private void VisibilidadeBotoes(bool ehParaApresentar)
        {
            panel1.Visible = ehParaApresentar;
        }

        public void RetireSelecaoLinhas()
        {
            SelecaoLinhas(false);
        }

        public void MostreLegenda(Color cor, string texto)
        {
            lblCorLegenda.Visible = true;
            lblCorLegenda.BackColor = cor;
            lblTextoLegenda.Visible = true;
            lblTextoLegenda.Text = texto;
        }

        public void OculteLegenda()
        {
            lblCorLegenda.Visible = false;
            lblTextoLegenda.Visible = false;
        }

        public void Limpe()
        {
            bsObjetos.Clear();
            bsObjetos.DataSource = null;
            bsObjetos.ResetBindings(false);
        }

        public List<T> ObtenhaTodosObjetos<T>()
        {
            return bsObjetos.ObtenhaObjetos<T>();
        }

        public void SelecioneObjetos<T>(IEnumerable<T> lista)
        {
            if (lista == null)
            {
                return;
            }

            foreach (DataGridViewRow linha in dgGrid.Rows)
            {
                var objeto = (T)linha.DataBoundItem;
                if (!lista.Contains(objeto))
                {
                    continue;
                }

                if (_estaExibidoCheckBoxSelecao)
                {
                    var celulaCheckBox = (DataGridViewCheckBoxCell)linha.Cells[0];
                    celulaCheckBox.Value = true;
                }
                else
                {
                    linha.Selected = true;
                }
            }
        }

        public bool ExisteAlgumItemNaoSelecionado()
        {
            return dgGrid.Rows.Count != dgGrid.SelectedRows.Count;
        }

        public bool GridEstaVazia()
        {
            return dgGrid.Rows.Count == 0;
        }

        public int Count()
        {
            return dgGrid.Rows.Count;
        }

        public bool ExistePeloMenosUmItemSelecionado()
        {
            return dgGrid.SelectedRows.Count > 0;
        }

        private void dgGrid_SelectionChanged_1(object sender, EventArgs e)
        {
            lblItensSelecionados.Visible = ExibirQuantidadeSelecionados;
            if (!ExibirQuantidadeSelecionados)
            {
                return;
            }

            if (dgGrid.SelectedRows.Count > 0)
            {
                lblItensSelecionados.Text = @"Itens Selecionados " + dgGrid.SelectedRows.Count;
            }
            else
            {
                lblItensSelecionados.Text = @"Nenhum Item Selecionado";
            }
        }

        public bool ReadOnly
        {
            get => dgGrid.ReadOnly;
            set => dgGrid.ReadOnly = value;
        }

        public bool MultiSelect
        {
            get => dgGrid.MultiSelect;
            set => dgGrid.MultiSelect = value;
        }

        public DataGridViewSelectionMode SelectionMode
        {
            get => dgGrid.SelectionMode;
            set => dgGrid.SelectionMode = value;
        }

        public void ReadOnlyWithMultiSelectRow()
        {
            ReadOnly = true;
            MultiSelect = true;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region Métodos de CheckBoxSelecao

        public void ExibaCheckBoxSelecao()
        {
            _estaExibidoCheckBoxSelecao = true;

            var coluna = Helper.CrieColunaCheckBox("", "", 20);
            dgGrid.Columns.Insert(0, coluna);

            dgGrid.CellContentClick += DataGridView_CellContentClick_CheckBoxSelecao;
            dgGrid.KeyDown += KeyDown_CheckBoxSelecao;
            AoClicarTodos += AoClicarTodos_CheckBoxSelecao;
            AoClicarNenhum += AoClicarNenhum_CheckBoxSelecao;

            MultiSelect = false;
        }

        private void KeyDown_CheckBoxSelecao(object sender, KeyEventArgs e)
        {
            if (dgGrid.CurrentCell.ColumnIndex == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Space)
            {
                var linha = dgGrid.CurrentRow;

                InvertaLinhaCheckBox(linha);

                e.Handled = true;
            }
        }

        private void InvertaLinhaCheckBox(DataGridViewRow linha)
        {
            var celulaCheckBox = (DataGridViewCheckBoxCell)linha.Cells[0];
            var celulaMarcada = EstaSelecionada(celulaCheckBox);
            celulaCheckBox.Value = !celulaMarcada;
            AoMudarSelecao?.Invoke();
        }

        private void AoClicarNenhum_CheckBoxSelecao()
        {
            SelecioneLinhasCheckBox(false);
        }

        private void AoClicarTodos_CheckBoxSelecao()
        {
            SelecioneLinhasCheckBox(true);
        }

        private void SelecioneLinhasCheckBox(bool flag)
        {
            foreach (DataGridViewRow linha in dgGrid.Rows)
            {
                var celulaCheckBox = (DataGridViewCheckBoxCell)linha.Cells[0];
                celulaCheckBox.Value = flag;
            }
        }

        private void DataGridView_CellContentClick_CheckBoxSelecao(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0)
            {
                return;
            }
            var linha = dgGrid.Rows[e.RowIndex];
            InvertaLinhaCheckBox(linha);
        }

        private bool EstaSelecionada(DataGridViewCheckBoxCell celulaCheckBox)
        {
            if (celulaCheckBox.Value == null)
            {
                return false;
            }

            bool.TryParse(celulaCheckBox.Value.ToString(), out var valor);
            return valor;
        }

        public bool EstaSelecionadaCheckBox(DataGridViewRow linha)
        {
            if (linha.Cells[0] is DataGridViewCheckBoxCell celula)
            {
                return EstaSelecionada(celula);
            }

            return false;
        }

        public void SelecioneLinha(DataGridViewRow linha)
        {
            if (_estaExibidoCheckBoxSelecao)
            {
                var celulaCheckBox = (DataGridViewCheckBoxCell)linha.Cells[0];
                celulaCheckBox.Value = true;
                return;
            }

            linha.Selected = true;
        }
        #endregion
    }
}
