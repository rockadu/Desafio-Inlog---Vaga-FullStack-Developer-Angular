namespace Inlog.Desafio.Backend.Infra.Database.Coordenadas;

public class CoordenadasRepositorio : ICoordenadasRepositorio
{
    private readonly Supabase.Client _supabaseClient;
    public CoordenadasRepositorio(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }
    public async Task CadastrarAsync(Domain.Models.Coordenadas coordenadas)
    {
        try
        {
            await _supabaseClient
                .From<Domain.Models.Coordenadas>()
                .Insert(coordenadas);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Domain.Models.Coordenadas> ObterPorRastreadorAsync(string rastreador)
    {
        return (await _supabaseClient
            .From<Domain.Models.Coordenadas>()
            .Where(r => r.Rastreador == rastreador).Get()).Models.First();
    }
}