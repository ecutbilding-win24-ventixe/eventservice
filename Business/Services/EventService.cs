using Business.Interfaces;
using Business.Models;
using Business.Models.Requests;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models.Event;
using System.Linq.Expressions;

namespace Business.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync()
    {
        try
        {
            var result = await _eventRepository.GetAllAsync(orderByDescending: true, sortBy: e => e.EventDate, includes:
                [
                    x => x.Category,
                    x => x.Status,
                    x => x.PackageDetail
                ]
            );

            if (!result.Succeeded || result.Result == null)
                return new EventResult<IEnumerable<Event>> { Succeeded = false, StatusCode = 404, Message = "Event not found." };

            return new EventResult<IEnumerable<Event>> { Succeeded = true, StatusCode = 200, Result = result.Result };
        }
        catch (Exception ex)
        {
            return new EventResult<IEnumerable<Event>> { Succeeded = false, Message = $"An error occurred while retrieving events. {ex.Message}" };
        }
    }

    public async Task<EventResult<Event>> GetEventByIdAsync(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
            throw new ArgumentException("Event ID cannot be null or empty.", nameof(eventId));

        try
        {
            var result = await _eventRepository.GetAsync(x => x.Id == eventId, x => x.Category, x => x.Status, x => x.PackageDetail);
            if (!result.Succeeded || result.Result == null)
                return new EventResult<Event> { Succeeded = false, StatusCode = 404, Message = "Event not found.You need to work harderrrr." };

            return new EventResult<Event> { Succeeded = true, StatusCode = 200, Result = result.Result, Message = "Yaha is ok!" };
        }
        catch (Exception ex)
        {
            return new EventResult<Event>
            {
                Succeeded = false,
                Message = $"An error occurred while retrieving the event. {ex.Message}",

            };
        }
    }

    public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");

        try
        {
            await _eventRepository.BeginTransactionAsync();

            var eventCategory = await _eventRepository.ExistsAsync(c => c.CategoryId == request.CategoryId);
            if (!eventCategory.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Category not found"};

            var eventStatus = await _eventRepository.ExistsAsync(c => c.StatusId == request.StatusId);
            if (!eventStatus.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Status not found" };

            var eventPackage = await _eventRepository.ExistsAsync(c => c.CategoryId == request.CategoryId);
            if (!eventPackage.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Package not found" };

            var newEvent = new EventEntity
            {
                Name = request.Name,
                Description = request.Description,
                EventDate = request.EventDate,
                Location = request.Location,
                Capacity = request.Capacity,
                ImageUrl = "",
                CategoryId = request.CategoryId,
                StatusId = request.StatusId,
                EventPackageDetailId = request.PackageDetailId,
            };

            var result = await _eventRepository.AddAsync(newEvent);
            await _eventRepository.CommitTransactionAsync();
            return result.Succeeded
                ? new EventResult { Succeeded = true, StatusCode = 201, Message = "Event created successfully." }
                : new EventResult { Succeeded = false, StatusCode = 500, Message = "An error occurred while creating the event." };
        }
        catch (Exception ex)
        {
            await _eventRepository.RollbackTransactionAsync();
            return new EventResult { Succeeded = false, StatusCode = 500, Message = $"An error occurred while creating the event. {ex.Message}" };
        }
    }

    public async Task<EventResult> UpdateEventAsync(UpdateEventRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");

        try
        {
            await _eventRepository.BeginTransactionAsync();
            var eventResult = await _eventRepository.GetAsync(e => e.Id == request.Id);
            if (!eventResult.Succeeded || eventResult.Result == null)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Event not found." };

            var existingEvent = eventResult.Result.MapTo<EventEntity>();

            var eventCategory = await _eventRepository.ExistsAsync(c => c.CategoryId == request.CategoryId);
            if (!eventCategory.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Category not found" };

            var eventStatus = await _eventRepository.ExistsAsync(c => c.StatusId == request.StatusId);
            if (!eventStatus.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Status not found" };

            var eventPackage = await _eventRepository.ExistsAsync(c => c.CategoryId == request.CategoryId);
            if (!eventPackage.Succeeded)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Package not found" };

            existingEvent.Name = request.Name;
            existingEvent.Description = request.Description;
            existingEvent.EventDate = request.EventDate;
            existingEvent.Location = request.Location;
            existingEvent.Capacity = request.Capacity;
            existingEvent.ImageUrl = request.ImageUrl ?? existingEvent.ImageUrl;
            existingEvent.CategoryId = request.CategoryId;
            existingEvent.StatusId = request.StatusId;
            existingEvent.EventPackageDetailId = request.PackageDetailId;

            var updateResult = await _eventRepository.UpdateAsync(existingEvent);
            await _eventRepository.CommitTransactionAsync();
            return updateResult.Succeeded
                ? new EventResult { Succeeded = true, StatusCode = 200, Message = "Event updated successfully." }
                : new EventResult { Succeeded = false, StatusCode = 500, Message = "An error occurred while updating the event." };
        }
        catch (Exception ex)
        {
            await _eventRepository.RollbackTransactionAsync();
            return new EventResult { Succeeded = false, StatusCode = 500, Message = $"An error occurred while updating the event. {ex.Message}" };
        }
    }

    public async Task<EventResult> DeleteEventAsync(string eventId)
    {
        if (string.IsNullOrWhiteSpace(eventId))
            throw new ArgumentException("Event ID cannot be null or empty.", nameof(eventId));
        try
        {
            await _eventRepository.BeginTransactionAsync();
            var eventResult = await _eventRepository.GetAsync(e => e.Id == eventId);
            if (!eventResult.Succeeded || eventResult.Result == null)
                return new EventResult { Succeeded = false, StatusCode = 404, Message = "Event not found." };

            var existingEvent = eventResult.Result.MapTo<EventEntity>();
            var deleteResult = await _eventRepository.DeleteAsync(existingEvent);
            await _eventRepository.CommitTransactionAsync();
            return deleteResult.Succeeded 
                ? new EventResult { Succeeded = true, StatusCode = 200, Message = "Event deleted successfully." }
                : new EventResult { Succeeded = false, StatusCode = 500, Message = "An error occurred while deleting the event." };

        }
        catch (Exception ex) 
        {
            await _eventRepository.RollbackTransactionAsync();
            return new EventResult { Succeeded = false, StatusCode = 500, Message = $"An error occurred while deleting the event. {ex.Message}" };
        }
    }
}
