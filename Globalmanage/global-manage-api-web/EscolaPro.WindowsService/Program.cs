namespace EscolaPro.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            new ItauArquivoRemessaService().Working().GetAwaiter().GetResult();
        }
    }
}
