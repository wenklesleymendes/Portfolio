using Acesso.Core;
using Acesso.Core.RemoteServices;
using Acesso.Core.Services;
using System;
using System.Reflection;

namespace Acesso.Interfaces
{
    public static class ControladorCatracaLoader
    {
        private static IControladorDeCatraca _controladorDeCatraca;

        public static void CarregueControlador()
        {
            Assembly assemblyRegrasAcesso;
            try
            {
                assemblyRegrasAcesso = Assembly.Load("Acesso.RegrasAcesso, Version = 1.0.0.0, Culture = neutral");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Não foi possível carregar o Assembly de RegrasAcesso", ex);
            }

            var configuracao = MapeadorArquivoJson.CarreguerArquivoJson<CatracaLoader>("Acesso.Loader.cfg");
            var tipoIntegracao = assemblyRegrasAcesso.GetType(configuracao.TipoIntegracao);
            if (tipoIntegracao == null)
            {
                throw new ApplicationException($"Não foi possível carregar o tipo configurado {configuracao.TipoIntegracao}");
            }

            Console.WriteLine("Tipo de integração: " + configuracao.TipoIntegracao);

            _controladorDeCatraca = InstancieControlador(tipoIntegracao);
            if (_controladorDeCatraca == null)
            {
                Console.WriteLine($"");
                Console.WriteLine($"Verifique se esta é uma integração NEOKOROS, se for ela deverá ser executada pela DLL com o Simulador.");
                //throw new ApplicationException($"O tipo configurado {configuracao.TipoIntegracao} não implementa a interface IControladorDeCatraca");
                return;
            }

            var servicoMonitorAcesso = ServiceFactory.Instance.Create<IServicoMonitorAcesso>();
            _controladorDeCatraca.Inicie(servicoMonitorAcesso);
        }

        private static IControladorDeCatraca InstancieControlador(Type tipoIntegracao)
        {
            try
            {
                return Activator.CreateInstance(tipoIntegracao) as IControladorDeCatraca;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Não foi possível possível instanciar o tipo {tipoIntegracao}", ex);
            }
        }

        public static void PareControlador()
        {
            _controladorDeCatraca?.Pare();
        }
    }
}
