using System;
using System.Runtime.InteropServices;

namespace ValidacaoExterna
{
    [Guid("6761462A-4CFA-473B-AE3E-D5C9E07B8F6B")]
    public interface IValidador
    {
        [DispId(1)]
        bool ConsultarCodigo(string codigo, out string nome);

        [DispId(2)]
        bool ValidarAcesso(string codigo, DateTime dataHora, int numTerminal,
                            out string mensagem1, out string mensagem2,
                            out int acessoEsperado);
        [DispId(3)]
        void RegistrarGiro(string codigo, DateTime dataHora, int numTerminal, int direcao);
    }
    [Guid("DA61C026-DA00-4385-A317-65CE807B5CD1"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface Validador_Events
    {
    }
}
