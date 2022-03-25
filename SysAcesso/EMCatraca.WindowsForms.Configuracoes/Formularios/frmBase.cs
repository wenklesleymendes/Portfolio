using EMCatraca.Configuracao;
using EMCatraca.WindowsForms.Configuracoes.Interfaces;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    public partial class FrmBase : Form, IFormBase
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public FrmBase()
        {
            InitializeComponent();

            //KeyUp += frmBase_KeyUp;
            slMensagemStatus.Text = string.Empty;
            slNomeFormulario.Text = GetType().Name;
            spbBarraProgresso.Visible = false;
            slAmbiente.Text = string.Empty;
            slAmbiente.ForeColor = Color.Red;
            slAmbiente.Visible = true;
        }

        public string NomeFuncao
        {
            get => lblFuncao.Text;
            set
            {
                lblFuncao.Text = value;
                lblFuncao.Width = Width - lblFuncao.Location.X;
                Text = value;
            }
        }

        public void ExibaStatus(string status)
        {
            slMensagemStatus.Text = status;
            Update();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            lblFuncao.Width = Width - lblFuncao.Location.X;
        }

        public void MauseMouveFormulario(MouseEventArgs eventoMouse)
        {
            if (eventoMouse.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                var msg = 0xA1;
                var wparam = 0x02;
                var lparam = 0;
                var hwnd = Handle;
                SendMessage(hwnd, msg, wparam, lparam);
            }
        }

        private void frmBase_MouseMove(object sender, MouseEventArgs e)
        {
            MauseMouveFormulario(e);
        }
    }
}
