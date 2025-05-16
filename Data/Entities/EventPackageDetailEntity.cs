using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EventPackageDetailEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? SeatingArragement { get; set; }
    public string? Placement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }
    public string? Currency { get; set; }
}
