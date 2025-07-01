
using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Veiculo;

public class VeiculoServico : IVeiculoServico
{
    private readonly Infra.Database.Veiculo.IVeiculoRepositorio _veiculoRepositorio;
    private readonly Infra.Database.Coordenadas.ICoordenadasRepositorio _coordenadasRepositorio;

    public VeiculoServico(Infra.Database.Veiculo.IVeiculoRepositorio veiculoRepositorio, Infra.Database.Coordenadas.ICoordenadasRepositorio coordenadasRepositorio)
    {
        _veiculoRepositorio = veiculoRepositorio;
        _coordenadasRepositorio = coordenadasRepositorio;
    }

    public async Task CadastrarAsync(Domain.Dtos.CadastrarVeiculoDto input)
    {

        var veiculo = new Domain.Models.Veiculo
        {
            Chassi = input.Chassi,
            TipoVeiculo = (TipoVeiculo)input.TipoVeiculo,
            Cor = input.Cor,
            Placa = input.Placa,
            Rastreador = input.Rastreador,
        };

        await _veiculoRepositorio.CadastrarAsync(veiculo);

        var coordenadas = new Coordenadas
        {
            Latitude = input.Coordenadas.Latitude,
            Longitude = input.Coordenadas.Longitude,
            Rastreador = veiculo.Rastreador
        };

        await _coordenadasRepositorio.CadastrarAsync(coordenadas);
    }

    public async Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync()
    {
        return await _veiculoRepositorio.ListarVeiculosAsync();
    }
}