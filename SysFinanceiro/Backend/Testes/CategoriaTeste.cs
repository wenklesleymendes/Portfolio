using Domain.Interfaces.ICategoria;
using Domain.Servicos;
using Entities.Entidades;
using Moq;

namespace Testes
{
    public class CategoriaTeste
    {
        [Fact]
        public async Task AdicionarCategoria_ValidCategoria_CallsAddMethod()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Categoria válida" };
            var mockInterfaceCategoria = new Mock<InterfaceCategoria>();
            var categoriaServico = new CategoriaServico(mockInterfaceCategoria.Object);

            // Act
            await categoriaServico.AdicionarCategoria(categoria);

            // Assert
            mockInterfaceCategoria.Verify(mock => mock.Add(categoria), Times.Once);
        }

        [Fact]
        public async Task AdicionarCategoria_InvalidCategoria_DoesNotCallAddMethod()
        {
            // Arrange
            var categoria = new Categoria { Nome = null }; // Categoria inválida
            var mockInterfaceCategoria = new Mock<InterfaceCategoria>();
            var categoriaServico = new CategoriaServico(mockInterfaceCategoria.Object);

            // Act
            await categoriaServico.AdicionarCategoria(categoria);

            // Assert
            mockInterfaceCategoria.Verify(mock => mock.Add(categoria), Times.Never);
        }

        [Fact]
        public async Task AtualizarCategoria_ValidCategoria_CallsUpdateMethod()
        {
            // Arrange
            var categoria = new Categoria { Nome = "Categoria válida" };
            var mockInterfaceCategoria = new Mock<InterfaceCategoria>();
            var categoriaServico = new CategoriaServico(mockInterfaceCategoria.Object);

            // Act
            await categoriaServico.AtualizarCategoria(categoria);

            // Assert
            mockInterfaceCategoria.Verify(mock => mock.Update(categoria), Times.Once);
        }

        [Fact]
        public async Task AtualizarCategoria_InvalidCategoria_DoesNotCallUpdateMethod()
        {
            // Arrange
            var categoria = new Categoria { Nome = null }; // Categoria inválida
            var mockInterfaceCategoria = new Mock<InterfaceCategoria>();
            var categoriaServico = new CategoriaServico(mockInterfaceCategoria.Object);

            // Act
            await categoriaServico.AtualizarCategoria(categoria);

            // Assert
            mockInterfaceCategoria.Verify(mock => mock.Update(categoria), Times.Never);
        }
    }
}
