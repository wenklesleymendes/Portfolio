using EMCatraca.Core.Dominio;
using System;
using ValidacaoExterna.Neokoros;

namespace EMCatraca.Server.Interfaces
{
    public interface IControladorDeCatracaNeokoros
    {
        Pessoa ConsulteCodigo(string codigo);

        TurmaMontada ConsulteTurmaMontada(string codigo);

        RetornoDeValidacaoDeAcesso ValideAcesso(string codigo, DateTime dataHora, int numeroTerminal);

        void RegistreGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro);

        void Pare();


    }
}
