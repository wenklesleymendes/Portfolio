using EMCatraca.Core.Logs;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;

namespace EMCatraca.Core.RemoteServices
{
    public static class ServiceHostLoader
    {
        private static bool _unhandledExceptionAssinado;

        public static void CarregueServicos()
        {
            IntercepteExcecaoNaoTratada();

            ContextConfig.IsInRemotingContext = true;

            var props = new Hashtable
            {
                ["port"] = ServiceFactory.Porta,
                ["typeFilterLevel"] = TypeFilterLevel.Full,
                ["name"] = "servicos"
            };

            LifetimeServices.LeaseTime = new TimeSpan(0, 10, 0);
            LifetimeServices.LeaseManagerPollTime = new TimeSpan(0, 1, 0);
            LifetimeServices.RenewOnCallTime = new TimeSpan(0, 5, 0);

            var binaryProvider = new BinaryServerFormatterSinkProvider { TypeFilterLevel = TypeFilterLevel.Full };
            var tcpCanal = new TcpServerChannel(props, binaryProvider);

            ChannelServices.RegisterChannel(tcpCanal, false);

            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteServiceFactory), "RemoteServiceFactory", WellKnownObjectMode.Singleton);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void IntercepteExcecaoNaoTratada()
        {
            if (_unhandledExceptionAssinado)
            {
                return;
            }

            AppDomain.CurrentDomain.UnhandledException += HandlerExcecoesNaoTratadas;

            _unhandledExceptionAssinado = true;
        }

        private static void HandlerExcecoesNaoTratadas(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            AuditoriaLog.EscrevaErro(nameof(ServiceHostLoader), ex);
        }
    }
}
