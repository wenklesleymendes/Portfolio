using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Negocio.Enumeradores;
using EMCatraca.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ValidacaoExterna.Neokoros;

namespace EMCatraca.Neokoros
{
    public abstract class ControladorDeCatracaNeokoros : IControladorDeCatracaNeokoros
    {
        protected abstract CatracaNeokorosAbstract CrieCatracaNeokoros(Dispositivo catraca);
        private ConfiguracoesDto _config = new ConfiguracoesDto();


        public Pessoa ConsulteCodigo(string codigo)
        {
            LogAuditoria.Escreva($"{nameof(ConsulteCodigo)}" +
                   $"Retorna Pessoa",
                    $"{nameof(ControladorDeCatracaNeokoros)}{codigo}");

            var dispositivos = EnumeradorTipoCFG.Dispositivo.Descricao;
            _config.TodosDispositivos = MapeadorArquivoJson
                .CarreguerJson<List<Dispositivo>>(dispositivos, _config);

            if (_config.TodosDispositivos.Any())
            {
                LogAuditoria.Escreva($"{nameof(ConsulteCodigo)}" +
                  $"Dispositivos carregado e mapeado com sucesso!",
                   $"{nameof(ControladorDeCatracaNeokoros)}{codigo}");

                CatracaNeokorosAbstract _catracaNeokoros = CrieCatracaNeokoros(_config.TodosDispositivos.First());

                LogAuditoria.Escreva($"{nameof(ConsulteCodigo)}" +
                  $"{nameof(CrieCatracaNeokoros)}: Objeto instanciado com sucesso!",
                   $"{nameof(ControladorDeCatracaNeokoros)}{codigo}");

                return _catracaNeokoros.ConsultePessoa(codigo);
            }

            return null;
        }

        public TurmaMontada ConsulteTurmaMontada(string codigo)
        {
            CatracaNeokorosAbstract _catracaNeokoros = CrieCatracaNeokoros(new Dispositivo()
            {
                Codigo = 1,
                Descricao = "CatracaNeokoros",
                EhGiroInvertido = false
            });

            return _catracaNeokoros.ConsulteTurmaMontada(codigo);
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string codigo, DateTime dataHora, int numeroTerminal)
        {
            CatracaNeokorosAbstract _catracaNeokoros = CrieCatracaNeokoros(new Dispositivo()
            {
                Codigo = 1,
                Descricao = "CatracaNeokoros",
                EhGiroInvertido = false
            });

            return _catracaNeokoros.ValideAcesso(codigo);
        }

        public void RegistreGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            CatracaNeokorosAbstract _catracaNeokoros = CrieCatracaNeokoros(new Dispositivo()
            {
                Codigo = numeroTerminal
            });

            _catracaNeokoros.RegisteAcesso(codigo, dataHora, numeroTerminal, direcaoGiro);
        }

        public void Pare()
        {
            throw new NotImplementedException();
        }
    }
}