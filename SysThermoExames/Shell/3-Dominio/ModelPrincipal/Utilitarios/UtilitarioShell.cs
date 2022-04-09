using ModelPrincipal;
using System.Windows.Forms;

namespace MdPaciente.Aplicacoes
{
    public class UtilitarioShell
    {
        private DtoConfigShell _dto = new DtoConfigShell();
        
        public UtilitarioShell(DtoConfigShell dtoConfigPaciente)
        {
            _dto = dtoConfigPaciente;
        }

        public UtilitarioShell() { }

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
