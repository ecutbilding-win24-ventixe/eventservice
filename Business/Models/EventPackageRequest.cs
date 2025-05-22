namespace Business.Models;

public class EventPackageRequest
{
    public string PackageTypeId { get; set; } = null!;
    public string? Placement { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
}
