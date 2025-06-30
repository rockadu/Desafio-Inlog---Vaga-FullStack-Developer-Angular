using Inlog.Desafio.Backend.Infra.Database.Veiculo;
using Moq;
using Bogus;
using FluentAssertions;
using Moq;
using Supabase.Postgrest;
using Supabase.Postgrest.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Inlog.Desafio.Backend.Test.Database.Veiculo;

public class VeiculoTest
{
    private readonly Mock<Supabase.Client> _supabaseClientMock;
    private readonly VeiculoRepositorio _veiculoRepositorio;
    private readonly Faker<Domain.Models.Veiculo> _faker;

    public VeiculoTest()
    {
        _supabaseClientMock = new Mock<Supabase.Client>();
        _veiculoRepositorio = new VeiculoRepositorio(_supabaseClientMock.Object);

        _faker = new Faker<Domain.Models.Veiculo>()
            .RuleFor(v => v.Chassi, f => f.Vehicle.Vin())
            .RuleFor(v => v.TipoVeiculo, f => f.PickRandom<Domain.Models.TipoVeiculo>())
            .RuleFor(v => v.Cor, f => f.Commerce.Color());
    }

    //[Fact]
    //public async Task CadastrarAsync_DeveInserirEVeiculoRetornado()
    //{
    //    // Arrange
    //    var veiculo = _faker.Generate();
    //    var baseResponse = new BaseResponse(); // Create a BaseResponse instance
    //    var response = new ModeledResponse<Domain.Models.Veiculo>(
    //        baseResponse,
    //        null, // Pass null for JsonSerializerSettings if not needed
    //        null, // Pass null for Func<Dictionary<string, string>>? if not needed
    //        false // Pass false for the last parameter
    //    );

    //    // Use reflection to set the read-only property 'Models'
    //    typeof(ModeledResponse<Domain.Models.Veiculo>)
    //        .GetProperty(nameof(response.Models))
    //        ?.SetValue(response, new List<Domain.Models.Veiculo> { veiculo });

    //    var tableMock = new Mock<Table<Domain.Models.Veiculo>>();
    //    tableMock
    //        .Setup(t => t.Insert(veiculo, It.IsAny<QueryOptions>(), default))
    //        .ReturnsAsync(response);

    //    _supabaseClientMock
    //        .Setup(c => c.From<Domain.Models.Veiculo>())
    //        .Returns((Supabase.Interfaces.ISupabaseTable<Domain.Models.Veiculo, Supabase.Realtime.RealtimeChannel>)tableMock.Object);

    //    // Act
    //    var resultado = await _veiculoRepositorio.CadastrarAsync(veiculo);

    //    // Assert
    //    resultado.Should().BeEquivalentTo(veiculo);
    //    tableMock.Verify(t => t.Insert(veiculo, It.IsAny<QueryOptions>(), default), Times.Once);
    //}

    //[Fact]
    //public async Task ListarVeiculosAsync_DeveRetornarListaDeVeiculos()
    //{
    //    // Arrange
    //    var veiculos = _faker.Generate(3);
    //    var baseResponse = new BaseResponse(); // Create a BaseResponse instance
    //    var response = new ModeledResponse<Veiculo>(
    //        baseResponse,
    //        null, // Pass null for JsonSerializerSettings if not needed
    //        null, // Pass null for Func<Dictionary<string, string>>? if not needed
    //        false // Pass false for the last parameter
    //    );

    //    // Use reflection to set the read-only property 'Models'
    //    typeof(ModeledResponse<Veiculo>)
    //        .GetProperty(nameof(response.Models))
    //        ?.SetValue(response, veiculos);

    //    var tableMock = new Mock<Table<Veiculo>>();
    //    tableMock
    //        .Setup(t => t.Get(It.IsAny<QueryOptions>(), default))
    //        .ReturnsAsync(response);

    //    _supabaseClientMock
    //        .Setup(c => c.From<Veiculo>())
    //        .Returns(tableMock.Object);

    //    // Act
    //    var resultado = await _veiculoRepositorio.ListarVeiculosAsync();

    //    // Assert
    //    resultado.Should().BeEquivalentTo(veiculos);
    //    tableMock.Verify(t => t.Get(It.IsAny<QueryOptions>(), default), Times.Once);
    //}
}

