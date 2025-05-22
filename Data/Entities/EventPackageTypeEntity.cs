using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EventPackageTypeEntity 
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? SeatingArragement { get; set; }
}
