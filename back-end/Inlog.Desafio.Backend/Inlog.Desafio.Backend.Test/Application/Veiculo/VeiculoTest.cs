using Bogus;
using FluentAssertions;
using Inlog.Desafio.Backend.Application.Veiculo;
using Inlog.Desafio.Backend.Domain.Dtos;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Infra.Database.Coordenadas;
using Inlog.Desafio.Backend.Infra.Database.Veiculo;
using Moq;
using Xunit;

namespace Inlog.Desafio.Backend.Test.Application.Veiculo;

public class VeiculoTest
{
    private readonly Mock<IVeiculoRepositorio> _veiculoRepoMock;
    private readonly Mock<ICoordenadasRepositorio> _coordenadasRepoMock;
    private readonly VeiculoServico _servico;
    private readonly Faker<Domain.Models.Veiculo> _faker;

    public VeiculoTest()
    {
        _veiculoRepoMock = new Mock<IVeiculoRepositorio>();
        _coordenadasRepoMock = new Mock<ICoordenadasRepositorio>();
        _servico = new VeiculoServico(_veiculoRepoMock.Object, _coordenadasRepoMock.Object);

        _faker = new Faker<Domain.Models.Veiculo>()
            .RuleFor(v => v.Chassi, f => f.Random.String2(17, "ABCDEFGHJKLMNPRSTUVWXYZ0123456789"))
            .RuleFor(v => v.Cor, f => f.Commerce.Color())
            .RuleFor(v => v.Placa, f => f.Vehicle.Vin().Substring(0, 7))
            .RuleFor(v => v.Rastreador, f => f.Random.AlphaNumeric(6))
            .RuleFor(v => v.TipoVeiculo, f => TipoVeiculo.Caminhao);
    }

    [Fact]
    public async Task ListarVeiculosAsync_DeveRetornarVeiculosComCoordenadas()
    {
        // Arrange
        var fakeVeiculo = _faker.Generate();
        var fakeCoordenada = new Coordenadas { Rastreador = fakeVeiculo.Rastreador, Latitude = -25.0m, Longitude = -49.0m };

        _veiculoRepoMock.Setup(r => r.ListarVeiculosAsync()).ReturnsAsync(new List<Domain.Models.Veiculo> { fakeVeiculo });
        _coordenadasRepoMock.Setup(c => c.ObterPorRastreadorAsync(fakeVeiculo.Rastreador)).ReturnsAsync(fakeCoordenada);

        // Act
        var resultado = (await _servico.ListarVeiculosAsync()).ToList();

        // Assert
        resultado.Should().HaveCount(1);
        resultado[0].Chassi.Should().Be(fakeVeiculo.Chassi);
        resultado[0].Coordenadas.Should().NotBeNull();
        resultado[0].Coordenadas!.Latitude.Should().Be(-25.0m);
    }

    [Fact]
    public async Task CadastrarAsync_DeveChamarRepositorios()
    {
        // Arrange
        var veiculoDto = new VeiculoDto
        {
            Chassi = "123ABC456",
            TipoVeiculo = (int)TipoVeiculo.Onibus,
            Cor = "#000000",
            Placa = "AAA-1234",
            Rastreador = "XX999X",
            Coordenadas = new CoordenadasDto { Latitude = -25.4m, Longitude = -49.2m }
        };

        // Act
        await _servico.CadastrarAsync(veiculoDto);

        // Assert
        _veiculoRepoMock.Verify(v => v.CadastrarAsync(It.IsAny<Domain.Models.Veiculo>()), Times.Once);
        _coordenadasRepoMock.Verify(c => c.CadastrarAsync(It.Is<Coordenadas>(d => d.Rastreador == veiculoDto.Rastreador)), Times.Once);
    }

    [Fact]
    public async Task DeletarAsync_DeveDeletarVeiculoECoordenadas()
    {
        // Arrange
        var chassi = "CHASSI123";
        var rastreador = "RAST01";

        _veiculoRepoMock.Setup(
            v => v.ObterPorChassiAsync(chassi))
            .ReturnsAsync(new Domain.Models.Veiculo { 
                Chassi = chassi, 
                Rastreador = rastreador });

        _coordenadasRepoMock.Setup(
            c => c.ObterPorRastreadorAsync(rastreador))
            .ReturnsAsync(new Coordenadas { 
                Rastreador = rastreador, 
                Latitude = -25.0m, 
                Longitude = -49.0m });

        _veiculoRepoMock
            .Setup(v => v.DeletarAsync(It.IsAny<Domain.Models.Veiculo>()))
            .Returns(Task.CompletedTask);

        _coordenadasRepoMock
            .Setup(c => c.DeletarAsync(It.IsAny<Coordenadas>()))
            .Returns(Task.CompletedTask);

        // Act
        await _servico.DeletarAsync(chassi);

        // Assert
        _veiculoRepoMock.Verify(v => v.DeletarAsync(It.Is<Domain.Models.Veiculo>(x => x.Chassi == chassi)), Times.Once);
        _coordenadasRepoMock.Verify(c => c.DeletarAsync(It.Is<Coordenadas>(x => x.Rastreador == rastreador)), Times.Once);
    }
}