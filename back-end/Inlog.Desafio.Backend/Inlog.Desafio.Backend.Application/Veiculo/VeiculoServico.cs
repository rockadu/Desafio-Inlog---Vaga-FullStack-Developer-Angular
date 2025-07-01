
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

    public async Task CadastrarAsync(Domain.Dtos.VeiculoDto input)
    {
        var veiculos = await _veiculoRepositorio.ListarVeiculosAsync();

        if (veiculos.Any(v => v.Chassi == input.Chassi))
            throw new ArgumentException($"Já existe um veículo cadastrado com o chassi {input.Chassi}.");

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

    public async Task DeletarAsync(string chassi)
    {
        var veiculo = await _veiculoRepositorio.ObterPorChassiAsync(chassi);
        var coordenadas = await _coordenadasRepositorio.ObterPorRastreadorAsync(veiculo.Rastreador);

        await _coordenadasRepositorio.DeletarAsync(coordenadas);
        await _veiculoRepositorio.DeletarAsync(veiculo);
    }

    public async Task<IEnumerable<Domain.Dtos.VeiculoDto>> ListarVeiculosAsync()
    {
        var veiculos = await _veiculoRepositorio.ListarVeiculosAsync();

        var veiculosDto = veiculos.Select(v => new Domain.Dtos.VeiculoDto
        {
            Chassi = v.Chassi,
            TipoVeiculo = (int)v.TipoVeiculo,
            Cor = v.Cor,
            Placa = v.Placa,
            Rastreador = v.Rastreador
        }).ToList();

        foreach (var veiculoDto in veiculosDto)
        {
            var coordenadas = await _coordenadasRepositorio.ObterPorRastreadorAsync(veiculoDto.Rastreador);
            if (coordenadas != null)
            {
                veiculoDto.Coordenadas = new Domain.Dtos.CoordenadasDto
                {
                    Latitude = coordenadas.Latitude,
                    Longitude = coordenadas.Longitude
                };
            }
        }

        return veiculosDto;
    }
}