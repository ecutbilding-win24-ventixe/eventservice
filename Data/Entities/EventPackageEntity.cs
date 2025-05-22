using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class EventPackageEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey(nameof(Event))]
    public string EventId { get; set; } = null!;
    public EventEntity Event { get; set; } = null!;

    [ForeignKey(nameof(PackageType))]
    public string PackageTypeId { get; set; } = null!;
    public EventPackageTypeEntity PackageType { get; set; } = null!;

    public string? Placement { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public string Currency { get; set; } = "USD";
}