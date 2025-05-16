using Business.Interfaces;
using Business.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Presentation.Model;


namespace Data.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        var result = await _eventService.GetEventByIdAsync(id);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await _eventService.GetAllEventsAsync();
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventRegistrationViewModel form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = form.MapTo<CreateEventRequest>();
        var result = await _eventService.CreateEventAsync(request);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(string id, [FromBody] EventUpdateViewModel form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (id != form.Id)
            return BadRequest("Event ID mismatch.");

        var request = form.MapTo<UpdateEventRequest>();
        var result = await _eventService.UpdateEventAsync(request);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var result = await _eventService.DeleteEventAsync(id);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }
}
