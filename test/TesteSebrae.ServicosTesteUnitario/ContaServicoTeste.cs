using Moq;
using TesteSebrae.Dominio;
using TesteSebrae.Infra.Interfaces;
using TesteSebrae.Servicos;

namespace TesteSebrae.ServicosTesteUnitario
{
    public class ContaServicoTeste
    {
        public ContaServicoTeste()
        {
        }

        [Fact]
        public async Task Adiciona_ContaValida_CriaId()
        {
            // Arrange
            var contaFake = ContaDadosFake.ContaFake();
            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Adiciona(It.Is<Conta>(c => c.Id != Guid.Empty), It.IsAny<CancellationToken>()))
                .ReturnsAsync(contaFake);

            var servico = new ContaServico(repositorioSimulado.Object);

            var contaRequisicao = contaFake;
            contaRequisicao.Id = Guid.Empty;

            // Act
            var resultado = await servico.Adiciona(contaRequisicao);

            // Assert
            Assert.False(resultado.Id == Guid.Empty);
        }

        [Fact]
        public async Task Atualiza_ContaValida_VinculaIdNaConta()
        {
            // Arrange
            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Atualiza(It.IsAny<Conta>(), It.IsAny<CancellationToken>()));

            var servico = new ContaServico(repositorioSimulado.Object);

            var contaRequisicao = ContaDadosFake.ContaFake();
            contaRequisicao.Id = Guid.Empty;
            var id = Guid.NewGuid();

            // Act
            await servico.Atualiza(id, contaRequisicao);

            // Assert
            repositorioSimulado.Verify(v => v.Atualiza(It.Is<Conta>(c => c.Id != Guid.Empty), It.IsAny<CancellationToken>()), Times.Once());
            Assert.True(contaRequisicao.Id == id);
        }

        [Fact]
        public async Task BuscaTodos_RetornaTodasContas()
        {
            // Arrange
            var resultadoEsperado = ContaDadosFake.ContasFake(20);
            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.BuscaTodos(It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.BuscaTodos();

            // Assert
            Assert.StrictEqual(resultado, resultadoEsperado);
        }

        [Fact]
        public async Task BuscaTodosPaginado_RetornaContasPaginado()
        {
            // Arrange
            var contasFake = ContaDadosFake.ContasFake(20);
            int quantidadeIgnorar = 5;
            int quantidadePegar = 10;
            var resultadoEsperado = contasFake.Skip(quantidadeIgnorar).Take(quantidadePegar);

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.BuscaTodosPaginado(quantidadeIgnorar, quantidadePegar, It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.BuscaTodosPaginado(quantidadeIgnorar, quantidadePegar);

            // Assert
            Assert.StrictEqual(resultado, resultadoEsperado);
        }

        [Fact]
        public async Task Deleta_ExisteIdNaBase_RetornaTrue()
        {
            // Arrange
            var id = Guid.NewGuid();

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Deleta(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.Deleta(id);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task Deleta_NaoExisteIdNaBase_RetornaFalse()
        {
            // Arrange
            var id = Guid.NewGuid();

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Deleta(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.Deleta(id);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Deleta_ExisteContaNaBase_RetornaTrue()
        {
            // Arrange
            var contaFake = ContaDadosFake.ContaFake();

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Deleta(contaFake, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.Deleta(contaFake);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task Deleta_NaoExisteContaNaBase_RetornaFalse()
        {
            // Arrange
            var contaFake = ContaDadosFake.ContaFake();

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.Deleta(contaFake, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.Deleta(contaFake);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ProcuraPeloId_ExisteIdNaBase_RetornaTrue()
        {
            // Arrange
            var resultadoEsperado = ContaDadosFake.ContaFake();
            var id = resultadoEsperado.Id;

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.ProcuraPeloId(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultadoEsperado);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.ProcuraPeloId(id);

            // Assert
            Assert.Equal(resultado, resultadoEsperado);
        }

        [Fact]
        public async Task ProcuraPeloId_NaoExisteIdNaBase_RetornaFalse()
        {
            // Arrange
            var contaFake = ContaDadosFake.ContaFake();
            var id = Guid.NewGuid();

            var repositorioSimulado = new Mock<IContaRepositorio>();
            repositorioSimulado.Setup(m => m.ProcuraPeloId(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as Conta);

            var servico = new ContaServico(repositorioSimulado.Object);

            // Act
            var resultado = await servico.ProcuraPeloId(id);

            // Assert
            Assert.Null(resultado);
        }
    }
}
