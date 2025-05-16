using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Event;

namespace Data.Repositories;

public class EventStatusRepository(DataContext context) : BaseRepository<EventStatusEntity, EventStatus>(context), IEventStatusRepository
{
}