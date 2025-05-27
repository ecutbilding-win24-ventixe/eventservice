using Business.Models;
using Business.Models.Requests;
using Business.Models.Response;
using Domain.Models.Event;

namespace Business.Interfaces;

public interface IEventService
{
    Task<EventResult<Event>> GetEventByIdAsync(string eventId);
    Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync();
    Task<EventResult> CreateEventAsync(CreateEventRequest request);
    Task<EventResult> UpdateEventAsync(UpdateEventRequest request);
    Task<EventResult> DeleteEventAsync(string eventId);
    Task<EventResult<IEnumerable<EventCategory>>> GetAllEventCategoriesAsync();
    Task<EventResult<IEnumerable<EventPackageType>>> GetAllEventPackagesTypeAsync();
    Task<EventResult<IEnumerable<EventStatus>>> GetAllEventStatusesAsync();
    Task<EventResult<EventDetailResponse>> GetEventBookingDetails(string id);
}
