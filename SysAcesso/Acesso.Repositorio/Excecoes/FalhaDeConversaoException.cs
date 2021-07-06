using System;

namespace Acesso.Interfaces.Excecoes
{
    public class FalhaDeConversaoException : ApplicationException
    {
        public FalhaDeConversaoException(string codigo) : base($"Falha ao converte o código em inteiro {codigo}")
        {

        }
    }
}
