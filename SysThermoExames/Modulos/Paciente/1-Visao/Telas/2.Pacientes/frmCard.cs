using MdPaciente._1_Visao.Telas._2.Pacientes;
using MdPaciente._2_Aplicacoes;
using MdPaciente._3_Dominio;
using MdPaciente._5_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MdPaciente._1_Visao
{
    public partial class frmCard : Form
    {
        private readonly UtilitarioPaciente _utilitario = new UtilitarioPaciente();

        private readonly DtoConfiguracaoPaciente _dto = new DtoConfiguracaoPaciente();

        ProcessoPaciente MdPaciente = new ProcessoPaciente();

        List<Paciente> _cachePacientes = new List<Paciente>();

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);

        //    try
        //    {
        //        _cachePacientes = CarreguePacientes();
        //        MonteCardsPaciente();
        //    }
        //    catch (Exception ex)
        //    {
        //        _utilitario.AbrirFormularioPanelUm(new frmExecoes(_utilitario, ex.Message));
        //    }
        //}

        public frmCard()
        {
            InitializeComponent();

            _cachePacientes = CarreguePacientes();

            MonteCardsPaciente();
        }

        public frmCard(DtoConfiguracaoPaciente dtoConfPaciente)
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
