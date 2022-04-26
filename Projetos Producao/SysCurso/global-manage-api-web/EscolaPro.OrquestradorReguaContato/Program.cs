using System;

namespace EscolaPro.OrquestradorReguaContato
{
    class Program
    {
        static void Main(string[] args)
        {
                new AtualizarFila().Working().GetAwaiter().GetResult();
                new DiparadorFila().Working().GetAwaiter().GetResult();
        }
    }
}
