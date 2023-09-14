using Entities.Entidades;

namespace Testes
{
    public class InvestimentoTeste
    {
        [Fact]
        public void CalculaValorFinal_DeveCalcularCorretamente()
        {
            // Arrange
            decimal valorInicial = 1000M;
            int meses = 12;
            Investimento investimento = new Investimento(valorInicial, meses);

            // Act
            investimento.CalculaValorFinal();

            // Assert
            decimal valorEsperadoBruto = 1212.6913697897046M;
            decimal valorEsperadoLiquido = valorEsperadoBruto * (1 - 0.015M);
            decimal tolerancia = 0.0000001M;

            Assert.InRange(investimento.VF, valorEsperadoBruto - tolerancia, valorEsperadoBruto + tolerancia);
            Assert.InRange(investimento.ValorLiquido, valorEsperadoLiquido - tolerancia, valorEsperadoLiquido + tolerancia);
        }

        [Fact]
        public void Construtor_ComValorInicialNegativo_DeveLancarExcecao()
        {
            // Arrange
            decimal valorInicial = -1000M;
            int meses = 12;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Investimento(valorInicial, meses));
        }

        [Fact]
        public void Construtor_ComPrazoMenorQueUm_DeveLancarExcecao()
        {
            // Arrange
            decimal valorInicial = 1000M;
            int meses = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Investimento(valorInicial, meses));
        }
    }
}