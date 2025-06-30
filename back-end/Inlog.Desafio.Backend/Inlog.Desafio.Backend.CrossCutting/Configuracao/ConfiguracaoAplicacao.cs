using Inlog.Desafio.Backend.CrossCutting.Configuracao.SubConfiguracoes;

namespace Inlog.Desafio.Backend.CrossCutting.Configuracao;

public class ConfiguracaoAplicacao
{
    public ConfiguracaoSupaBase SupaBase { get; set; } = new();
}