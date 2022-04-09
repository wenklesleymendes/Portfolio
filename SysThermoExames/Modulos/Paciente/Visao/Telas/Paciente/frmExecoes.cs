using MdPaciente.Aplicacoes;
using MdPaciente.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MdPaciente.Visao.Telas._2.Pacientes
{
    public partial class frmExecoes : Form
    {
        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();


        public frmExecoes()
        {
            InitializeComponent();
        }

        public frmExecoes(DtoConfiguracao dto, string message)
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
