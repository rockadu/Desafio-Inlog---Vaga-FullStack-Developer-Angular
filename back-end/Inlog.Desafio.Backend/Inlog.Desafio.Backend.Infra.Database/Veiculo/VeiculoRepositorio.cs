namespace Inlog.Desafio.Backend.Infra.Database.Veiculo;

public class VeiculoRepositorio : IVeiculoRepositorio
{
    private readonly Supabase.Client _supabaseClient;

    public VeiculoRepositorio(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<Domain.Models.Veiculo> CadastrarAsync(Domain.Models.Veiculo veiculo)
    {
        var result = await _supabaseClient
            .From<Domain.Models.Veiculo>()
            .Insert(veiculo);

        return result.Models.First();
    }

    public async Task<IEnumerable<Domain.Models.Veiculo>> ListarVeiculosAsync()
    {
        return (await _supabaseClient
            .From<Domain.Models.Veiculo>()
            .Get()).Models;
    }
}