namespace Inlog.Desafio.Backend.Infra.Database.Coordenadas;

public interface ICoordenadasRepositorio
{
    public Task CadastrarAsync(Domain.Models.Coordenadas coordenadas);
    public Task<Domain.Models.Coordenadas> ObterPorRastreadorAsync(string rastreador);
}