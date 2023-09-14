using EMCatraca.Core;
using EMCatraca.Core.Logs;
using EMCatraca.Server.Interfaces;
using System;
using System.Reflection;

namespace EMCatraca.Server.Controladores
{
    public static class ControladorCatracaNeokorosLoader
    {
        private static IControladorDeCatracaNeokoros _controladorDeCatraca;
        private static string nomeAssembly = "EMCatraca.RegrasAcesso";
        private static string _messagenLog;
        private static string _nomeLog = "Auditoria";

        public static IControladorDeCatracaNeokoros CarregueControlador()
        {
            Assembly assembly;
            try
            {
                _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(CarregueControlador)}: " +
                                       $"Retorno IControladorDeCatracaNeokoros";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                assembly = Assembly.Load($"{nomeAssembly}, Version = 1.0.0.0, Culture = neutral");
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(_nomeLog, ex);

                throw new ApplicationException($"Erro ao carregar o Assembly {nomeAssembly}:", ex);
            }

            _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(CarregueControlador)}: " +
                                       $"Assembly {nomeAssembly} carregado com sucesso";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            CatracaLoader configuracao = MapeadorArquivoJson
                .CarreguerArquivoJson<CatracaLoader>("EmCatraca.Loader.cfg");

            Type integracao = assembly.GetType(configuracao.TipoIntegracao);

            IdentifiqueTipoIntegracao(integracao);

            _controladorDeCatraca = Controlador(integracao);

            if (_controladorDeCatraca != null)
            {
                _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(CarregueControlador)}: " +
                                       $"O controlador da catraca integracao {integracao} instanciada com sucesso!";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);
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
                        _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(IdentifiqueTipoIntegracao)}: " +
                                       $"{mensagemErro}";
                        AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                        throw new ApplicationException(mensagemErro);
                    }

                default:

                    _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(IdentifiqueTipoIntegracao)}: " +
                                       $"Integração {tipoIntegracao.Name} identificada com sucesso!";
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

                _messagenLog = $"{nameof(EMCatraca.Server.Controladores)}.{nameof(ControladorCatracaNeokorosLoader)}.{nameof(Controlador)}: " +
                                       $"{mensagemErro}";
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