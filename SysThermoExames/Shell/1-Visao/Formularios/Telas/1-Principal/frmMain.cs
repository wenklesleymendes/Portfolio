using Autofac;
using Formularios.Telas._4_Login;
using Formularios.Telas._6_Consultas;
using Formularios.Telas._7_Logo;
using ModelPrincipal._1_Entidades;
using ModelPrincipal._3_Utilitarios;
using Processos.Configuracoes;
using Repositorio.IoC;
using Shell.Modulos;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formularios.Telas._1_Principal
{
    public partial class frmMain : Form
    {
        public Operador OperadorLogado { get; set; }

        private static ConfiguracaoModulo _configuracao { get; set; }

        private static ProcessoConfiguracao _processoConfiguracao => new ProcessoConfiguracao();

        private static IContainer _container { get; set; }

        public frmMain()
        {
            InitializeComponent();

            //Estas lineas eliminan los parpadeos del formulario o controles en la interfaz grafica (Pero no en un 100%)
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MostrarFormModuloPacientes();
        }

        //METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO  TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(ClientRectangle.Width - tolerance, ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            pnContenerPrincipal.Region = region;
            Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(55, 61, 69));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO------------------------------------------------------
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = Location.X;
            ly = Location.Y;
            sw = Size.Width;
            sh = Size.Height;
            Size = Screen.PrimaryScreen.WorkingArea.Size;
            Location = Screen.PrimaryScreen.WorkingArea.Location;
            btnMaximizar.Visible = false;
            btnNormal.Visible = true;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            Size = new Size(sw, sh);
            Location = new Point(lx, ly);
            btnNormal.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja fechar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        private void AbrirPanelCentral(object formHijo)
        {
            if (pnContenerForm.Controls.Count > 0)
            {
                pnContenerForm.Controls.RemoveAt(0);
            }

            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            pnContenerForm.Controls.Add(fh);
            pnContenerForm.Tag = fh;
            fh.Show();
        }

        //METODO PARA MOSTRAR FORMULARIO DE LOGO Al INICIAR ----------------------------------------------------------
        private void MostrarFormLogo()
        {
            AbrirPanelCentral(new fmLogo());
        }

        private void MostrarFormModuloPacientes()
        {
            var diretorio = Environment.CurrentDirectory;
            var path = $"{diretorio}\\modulosAdicionais";

            var assembliesSelecionados =
                  from arquivo in new DirectoryInfo(path).GetFiles()
                  where arquivo.Name == ("MdPaciente.exe")
                  select Assembly.LoadFrom(arquivo.FullName);

            if (assembliesSelecionados is null || !assembliesSelecionados.Any())
            {
                MessageBox.Show("O módulo selecionado não foi encontrado.\n" + "Verfique se está instalado na pasta correta.");
                return;
            }

            ContainerBuilder builder = new ContainerBuilder();

            foreach (var assembly in assembliesSelecionados)
            {
                builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().FindConstructorsWith(new AllConstructorFinder());
                builder.RegisterAssemblyModules(assembly);
            }

            _container = builder.Build();
            var lista = _container.ComponentRegistry.Registrations.ToList();
            var teste2 = lista.FirstOrDefault(l => l.Activator.LimitType.Name.Equals("frmCard"));
            var scope = _container.BeginLifetimeScope();

            {
                try
                {
                    var mod = scope.Resolve<IModulo>();
                    AbrirPanelCentral(mod.FrmMainPaciente());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível abrir pelo seguinte motivo:\n" + ex.Message);
                }
            }
        }

        //METODO PARA MOSTRAR FORMULARIO DE LOGO Al CERRAR OTROS FORM ----------------------------------------------------------
        private void MostrarFormLogoAlCerrarForms(object sender, FormClosedEventArgs e)
        {
            MostrarFormLogo();
        }

        //METODOS PARA ABRIR OTROS FORMULARIOS Y MOSTRAR FORM DE LOGO Al CERRAR ----------------------------------------------------------
        private void btnListaPacientes_Click(object sender, EventArgs e)
        {
            frmListaPadrao fm = new frmListaPadrao();
            fm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirPanelCentral(fm);
        }

        private void btnMainOperadores_Click(object sender, EventArgs e)
        {
            var fm = new frmListaOperadores(this);
            fm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirPanelCentral(fm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja fechar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Size = new Size(sw, sh);
            Location = new Point(lx, ly);
            btnNormal.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMembresia_Click(object sender, EventArgs e)
        {
            frmConsultaPadrao frm = new frmConsultaPadrao();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirPanelCentral(frm);
        }

        //METODO PARA HORA Y FECHA ACTUAL ----------------------------------------------------------
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
        }
    }
}
