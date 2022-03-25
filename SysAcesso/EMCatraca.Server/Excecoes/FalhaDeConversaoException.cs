using System;

namespace EMCatraca.Server.Excecoes
{
    public class FalhaDeConversaoException : ApplicationException
    {
        public FalhaDeConversaoException(string codigo) : base($"Falha ao converte o código em inteiro {codigo}")
        {

        }
    }
}
