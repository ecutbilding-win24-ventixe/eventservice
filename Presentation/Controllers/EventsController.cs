using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Interfaces;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
}
