using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Inlog.Desafio.Backend.Domain.Models;

[Table("Coordenadas")]
public class Coordenadas : BaseModel
{
    [PrimaryKey("Rastreador")]
    public string Rastreador { get; set; } = string.Empty;

    [Column("Latitude")]
    public decimal Latitude { get; set; }

    [Column("Longitude")]
    public decimal Longitude { get; set; }
}