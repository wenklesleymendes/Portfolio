using System;

namespace Acesso.Interfaces.Excecoes
{
    public class AcessoNegadoException : ApplicationException
    {
        public AcessoNegadoException(string message) : base(message)
        {
        }


    }
}
