using MdPaciente.Dtos;
using System.Windows.Forms;

namespace MdPaciente.Aplicacoes
{
    public class Utilitario
    {
        private DtoConfiguracao _dto = new DtoConfiguracao();
        
        public Utilitario(DtoConfiguracao dtoConfigPaciente)
        {
            _dto = dtoConfigPaciente;
        }

        public Utilitario() { }

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
