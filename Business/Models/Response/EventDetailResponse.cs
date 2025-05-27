namespace Business.Models.Response;

public class EventDetailResponse
{
    public string Id { get; set; } = null!;
    public int Capacity { get; set; }
    public List<PackageResponse> Packages { get; set; } = new();
}
