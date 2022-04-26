using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Simuladores.Henrry;
using EMCatraca.Simuladores.Neokoros;
using EMCatraca.Simuladores.SimuladorTopData.UI.FrmOnline;
using EMCatraca.WindowsForms.Configuracoes.Formularios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EMCatraca.Simuladores
{
    public partial class frmMenuDebug : Form
    {
        private int _contador;
        private int _proximoPontoX;

        public frmMenuDebug()
        {
            InitializeComponent();
        }

        private void btnSimuladorTcip_Click(object sender, EventArgs e)
        {
            var catracas = MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");

            if (catracas.Count > 0)
            {
                foreach (var catraca in catracas)
                {
                    _proximoPontoX = _proximoPontoX + 10;
                    var totalFrmAberto = Application.OpenForms.OfType<frmSimuladorCatracasHenrry>().Count();
                    if (totalFrmAberto < catracas.Count)
                    {
                        _contador++;
                        var frm = new frmSimuladorCatracasHenrry(catraca, _contador);
                        frm.Show();
                        frm.Location = new Point(Width + _proximoPontoX, Location.Y);
                        _proximoPontoX = Location.X;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                AbrirFrmConfiguracaoAcesso();
            }
        }

        private void AbrirFrmConfiguracaoAcesso()
        {
            Visible = false;
            var ehAdministrador = true;

            var frm = new FrmConfiguraAcesso(ehAdministrador);
            frm.ShowDialog();

            Visible = true;
        }

        private void btnExterno_Click(object sender, EventArgs e)
        {
            var frm = new frmSimuladorNeokoros();
            frm.Show();
        }

        private void btnConfigurarAcesso_Click(object sender, EventArgs e)
        {
            AbrirFrmConfiguracaoAcesso();
        }

        private void btnSimuladorCatracaTopData_Click(object sender, EventArgs e)
        {
            var frm = new FrmOnline();
            frm.ShowDialog();
        }
    }
}
