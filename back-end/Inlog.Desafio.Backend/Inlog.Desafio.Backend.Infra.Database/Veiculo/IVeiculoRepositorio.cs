namespace Inlog.Desafio.Backend.Infra.Database.Veiculo;

public interface IVeiculoRepositorio
{
    public Task<Domain.Models.Veiculo> CadastrarAsync(Domain.Models.Veiculo veiculo);
    public Task<Domain.Models.Veiculo> ObterPorChassiAsync(string chassi);
    public Task DeletarAsync(Domain.Models.Veiculo veiculo);
    public Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync();
}