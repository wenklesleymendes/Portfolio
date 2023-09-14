using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TcpIp;

namespace EMCatraca.MonitorAcesso
{
    public partial class frmMonitorNovo : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private ControladorDeControlesDeCatraca _controladorDeControlesDeCatraca;
        private TcpIpCliente cliente = new TcpIpCliente();

        public frmMonitorNovo()
        {
            InitializeComponent();

            tlpFotoPessoas.Visible = false;
            tlpFotoPessoas.Dock = DockStyle.Fill;

            var tm = new Timer
            {
                Interval = 100
            };
            tm.Tick += (s, ev) => txtRelogio.Text = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");
            tm.Start();

            this.KeyPreview = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Screen.AllScreens.Length > 1)
            {
                DialogResult result = MessageBox.Show("Foi detectado um segundo monitor!\nDeseja direcionar esta janela para o segundo monitor?", "Monitor de Acessos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.OK))
                {
                    this.StartPosition = FormStartPosition.Manual;
                    Screen screen = recuperaSegundoMonitor();
                    this.Location = screen.WorkingArea.Location;
                    this.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
                }
            }
        }

        public Screen recuperaSegundoMonitor()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }
            return null;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _controladorDeControlesDeCatraca = new ControladorDeControlesDeCatraca
            {
                Panel = tlpFotoPessoas,
                Catracas = new[] { ucCatraca1, ucCatraca2, ucCatraca3, ucCatraca4, ucCatraca5, ucCatraca6, ucCatraca7, ucCatraca8, ucCatraca9, ucCatraca10 },
                NumeroLinhas = 2,
                NumeroColunasPorLinha = 5
            };

            Application.DoEvents();

            //Consulta No Servidor
            System.Threading.Thread.Sleep(1000);

            var eventProxy = new EventProxy();
            eventProxy.InicieCanal();
            eventProxy.AoDispararEvento += EventProxy_AoDispararEvento;

            cliente.Cliente_Change += Client_Change;

            try
            {
                var catracas = MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");
                var servidor = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");
                _controladorDeControlesDeCatraca.MonteCatracas(catracas);
                cliente.Iniciar(servidor.IP, servidor.PortaTcpIp, catracas);
            }
            catch (Exception ex)
            {
                pnlTitulo.Text = ex.Message;
                pnlTitulo.ForeColor = Color.Red;
                return;
            }

            tlpFotoPessoas.Visible = true;
        }

        private void Client_Change(object sender, EventArgs e)
        {
            EmitirStatus(cliente.resultado);
        }

        delegate void EmitirStatusCallback(string resultado);

        void EmitirStatus(string resultado)
        {
            try
            {
                if (InvokeRequired)
                {
                    EmitirStatusCallback callback = EmitirStatus;
                    Invoke(callback, resultado);
                }
                else
                {
                    this.Show();
                    if (!string.IsNullOrEmpty(resultado))
                    {
                        //Trace.WriteLog(resultado + "\n\r");
                        AtualizaEstadoAtual(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(nameof(frmMonitor), ex );
            }
        }
        private void AtualizaEstadoAtual(string comando)
        {
            var evento = JsonConvert.DeserializeObject<EventoCatraca>(comando);
            _controladorDeControlesDeCatraca.DispareEvento(evento);
        }

        private void EventProxy_AoDispararEvento(EventoCatraca evento)
        {
            _controladorDeControlesDeCatraca.DispareEvento(evento);
        }

        private void ImgFormClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void panelCabecalho_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void MoverFormulario(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

            WindowState = FormWindowState.Maximized;
        }

        private void tlpPainel_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pnlCorpo_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }
    }
}
