using MdPaciente.Visao.Telas._2.Pacientes;
using MdPaciente.Aplicacoes;
using MdPaciente.Dominio;
using MdPaciente.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MdPaciente.Visao
{
    public partial class frmCard : Form
    {
        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();

        ProcessoPaciente MdPaciente = new ProcessoPaciente();

        List<Paciente> _cachePacientes = new List<Paciente>();

        public frmCard()
        {
            InitializeComponent();

            _cachePacientes = CarreguePacientes();

            MonteCardsPaciente();
        }

        public frmCard(DtoConfiguracao dtoConfPaciente)
        {
            InitializeComponent();

            _cachePacientes = CarreguePacientes();
            _dto = dtoConfPaciente;

            MonteCardsPaciente();
        }


        private void MonteCardsPaciente()
        {
            flpCards.Controls.Clear();

            foreach (var paciente in _cachePacientes)
            {
                flpCards.Controls.Add(new ucPacienteCard(paciente,_dto));
            }
        }


        private List<Paciente> CarreguePacientes()
        {
            var todosPacientes = MdPaciente.ConsulteTodosPacientes();
            return todosPacientes.ToList();
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastro(_dto), _dto.PnUm);
        }

        private void pbRemove_Click(object sender, EventArgs e)
        {
            if (_dto.ExisteCardSelecionadoPaciente)
            {
                var messangem = $"Tem certeza deseja excluir o Paciente:";
                _utilitario.AbrirFormPanel(new frmExecoes(_dto, messangem), _dto.PnDois);
            }
            else
            {
                var messangem = "Não existe paciente selecionado para excluir";
                _utilitario.AbrirFormPanel(new frmExecoes(_dto, messangem), _dto.PnDois);
            }
        }
    }
}
