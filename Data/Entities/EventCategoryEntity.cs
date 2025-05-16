using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EventCategoryEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
