using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Server;
using System;
using System.Collections.Generic;
using ValidacaoExternaCatraca.Terabyte.ValidacoesDeAcesso;

namespace EMCatraca.Neokoros
{
    public abstract class ControladorDeCatracaNeokoros : IControladorDeCatracaNeokoros
    {
        protected abstract CatracaNeokorosAbstract CrieCatracaNeokoros(Catraca catraca);

        public Pessoa ConsulteCodigo(string codigo)
        {
            var _catracaNeokoros = CrieCatracaNeokoros(new Catraca() { Codigo = 0 });
            return _catracaNeokoros.ConsultePessoa(codigo);
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string codigo, DateTime dataHora, int numeroTerminal)
        {
            var _catracaNeokoros = CrieCatracaNeokoros(new Catraca() { Codigo = numeroTerminal });
            return _catracaNeokoros.ValideAcesso(codigo);
        }

        public void RegistreGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            var _catracaNeokoros = CrieCatracaNeokoros(new Catraca() { Codigo = numeroTerminal });
            _catracaNeokoros.RegisteAcesso(codigo, dataHora, numeroTerminal, direcaoGiro);
        }

        public void Pare()
        {
            throw new NotImplementedException();
        }
    }
}
