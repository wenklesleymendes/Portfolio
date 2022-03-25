using System;

namespace EMCatraca.Server.Excecoes
{
    public class AcessoNegadoException : ApplicationException
    {
        public AcessoNegadoException(string message) : base(message)
        {
        }


    }
}
