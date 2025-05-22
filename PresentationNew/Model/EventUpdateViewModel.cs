using System.ComponentModel.DataAnnotations;

namespace PresentationNew.Model;

public class EventUpdateViewModel : EventRegistrationViewModel
{
    [Required(ErrorMessage = "Event ID is required.")]
    public string Id { get; set; } = null!;
}