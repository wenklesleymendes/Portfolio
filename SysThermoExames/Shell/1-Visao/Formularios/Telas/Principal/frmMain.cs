using Autofac;
using Formularios.Telas.Login;
using MdPaciente.Aplicacoes;
using ModelPrincipal;
using ModelPrincipal.Entidades;
using Repositorio.IoC;
using Shell.Modulos;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Formularios.Telas.Principal
{
    public partial class frmMain : Form
    {
        public Operador OperadorLogado { get; set; }

        private static IContainer _container { get; set; }

        private readonly UtilitarioShell _utilitario = new UtilitarioShell();

        private readonly DtoConfigShell _dto = new DtoConfigShell();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _dto.PnCentral = pnContenerForm;
            MostrarFormulariosModulos();
        }

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

        private void MostrarFormulariosModulos()
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja fechar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnOperadores_Click(object sender, EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCardOperador(_dto), _dto.PnCentral);
        }
    }
}
