namespace Inlog.Desafio.Backend.Application.Veiculo;

public interface IVeiculoServico
{
    public Task CadastrarAsync(Domain.Dtos.VeiculoDto veiculo);
    public Task DeletarAsync(string chassi);
    public Task<IEnumerable<Domain.Dtos.VeiculoDto>> ListarVeiculosAsync();
}