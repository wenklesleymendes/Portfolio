using System;

namespace EMCatraca.Core.Interfaces
{
    public interface Log
    {
        void Escreva(string message);

        void EscrevaError(string message, Exception ex);
    }
}
