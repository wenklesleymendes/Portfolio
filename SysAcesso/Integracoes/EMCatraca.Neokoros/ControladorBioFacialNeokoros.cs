using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System;
using ValidacaoExterna.Neokoros;

namespace EMCatraca.Neokoros
{
    public abstract class ControladorBioFacialNeokoros : IControladorDeCatracaNeokoros
    {
        protected abstract BioFacialNeokorosAbstract CrieBioFacailNeokoros();

        public Pessoa ConsulteCodigo(string codigo)
        {
            BioFacialNeokorosAbstract _catracaNeokoros = CrieBioFacailNeokoros();
            return _catracaNeokoros.ConsultePessoa(codigo);
        }

        public TurmaMontada ConsulteTurmaMontada(string codigo)
        {
            BioFacialNeokorosAbstract _catracaNeokoros = CrieBioFacailNeokoros();
            return _catracaNeokoros.ConsulteTurmaMontada(codigo);
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string codigo, DateTime dataHora, int numeroTerminal)
        {
            BioFacialNeokorosAbstract _catracaNeokoros = CrieBioFacailNeokoros();
            return _catracaNeokoros.ValideAcesso(codigo,dataHora,numeroTerminal);
        }

        public void RegistreGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            BioFacialNeokorosAbstract _catracaNeokoros = CrieBioFacailNeokoros();
            _catracaNeokoros.RegistreAcesso(codigo, dataHora, numeroTerminal, direcaoGiro);
        }

        public void Pare()
        {
            throw new NotImplementedException();
        }
    }
}