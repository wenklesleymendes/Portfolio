using Formularios.Telas._1_Principal;
using ModelPrincipal._1_Entidades;
using Processos.Operadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Formularios.Telas._4_Login
{
    public partial class frmListaOperadores : Form
    {
        ProcessoOperadores cadastroOperadores = new ProcessoOperadores();
        List<Operador> _cacheOperadores = new List<Operador>();

        frmMain FormularioPrincipal { get; set; }

        private static string acessar = "Acessar Sistema";

        public frmListaOperadores(frmMain main)
        {
            InitializeComponent();
            FormularioPrincipal = main;
            _cacheOperadores = CarregueOperadores();
            IniciarAutenticaçao();
        }

        private void IniciarAutenticaçao()
        {
            btnLstOperdoresNovo.Text = acessar;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<Operador> CarregueOperadores()
        {
            var todosPacientes = cadastroOperadores.ConsulteTodosOperadores();
            return todosPacientes.ToList();
        }

        private void frmListaOperadores_Load(object sender, EventArgs e)
        {
            _cacheOperadores = CarregueOperadores();
            InsertarFilas(_cacheOperadores);
        }


        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void InsertarFilas(List<Operador> operadores = null)
        {
            dgvLstOperadores.Rows.Clear();
            operadorBindingSource.Clear();
            operadorBindingSource.DataSource = operadores;
            dgvLstOperadores.Refresh();
        }

        public void AtualizeGrid()
        {
            dgvLstOperadores.Rows.Clear();
            operadorBindingSource.Clear();
            _cacheOperadores = CarregueOperadores();
            operadorBindingSource.DataSource = _cacheOperadores;
            dgvLstOperadores.Refresh();

        }

        private void dgvLstOperadores_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //var frm = Owner as FormMembresia;
            ////FormMembresia frm = new FormMembresia();

            //frm.txtid.Text = dgvLstOperadores.CurrentRow.Cells[0].Value.ToString();
            //frm.txtnombre.Text = dgvLstOperadores.CurrentRow.Cells[1].Value.ToString();
            //frm.txtapellido.Text = dgvLstOperadores.CurrentRow.Cells[2].Value.ToString();
            //this.Close();


            //var Login = new frmLoginOperador(FormularioPrincipal);
            //Operador operadorSelecionado = (Operador)dgvLstOperadores.CurrentRow.DataBoundItem;
            //Login.txtLoginOperador.Text = operadorSelecionado.Codigo.ToString();
            //Login.txtNome.Text = operadorSelecionado.Nome.ToString();
            //Login.txtLogin.Text = operadorSelecionado.Login;
            //Login.ShowDialog();
        }

        private void btnLstOperdoresNovo_Click(object sender, EventArgs e)
        {
            var chaveAcao = btnLstOperdoresNovo.Text;
            var nome  = ObtenhaLoginLinhaSelecionada();

            if (Equals(chaveAcao, acessar))
            {
                var frm = new frmLoginOperador();
                frm.ShowDialog();
            }
            else
            {
                frmOperador frm = new frmOperador(this);
                frm.ShowDialog();
            }

        }

        private string ObtenhaLoginLinhaSelecionada()
        {
            // criar rotinas para pegar nome do login e passar para login 

            return "";
        }

        private void btnLstOperadorEditar_Click(object sender, EventArgs e)
        {
            var frm = new frmOperador(this);
            if (dgvLstOperadores.SelectedRows.Count == 1)
            {
                Operador operadorSelecionado = (Operador)dgvLstOperadores.CurrentRow.DataBoundItem;
                frm.txtId.Text = operadorSelecionado.Codigo.ToString();
                frm.txtNome.Text = operadorSelecionado.Nome ?? "";
                frm.txtCpf.Text = operadorSelecionado.CPF.CpfRetorno ?? "";
                frm.txtLogin.Text = operadorSelecionado.Login ?? "";

                frm.ShowDialog();
            }
            else
                MessageBox.Show("Selecione um operador.");
        }

        private void btnLstOperadoresExcluir_Click(object sender, EventArgs e)
        {
            var frm = new frmOperador(this);
            if (dgvLstOperadores.SelectedRows.Count != 1)
            {
                MessageBox.Show("Selecione um operador.");
                return;
            }

            Operador operadorSelecionado = (Operador)dgvLstOperadores.CurrentRow.DataBoundItem;
            try
            {
                cadastroOperadores.ExcluaOperador(operadorSelecionado);
                AtualizeGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu o seguinte erro ao tentar excluir o operador:\n{ex.Message}");
            }

        }
    }
}
