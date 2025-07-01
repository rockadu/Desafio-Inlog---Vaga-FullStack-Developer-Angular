using Inlog.Desafio.Backend.Infra.Database.Coordenadas;
using Inlog.Desafio.Backend.Infra.Database.Veiculo;
using Microsoft.Extensions.DependencyInjection;

namespace Inlog.Desafio.Backend.Infra.Database;

public static class InjecaoDeDependencia
{
    public static IServiceCollection AdicionarRepositorios(this IServiceCollection services)
    {
        services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
        services.AddScoped<ICoordenadasRepositorio, CoordenadasRepositorio>();
        return services;
    }
}