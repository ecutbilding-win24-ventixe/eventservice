using Data.Entities;
using Domain.Models.Event;

namespace Data.Interfaces;

public interface IEventCategoryRepository : IBaseRepository<EventCategoryEntity, EventCategory>
{
}
