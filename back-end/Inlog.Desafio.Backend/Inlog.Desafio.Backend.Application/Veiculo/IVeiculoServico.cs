namespace Inlog.Desafio.Backend.Application.Veiculo;

public interface IVeiculoServico
{
    public Task<Domain.Models.Veiculo> CadastrarAsync(Domain.Models.Veiculo veiculo);
    public Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync();
}