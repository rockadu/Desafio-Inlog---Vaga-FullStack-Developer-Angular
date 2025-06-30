using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

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
}