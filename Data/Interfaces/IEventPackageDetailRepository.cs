using Data.Entities;
using Domain.Models.Event;

namespace Data.Interfaces;

public interface IEventPackageDetailRepository : IBaseRepository<EventPackageDetailEntity, EventPackageDetail>
{
}