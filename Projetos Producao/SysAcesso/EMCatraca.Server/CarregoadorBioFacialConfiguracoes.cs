using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Server.Interfaces;
using System;
using System.Reflection;

namespace EMCatraca.Server
{
    public static class CarregadorBioFacialConfiguracoes
    {
        private static IControladorDeCatracaNeokoros _iCarregadorNeokoros;
        private static readonly string assemblyRegraAcesso = $"EMCatraca.{nameof(RegrasAcesso)}";
        private const string CarregadorJson = "EmCatraca.Loader.cfg";
        private static string _messagenLog;
        private static string _nomeLog = "Auditoria";

        public static IControladorDeCatracaNeokoros CarregueAssemblys()
        {
            Assembly assembly;
            try
            {
                _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: "+
                    $"Carregando Assembly {assemblyRegraAcesso}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                assembly = Assembly.Load($"{assemblyRegraAcesso}, Version = 1.0.0.0, Culture = neutral");
            }
            catch (Exception ex)
            {
                string labelErro = $"Erro ao carregar o Assembly {assemblyRegraAcesso}:";

                _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                    $"{labelErro}: {ex.Message}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                throw new ApplicationException(labelErro, ex);
            }

            _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                    $"Assembly {assemblyRegraAcesso} carregado com sucesso";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            CatracaLoader configuracao = MapeadorArquivoJson.CarreguerArquivoJson<CatracaLoader>(CarregadorJson);

            Type integracao = assembly.GetType(configuracao.TipoIntegracao);

            IdentifiqueTipoIntegracao(integracao);

            _iCarregadorNeokoros = Controlador(integracao);

            if (_iCarregadorNeokoros != null)
            {
                _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                    $"O controlador da catraca " +
                    $"integracao {integracao} instanciada com sucesso!";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);
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

                        _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                        $"{mensagemErro}, CFG";
                        AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                        throw new ApplicationException(mensagemErro);
                    }

                default:

                    _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                        $"Integração {tipoIntegracao.Name} " +
                        $"identificada com sucesso!";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);

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

                _messagenLog = $"{nameof(EMCatraca.Server)}.{nameof(CarregadorBioFacialConfiguracoes)}.{nameof(CarregueAssemblys)}: " +
                        $"{mensagemErro}, CFG: {ex}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                throw new ApplicationException(mensagemErro, ex);
            }
        }

        public static void PareControlador()
        {
            //_controladorDeCatraca?.Pare();
        }
    }
}