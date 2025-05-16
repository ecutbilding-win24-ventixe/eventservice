using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Event;

namespace Data.Repositories;

public class EventCategoryRepository(DataContext context) : BaseRepository<EventCategoryEntity, EventCategory>(context), IEventCategoryRepository
{
}
