using EMCatraca.Core.Dominio;
using System;

namespace EMCatraca.Core.RemoteServices
{
    public class ServiceFactory : IServiceFactory
    {
        internal static readonly int Porta = 8095;

        public static ServiceFactory Instance = new ServiceFactory();

        private IServiceFactory serviceFactory = null;

        private ServiceFactory()
        {

            if (ContextConfig.IsInRemotingContext)
            {
                serviceFactory = LocalServiceFactory.Instance;
            }
            else
            {
                serviceFactory = (IServiceFactory)Activator.GetObject(typeof(RemoteServiceFactory), GetRemoteFactoryLocations());
            }
        }

        private string GetRemoteFactoryLocations()
        {
            var servidor = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");
            return $"tcp://localhost:{Porta}/RemoteServiceFactory";
        }

        public T Create<T>(params object[] args) where T : IService
        {
            return serviceFactory.Create<T>(args);
        }
    }
}
