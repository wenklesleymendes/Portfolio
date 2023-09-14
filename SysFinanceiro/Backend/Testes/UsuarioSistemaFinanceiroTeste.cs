using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Domain.Servicos;
using Entities.Entidades;
using Moq;

namespace Testes 
{
    public class UsuarioSistemaFinanceiroTeste
    {
        [Fact]
        public async Task CadastrarUsuarioNoSistema_CallsAddMethod()
        {
            // Arrange
            var usuarioSistemaFinanceiro = new UsuarioSistemaFinanceiro();
            var mockInterfaceUsuarioSistemaFinanceiro = new Mock<InterfaceUsuarioSistemaFinanceiro>();
            var usuarioSistemaFinanceiroServico = new UsuarioSistemaFinanceiroServico(mockInterfaceUsuarioSistemaFinanceiro.Object);

            // Act
            await usuarioSistemaFinanceiroServico.CadastrarUsuarioNoSistema(usuarioSistemaFinanceiro);

            // Assert
            mockInterfaceUsuarioSistemaFinanceiro.Verify(mock => mock.Add(usuarioSistemaFinanceiro), Times.Once);
        }
    }
}

