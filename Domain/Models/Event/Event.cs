namespace Domain.Models.Event;

public class Event
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }

    public EventCategory Category { get; set; } = null!;
    public EventStatus Status { get; set; } = null!;

    public List<EventPackageDetail> Packages { get; set; } = [];

}
