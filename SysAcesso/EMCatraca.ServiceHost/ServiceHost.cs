using EMCatraca.Core.RemoteServices;
using EMCatraca.Server;
using EMCatraca.Server.Controladores;
using System.ComponentModel;
using System.ServiceProcess;

namespace EMCatraca.ServiceHost
{
    [RunInstaller(true)]
    public partial class ServiceHost : ServiceBase
    {
        public ServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ServiceHostLoader.IntercepteExcecaoNaoTratada();
            ServiceHostLoader.CarregueServicos();
            ControladorCatracaLoader.CarregueControlador();
        }

        protected override void OnStop()
        {
            ControladorCatracaLoader.PareControlador();
        }
    }
}
