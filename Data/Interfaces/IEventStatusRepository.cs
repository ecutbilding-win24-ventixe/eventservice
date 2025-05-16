using Data.Entities;
using Domain.Models.Event;

namespace Data.Interfaces;

public interface IEventStatusRepository : IBaseRepository<EventStatusEntity, EventStatus>
{
}
