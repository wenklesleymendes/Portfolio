using Acesso.Dominio;
using System;
using System.Reflection;

namespace Acesso.Interfaces
{
    public static class CarregadorBioFacialConfiguracoes
    {
        private static IControladorDeCatracaNeokoros _iCarregadorNeokoros;
        private static readonly string assemblyRegraAcesso = $"Acesso.{nameof(RegrasAcesso)}";
        private const string CarregadorJson = "Acesso.Loader.cfg";

        public static IControladorDeCatracaNeokoros CarregueAssemblys()
        {
            Assembly assembly;
            try
            {
                AuditoriaLog.WriteLog($"Carregando Assembly {assemblyRegraAcesso}", "cfg");
                assembly = Assembly.Load($"{assemblyRegraAcesso}, Version = 1.0.0.0, Culture = neutral");
            }
            catch (Exception ex)
            {
                string labelErro = $"Erro ao carregar o Assembly {assemblyRegraAcesso}:";
                AuditoriaLog.WriteLog($"{labelErro}: {ex.Message}", "CFG");
                throw new ApplicationException(labelErro, ex);
            }

            AuditoriaLog.WriteLog($"Assembly {assemblyRegraAcesso} " +
                $"carregado com sucesso", "CFG");

            CatracaLoader configuracao = MapeadorArquivoJson.CarreguerArquivoJson<CatracaLoader>(CarregadorJson);

            Type integracao = assembly.GetType(configuracao.TipoIntegracao);

            IdentifiqueTipoIntegracao(integracao);

            _iCarregadorNeokoros = Controlador(integracao);

            if (_iCarregadorNeokoros != null)
            {
                AuditoriaLog.WriteLog($"O controlador da catraca integracao {integracao} " +
                    $"instanciada com sucesso!", "CFG");
            }

            return _iCarregadorNeokoros;
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
                        $"identificada com sucesso!", "CFG");
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