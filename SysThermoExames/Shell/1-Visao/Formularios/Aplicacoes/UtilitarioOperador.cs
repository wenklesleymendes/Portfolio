using System;
using System.Windows.Forms;
using Formularios.Dtos;
using Formularios.Telas._4_Login;

namespace Formularios.Aplicacoes
{
    public class UtilitarioOperador
    {
        private DtoConfiguracaoOperador _dto = new DtoConfiguracaoOperador();

        public UtilitarioOperador(DtoConfiguracaoOperador dtoConfigOperador)
        {
            _dto = dtoConfigOperador;
        }

        public UtilitarioOperador() { }

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

        internal void AbrirFormPanel(object frmObjto)
        {
            var pn = new Panel();
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
