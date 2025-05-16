using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Domain.Models.Event;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity, Event>(context), IEventRepository
{
    public override async Task<RepositoryResult<Event>> GetAsync(Expression<Func<EventEntity, bool>> where, params Expression<Func<EventEntity, object>>[] includes)
    {
        try
        {
            IQueryable<EventEntity> query = _table;

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var entity = await query
                .Include(e => e.Category)
                .Include(e => e.Status)
                .Include(e => e.PackageDetail)
                .FirstOrDefaultAsync(where);

            if (entity == null)
                return new RepositoryResult<Event> { Succeeded = false, StatusCode = 404, Message = "Event not found." };

            var domainModel = new Event
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                EventDate = entity.EventDate,
                Location = entity.Location,
                Capacity = entity.Capacity,
                ImageUrl = entity.ImageUrl,
                Category = new EventCategory
                {
                    Name = entity.Category.Name,
                },
                Status = new EventStatus
                {
                    Name = entity.Status.Name,
                },
                Packages =
                [
                    new() {
                        Title = entity.PackageDetail.Title,
                        SeatingArragement = entity.PackageDetail.SeatingArragement,
                        Placement = entity.PackageDetail.Placement,
                        Price = entity.PackageDetail.Price,
                        Currency = entity.PackageDetail.Currency
                    }
                ]
            };

            return new RepositoryResult<Event>{ Succeeded = true, StatusCode = 200, Result = domainModel };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<Event>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = $"An error occurred while retrieving the event. {ex.Message}"
            };
        }
    }

    public override async Task<RepositoryResult<IEnumerable<Event>>> GetAllAsync(bool orderByDescending = false, Expression<Func<EventEntity, object>>? sortBy = null, Expression<Func<EventEntity, bool>>? where = null, params Expression<Func<EventEntity, object>>[] includes)
    {
        try
        {
            IQueryable<EventEntity> query = _table;

            if (where != null)
                query = query.Where(where);

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            query = query
                .Include(e => e.Category)
                .Include(e => e.Status)
                .Include(e => e.PackageDetail);

            if (sortBy != null)
                query = orderByDescending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);

            var entities = await query.ToListAsync();

            var domainModels = entities.Select(entity => new Event
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                EventDate = entity.EventDate,
                Location = entity.Location,
                Capacity = entity.Capacity,
                ImageUrl = entity.ImageUrl,
                Category = new EventCategory
                {
                    Name = entity.Category.Name,
                },
                Status = new EventStatus
                {
                    Name = entity.Status.Name,
                },
                Packages =
                [
                    new() {
                        Title = entity.PackageDetail.Title,
                        SeatingArragement = entity.PackageDetail.SeatingArragement,
                        Placement = entity.PackageDetail.Placement,
                        Price = entity.PackageDetail.Price,
                        Currency = entity.PackageDetail.Currency
                    }
                ]
            }).ToList();

            return new RepositoryResult<IEnumerable<Event>> { Succeeded = true, StatusCode = 200, Result = domainModels };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<Event>>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = $"An error occurred while retrieving events. {ex.Message}"
            };
        }
    }
}
