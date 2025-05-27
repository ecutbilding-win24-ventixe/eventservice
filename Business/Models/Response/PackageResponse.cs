namespace Business.Models.Response;

public class PackageResponse
{
    public string Id { get; set; } = null!;
    public string PackageTypeId { get; set; } = null!;
    public string EventPriceId { get; set; } = null!;
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
    public string Name { get; set; } = null!;
}