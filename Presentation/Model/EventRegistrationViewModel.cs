using Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Model;

public class EventRegistrationViewModel
{
    [Required(ErrorMessage = "Event name is required.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Event description is required.")]
    public string Description { get; set; } = null!;


    [Required(ErrorMessage = "Event date is required.")]
    public DateTime EventDate { get; set; } = DateTime.Now;


    [Required(ErrorMessage = "Event location is required.")]
    public string Location { get; set; } = null!;

    [Required(ErrorMessage = "Event capacity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 100")]
    public int Capacity { get; set; } = 100;

    public string? ImageUrl { get; set; } = null!;


    [Required(ErrorMessage = "Event category is required.")]
    public string CategoryId { get; set; } = null!;


    [Required(ErrorMessage = "Event status is required.")]
    public string StatusId { get; set; } = null!;


    [Required]
    [MinLength(1, ErrorMessage = "At least one package is required.")]
    public List<EventPackageViewModel> Packages { get; set; } = [];



    public TModel MapTo<TModel>() where TModel : class, new()
    {
        return MappExtensions.MapTo<TModel>(this);
    }

}
