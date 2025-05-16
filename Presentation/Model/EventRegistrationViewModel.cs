using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    [Required(ErrorMessage = "Event package detail is required.")]
    public string PackageDetailId { get; set; } = null!;

    public List<SelectItem> CategoryList { get; set; } = new();
    public List<SelectItem> StatusList { get; set; } = new();
    public List<SelectItem> PackageDetailList { get; set; } = new();

    public TModel MapTo<TModel>() where TModel : class, new ()
    {
        //Chatgpt hjälpe här
        return this.MapTo<TModel>();
    }

}

public class SelectItem
{
    public string Value { get; set; } = null!;
    public string Text { get; set; } = null!;
}
