using Inlog.Desafio.Backend.Application.Veiculo;
using Microsoft.Extensions.DependencyInjection;

namespace Inlog.Desafio.Backend.Application;

public static class InjecaoDeDependencia
{
    public static IServiceCollection AdicionarServicos(this IServiceCollection services)
    {
        services.AddScoped<IVeiculoServico, VeiculoServico>();
        return services;
    }
}