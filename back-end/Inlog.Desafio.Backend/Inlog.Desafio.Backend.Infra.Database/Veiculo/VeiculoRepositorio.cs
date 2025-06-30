namespace Inlog.Desafio.Backend.Infra.Database.Veiculo;

public class VeiculoRepositorio : IVeiculoRepositorio
{
    private readonly Supabase.Client _supabaseClient;

    public VeiculoRepositorio(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public Task<Domain.Models.Veiculo> CadastrarAsync(Domain.Models.Veiculo veiculo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync()
    {
        throw new NotImplementedException();
    }
}