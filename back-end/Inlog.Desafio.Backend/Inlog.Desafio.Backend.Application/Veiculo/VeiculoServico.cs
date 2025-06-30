
namespace Inlog.Desafio.Backend.Application.Veiculo;

public class VeiculoServico : IVeiculoServico
{
    private readonly Infra.Database.Veiculo.IVeiculoRepositorio _veiculoRepositorio;

    public VeiculoServico(Infra.Database.Veiculo.IVeiculoRepositorio veiculoRepositorio)
    {
        _veiculoRepositorio = veiculoRepositorio;
    }

    public async Task<Domain.Models.Veiculo> CadastrarAsync(Domain.Models.Veiculo veiculo)
    {
        return await _veiculoRepositorio.CadastrarAsync(veiculo);
    }

    public async Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync()
    {
        return await _veiculoRepositorio.ListarVeiculosAsync();
    }
}