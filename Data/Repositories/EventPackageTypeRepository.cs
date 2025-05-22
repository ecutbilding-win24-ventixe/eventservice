using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Event;

namespace Data.Repositories;

public class EventPackageTypeRepository(DataContext context) : BaseRepository<EventPackageTypeEntity, EventPackageType>(context), IEventPackageTypeRepository
{
}