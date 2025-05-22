using System.ComponentModel.DataAnnotations;

namespace Presentation.Model;

public class EventPackageViewModel
{
    [Required]
    public string PackageTypeId { get; set; } = null!;

    public string? Placement { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
    public decimal Price { get; set; }

    public string Currency { get; set; } = "USD";
}