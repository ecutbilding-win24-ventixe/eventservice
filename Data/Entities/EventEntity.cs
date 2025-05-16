using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class EventEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string ImageUrl { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public string CategoryId { get; set; } = null!;
    public EventCategoryEntity Category { get; set; } = null!;


    [ForeignKey(nameof(Status))]
    public string StatusId { get; set; } = null!;
    public EventStatusEntity Status { get; set; } = null!;

    
    [ForeignKey(nameof(PackageDetail))]
    public string EventPackageDetailId { get; set; } = null!;
    public EventPackageDetailEntity PackageDetail { get; set; } = null!;

}
