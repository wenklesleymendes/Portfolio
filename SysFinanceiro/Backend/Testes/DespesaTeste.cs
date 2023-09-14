using Domain.Interfaces.IDespesa;
using Domain.Servicos;
using Entities.Entidades;
using Moq;

namespace Testes
{
    public class DespesaTeste
    {
        [Fact]
        public async Task AdicionarDespesa_ValidDespesa_CallsAddMethod()
        {
            // Arrange
            var despesa = new Despesa { Nome = "Despesa válida" };
            var mockInterfaceDespesa = new Mock<InterfaceDespesa>();
            var despesaServico = new DespesaServico(mockInterfaceDespesa.Object);

            // Act
            await despesaServico.AdicionarDespesa(despesa);

            // Assert
            mockInterfaceDespesa.Verify(mock => mock.Add(despesa), Times.Once);
        }

        [Fact]
        public async Task AtualizarDespesa_ValidDespesa_CallsUpdateMethod()
        {
            // Arrange
            var despesa = new Despesa { Nome = "Despesa válida" };
            var mockInterfaceDespesa = new Mock<InterfaceDespesa>();
            var despesaServico = new DespesaServico(mockInterfaceDespesa.Object);

            // Act
            await despesaServico.AtualizarDespesa(despesa);

            // Assert
            mockInterfaceDespesa.Verify(mock => mock.Update(despesa), Times.Once);
        }
    }
}
