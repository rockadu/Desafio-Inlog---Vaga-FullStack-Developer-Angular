namespace Inlog.Desafio.Backend.Domain.Dtos;

public class VeiculoDto
{
    public string Chassi { get; set; } = string.Empty;
    public int TipoVeiculo { get; set; }
    public string NomeTipoVeiculo => TipoVeiculo == 1 ? "Onibus" : "Caminhão";
    public string Cor { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public string Rastreador { get; set; } = string.Empty;
    public CoordenadasDto Coordenadas { get; set; } = new();
}

public class CoordenadasDto
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}