using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Event;

namespace Data.Repositories;

public class EventPackageRepository(DataContext context) : BaseRepository<EventPackageEntity, EventPackage>(context), IEventPackageRepository
{
}
