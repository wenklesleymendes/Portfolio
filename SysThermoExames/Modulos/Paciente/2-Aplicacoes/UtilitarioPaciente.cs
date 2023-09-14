using MdPaciente._5_Dtos;
using System.Windows.Forms;

namespace MdPaciente._2_Aplicacoes
{
    public class UtilitarioPaciente
    {
        private DtoConfiguracaoPaciente _dto = new DtoConfiguracaoPaciente();
        
        public UtilitarioPaciente(DtoConfiguracaoPaciente dtoConfigPaciente)
        {
            _dto = dtoConfigPaciente;
        }

        public UtilitarioPaciente() { }

        public void AbrirFormPanel(object frmObjto, Panel pn)
        {
            if (pn.Controls.Count > 0)
            {
                pn.Controls.RemoveAt(0);
            }

            Form fh = frmObjto as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            pn.Controls.Add(fh);
            pn.Tag = fh;
            fh.Show();
        }
    }
}
