using System;

namespace Acesso.Interfaces.Excecoes
{
    public class PessoaNaoIdentificadaException : ApplicationException
    {
        public PessoaNaoIdentificadaException(string codigo) : base($"Identificador de pessoa inválido {codigo}")
        {

        }
    }
}
