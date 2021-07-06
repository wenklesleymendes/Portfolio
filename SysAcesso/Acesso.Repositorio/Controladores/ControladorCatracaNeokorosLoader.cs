using Acesso.Core;
using System;
using System.Reflection;

namespace Acesso.Interfaces
{
    public static class ControladorCatracaNeokorosLoader
    {
        private static IControladorDeCatracaNeokoros _controladorDeCatraca;
        private static string nomeAssembly = "Acesso.RegrasAcesso";

        public static IControladorDeCatracaNeokoros CarregueControlador()
        {
            Assembly assembly;
            try
            { 
                AuditoriaLog.WriteLog($"Carregando Assembly {nomeAssembly}", "CFG");

                assembly = Assembly.Load($"{nomeAssembly}, " +
                    $"Version = 1.0.0.0, Culture = neutral");
            }
            catch (Exception ex)
            {
                string labelErro = $"Erro ao carregar o Assembly {nomeAssembly}:";
                AuditoriaLog.WriteLog($"{labelErro}: {ex.Message}", "CFG");
                throw new ApplicationException(labelErro,ex);
            }

            AuditoriaLog.WriteLog($"Assembly {nomeAssembly} " +
                $"carregado com sucesso", "CFG");

            CatracaLoader configuracao = MapeadorArquivoJson
                .CarreguerArquivoJson<CatracaLoader>("Acesso.Loader.cfg");

            Type integracao = assembly.GetType(configuracao.TipoIntegracao);

            IdentifiqueTipoIntegracao(integracao);

            _controladorDeCatraca = Controlador(integracao);

            if (_controladorDeCatraca != null)
            {
                AuditoriaLog.WriteLog($"O controlador da catraca integracao {integracao} " +
                    $"instanciada com sucesso!", "CFG");
            }

            return _controladorDeCatraca;
        }

        private static void IdentifiqueTipoIntegracao(Type tipoIntegracao)
        {
            switch (tipoIntegracao)
            {
                case null:
                    {
                        var mensagemErro = "Não foi indetificado integração";
                        AuditoriaLog.WriteLog(mensagemErro, "CFG");
                        throw new ApplicationException(mensagemErro);
                    }

                default:

                    AuditoriaLog.WriteLog($"Integração {tipoIntegracao.Name} " +
                        $"identificada com sucesso!","CFG");
                    break;
            }
        }

        private static IControladorDeCatracaNeokoros Controlador(Type integracao)
        {
            try
            {
                return Activator.CreateInstance(integracao) as IControladorDeCatracaNeokoros;
            }
            catch (Exception ex)
            {
                var mensagemErro = $"Erro ao instanciar controlador de catraca " +
                    $"integraçao {integracao.Name} ";

                AuditoriaLog.WriteLog(mensagemErro, "CFG");

                throw new ApplicationException(mensagemErro, ex);
            }
        }

        public static void PareControlador()
        {
            //_controladorDeCatraca?.Pare();
        }
    }
}