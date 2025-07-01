namespace Inlog.Desafio.Backend.Application.Veiculo;

public interface IVeiculoServico
{
    public Task CadastrarAsync(Domain.Dtos.CadastrarVeiculoDto veiculo);
    public Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync();
}