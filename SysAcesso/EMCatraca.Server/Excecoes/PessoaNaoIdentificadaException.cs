using System;

namespace EMCatraca.Server.Excecoes
{
    public class PessoaNaoIdentificadaException : ApplicationException
    {
        public PessoaNaoIdentificadaException(string codigo) : base($"Identificador de pessoa inválido {codigo}")
        {

        }
    }
}
