using MdPaciente._2_Aplicacoes;
using MdPaciente._5_Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MdPaciente._1_Visao.Telas._2.Pacientes
{
    public partial class frmExecoes : Form
    {
        private readonly UtilitarioPaciente _utilitario = new UtilitarioPaciente();

        private readonly DtoConfiguracaoPaciente _dto = new DtoConfiguracaoPaciente();


        public frmExecoes()
        {
            InitializeComponent();
        }

        public frmExecoes(DtoConfiguracaoPaciente dto, string message)
        {
            InitializeComponent();

            txtTextoInformacoes.Text = message;
            _dto = dto;
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastro(_dto),_dto.PnUm);
        }
    }
}
