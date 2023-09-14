using Formularios.Telas._8_Controles;
using Formularios.Telas.Principal;
using MdPaciente.Aplicacoes;
using ModelPrincipal;
using ModelPrincipal.Entidades;
using Processos.Operadores;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Formularios.Telas.Login
{
    public partial class frmCardOperador : Form
    {
        private readonly UtilitarioShell _utilitario = new UtilitarioShell();

        private readonly DtoConfigShell _dto = new DtoConfigShell();

        private readonly ProcessoOperadores _processoOperdores = new ProcessoOperadores();

        List<Operador> _cacheOperador = new List<Operador>();

        public frmCardOperador(ModelPrincipal.DtoConfigShell dto)
        {
            InitializeComponent();
            _dto = dto;

            _cacheOperador = CarregueOperadores();
            MonteCardsPaciente();
        }

        private void MonteCardsPaciente()
        {
            flpCards.Controls.Clear();

            foreach (var operador in _cacheOperador)
            {
                if(operador.EhAtivo == true)
                {
                  flpCards.Controls.Add(new ucOperadorCard(operador, _dto));
                }
            }
        }

        private List<Operador> CarregueOperadores()
        {
            var todosOperador = _processoOperdores.ConsulteTodosOperadores();
            return todosOperador;
        }

        private void pbAdd_Click(object sender, System.EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastroOperador(_dto), _dto.PnCentral);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();    
            frmMain.ShowDialog();
        }

        private void pbRemove_Click(object sender, EventArgs e)
        {
            var operadorParaExcluir = operadoresSeleciondos();
            if (operadorParaExcluir.Count>1)
            {
                MessageBox.Show("Selecione apenas um operador para excluir");
            }
            if (operadorParaExcluir.Count < 1)
            {
                MessageBox.Show("Selecione um operador para excluir");
            }
            if(operadorParaExcluir.Count == 1)
            {
                var Operador = operadorParaExcluir[0].GetOperador();
                _processoOperdores.ExcluaOperador(Operador.Codigo);
                _cacheOperador = CarregueOperadores();
                MonteCardsPaciente();
                
            }

        }
        private List<ucOperadorCard> operadoresSeleciondos()
        {
            List<ucOperadorCard> operadoresSeleciondos = new List<ucOperadorCard>();
            var cards = flpCards.Controls;
            foreach (var card in cards)
            {
                ucOperadorCard teste = (ucOperadorCard)card;
                if (teste.selecionado)
                {
                    operadoresSeleciondos.Add(teste);
                }
            }
            return operadoresSeleciondos;
        }

        private void pbEditar_Click(object sender, EventArgs e)
        {
            var operadorParaEditar = operadoresSeleciondos();
            if (operadorParaEditar.Count > 1)
            {
                MessageBox.Show("Selecione apenas um operador para excluir");
            }
            if (operadorParaEditar.Count < 1)
            {
                MessageBox.Show("Selecione um operador para excluir");
            }
            if (operadorParaEditar.Count == 1)
            {
                var operador = operadorParaEditar[0].GetOperador();
                _utilitario.AbrirFormPanel(new frmCadastroOperador(operador,_dto), _dto.PnCentral);


            }
        }
    }
}
