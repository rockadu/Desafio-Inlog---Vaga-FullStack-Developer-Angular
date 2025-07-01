using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace Inlog.Desafio.Backend.Domain.Models;

[Table("Veiculo")]
public class Veiculo : BaseModel
{
    [PrimaryKey("Chassi")]
    public string Chassi { get; set; } = string.Empty;

    [Column("TipoVeiculo")]
    public TipoVeiculo TipoVeiculo { get; set; }

    [Column("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Column("Placa")]
    public string Placa { get; set; } = string.Empty;

    [Column("Rastreador")]
    public string Rastreador { get; set; } = string.Empty;
}