using Bogus;
using Inlog.Desafio.Backend.Application.Veiculo;
using Inlog.Desafio.Backend.Infra.Database.Veiculo;
using Inlog.Desafio.Backend.Domain.Models;
using Moq;
using Xunit;
using FluentAssertions;

namespace Inlog.Desafio.Backend.Test.Application.Veiculo;

public class VeiculoTest
{
    private readonly Mock<IVeiculoRepositorio> _veiculoRepositorioMock;
    private readonly VeiculoServico _veiculoServico;
    private readonly Faker<Domain.Models.Veiculo> _faker;

    public VeiculoTest()
    {
        _veiculoRepositorioMock = new Mock<IVeiculoRepositorio>();
        _veiculoServico = new VeiculoServico(_veiculoRepositorioMock.Object);

        _faker = new Faker<Domain.Models.Veiculo>()
            .RuleFor(v => v.Chassi, f => f.Vehicle.Vin())
            .RuleFor(v => v.TipoVeiculo, f => f.PickRandom<TipoVeiculo>())
            .RuleFor(v => v.Cor, f => f.Commerce.Color());
    }

    [Fact]
    public async Task CadastrarAsync_DeveRetornarVeiculoCadastrado()
    {
        // Arrange
        var veiculo = _faker.Generate();
        _veiculoRepositorioMock
            .Setup(r => r.CadastrarAsync(veiculo))
            .ReturnsAsync(veiculo);

        // Act
        var resultado = await _veiculoServico.CadastrarAsync(veiculo);

        // Assert
        resultado.Should().BeEquivalentTo(veiculo);
        _veiculoRepositorioMock.Verify(r => r.CadastrarAsync(veiculo), Times.Once);
    }

    [Fact]
    public async Task ListarVeiculosAsync_DeveRetornarListaDeVeiculos()
    {
        // Arrange
        var veiculos = _faker.Generate(3);
        _veiculoRepositorioMock
            .Setup(r => r.ListarVeiculosAsync())
            .ReturnsAsync(veiculos);

        // Act
        var resultado = await _veiculoServico.ListarVeiculosAsync();

        // Assert
        resultado.Should().BeEquivalentTo(veiculos);
        _veiculoRepositorioMock.Verify(r => r.ListarVeiculosAsync(), Times.Once);
    }
}