using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace EMCatraca.ServiceHost
{
    [RunInstaller(true)]
    public partial class InstallerService : System.Configuration.Install.Installer
    {
        private readonly ServiceInstaller serviceInstaller;
        private readonly ServiceProcessInstaller processInstaller;

        void InstallerService_AfterInstall(object sender, InstallEventArgs e)
        {
            var sc = new ServiceController(Constantes.SERVICE_NAME);
            sc.Start();
        }

        public InstallerService()
        {
            InitializeComponent();

            AfterInstall += new InstallEventHandler(InstallerService_AfterInstall);

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = Constantes.SERVICE_NAME;
            serviceInstaller.Description = "Hospeda os serviços do Escolar Manager CATRACA";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
