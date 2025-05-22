namespace Domain.Models.Event;

public class EventPackage
{
    public string Title { get; set; } = null!;
    public string? SeatingArragement { get; set; }
    public string? Placement { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
}