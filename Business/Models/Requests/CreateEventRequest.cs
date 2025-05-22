namespace Business.Models.Requests;

public class CreateEventRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string Location { get; set; } = null!;
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }
    public string CategoryId { get; set; } = null!;
    public string StatusId { get; set; } = null!;

    public List<EventPackageRequest> Packages { get; set; } = [];

}
