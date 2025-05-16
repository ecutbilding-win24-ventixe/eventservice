using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models.Event;

namespace Data.Repositories;

public class EventPackageDetailRepository(DataContext context) : BaseRepository<EventPackageDetailEntity, EventPackageDetail>(context), IEventPackageDetailRepository
{
}