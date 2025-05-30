﻿using Business.Interfaces;
using Business.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using PresentationNew.Model;


namespace PresentationNew.Controllers;

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
    public async Task<IActionResult> CreateEvent(EventRegistrationViewModel form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = form.ToCreateRequest();
        var result = await _eventService.CreateEventAsync(request);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateEvent(string id, [FromBody] EventUpdateViewModel form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != form.Id)
            return BadRequest("Event ID mismatch.");

        var request = form.ToEventUpdateRequest();
        var result = await _eventService.UpdateEventAsync(request);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var result = await _eventService.DeleteEventAsync(id);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetEventCategories()
    {
        var result = await _eventService.GetAllEventCategoriesAsync();
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet("statuses")]
    public async Task<IActionResult> GetEventStatuses()
    {
        var result = await _eventService.GetAllEventStatusesAsync();
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet("packages")]
    public async Task<IActionResult> GetEventPackages()
    {
        var result = await _eventService.GetAllEventPackagesTypeAsync();
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}/booking-details")]
    public async Task<IActionResult> GetEventBookingDetails(string id)
    {
        var result = await _eventService.GetEventBookingDetails(id);
        return result.Succeeded
            ? Ok(result)
            : StatusCode(result.StatusCode, result);
    }
}
