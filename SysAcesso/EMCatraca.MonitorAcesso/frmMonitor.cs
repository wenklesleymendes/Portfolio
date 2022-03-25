using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.RemoteServices;
using EMCatraca.Core.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EMCatraca.MonitorAcesso
{
    public partial class frmMonitor : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private ControladorDeControlesDeCatraca _controladorDeControlesDeCatraca;
        private IServicoMonitorAcesso _servicoMonitorAcesso;

        public frmMonitor()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;

            tlpPainel.Visible = false;
            tlpPainel.Dock = DockStyle.Fill;
            pnlAguarde.Visible = true;
            pnlAguarde.Dock = DockStyle.Fill;

            var tm = new Timer
            {
                Interval = 100
            };
            tm.Tick += (s, ev) => lblRelogio.Text = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");
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
            e.Cancel = MessageBox.Show("Tem certeza que deseja fechar?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _controladorDeControlesDeCatraca = new ControladorDeControlesDeCatraca
            {
                Panel = tlpPainel,
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

            _servicoMonitorAcesso = ServiceFactory.Instance.Create<IServicoMonitorAcesso>();
            _servicoMonitorAcesso.AoDispararEvento += eventProxy.LocallyHandleMessageArrived;

            var catracas = _servicoMonitorAcesso.ObtenhaCatracas();

            try
            {
                _controladorDeControlesDeCatraca.MonteCatracas(catracas);
            }
            catch (Exception ex)
            {
                lblAguarde.Text = ex.Message;
                lblAguarde.ForeColor = Color.Red;
                return;
            }

            tlpPainel.Visible = true;
            pnlAguarde.Visible = false;

        }

        private void EventProxy_AoDispararEvento(EventoCatraca evento)
        {
            LogAuditoria.Escreva($"Evento foi recebido pelo " +
                $"Cliente - Dispositivo({evento.Dispositivo.Codigo})",
                nameof(frmMonitor));

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
            btnRestaurar.Visible = true;
        }

        private void lblAguarde_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void tlpPainel_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MoverFormulario(e);
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            btnRestaurar.Visible = true;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
