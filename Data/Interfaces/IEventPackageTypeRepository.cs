using Data.Entities;
using Domain.Models.Event;

namespace Data.Interfaces;

public interface IEventPackageTypeRepository : IBaseRepository<EventPackageTypeEntity, EventPackageType>
{
}