using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace EMCatraca.ServiceHost
{
    static class Program
    {
        static void Main(string[] args)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.GetDirectoryName(path);
            System.IO.Directory.SetCurrentDirectory(path);

            if (Environment.UserInteractive)
            {
                var parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--start":
                        StartService(Constantes.SERVICE_NAME);
                        break;
                    case "--stop":
                        StopService(Constantes.SERVICE_NAME);
                        break;
                    case "--install":
                        if (IsServiceInstalled(Constantes.SERVICE_NAME))
                        {
                            StartService(Constantes.SERVICE_NAME);
                        }
                        else
                        {
                            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        }
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                }
            }
            else
            {
                ServiceBase.Run(new ServiceHost());
            }
        }

        static void StartService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            try
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(2));
            }
            catch { }
        }

        static void StopService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(2));
            }
            catch { }
        }

        static bool IsServiceInstalled(string serviceName)
        {
            var services = ServiceController.GetServices();
            foreach (var service in services)
            {
                if (service.ServiceName == serviceName)
                    return true;
            }
            return false;
        }
    }

}
