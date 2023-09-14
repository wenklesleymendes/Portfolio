using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Moq;

namespace Testes
{
    public class SistemaFinanceiroTeste
    {
        [Fact]
        public async Task AdicionarSistemaFinanceiro_ValidSistemaFinanceiro_CallsAddMethod()
        {
            // Arrange
            var sistemaFinanceiro = new SistemaFinanceiro { Nome = "Sistema Financeiro válido" };
            var mockInterfaceSistemaFinanceiro = new Mock<InterfaceSistemaFinanceiro>();
            var sistemaFinanceiroServico = new Domain.Servicos.SistemaFinanceiroTeste(mockInterfaceSistemaFinanceiro.Object);

            // Act
            await sistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

            // Assert
            mockInterfaceSistemaFinanceiro.Verify(mock => mock.Add(sistemaFinanceiro), Times.Once);
        }

        [Fact]
        public async Task AtualizarSistemaFinanceiro_ValidSistemaFinanceiro_CallsUpdateMethod()
        {
            // Arrange
            var sistemaFinanceiro = new SistemaFinanceiro { Nome = "Sistema Financeiro válido" };
            var mockInterfaceSistemaFinanceiro = new Mock<InterfaceSistemaFinanceiro>();
            var sistemaFinanceiroServico = new Domain.Servicos.SistemaFinanceiroTeste(mockInterfaceSistemaFinanceiro.Object);

            // Act
            await sistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

            // Assert
            mockInterfaceSistemaFinanceiro.Verify(mock => mock.Update(sistemaFinanceiro), Times.Once);
        }
    }
}
