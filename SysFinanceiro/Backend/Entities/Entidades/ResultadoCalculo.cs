namespace Entities.Entidades
{
    public class ResultadoCalculo
    {
        public ResultadoCalculo(decimal valorLiquido, decimal valorBruto)
        {
            ValorLiquido = valorLiquido;
            ValorBruto = valorBruto;
        }

        public decimal ValorLiquido { get; set; }
        public decimal ValorBruto { get; set; }


    }
}
