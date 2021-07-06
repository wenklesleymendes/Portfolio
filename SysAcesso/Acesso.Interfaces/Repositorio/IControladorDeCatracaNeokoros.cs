using  Acesso.Dominio;
using System;
using ValidacaoExterna.Neokoros;

namespace Acesso.Interfaces
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
