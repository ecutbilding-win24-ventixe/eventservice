using Data.Entities;
using Domain.Models.Event;

namespace Data.Interfaces;

public interface IEventPackageRepository : IBaseRepository<EventPackageEntity, EventPackage>
{
}
