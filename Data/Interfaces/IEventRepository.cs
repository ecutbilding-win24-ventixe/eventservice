using Data.Entities;
using Data.Models;
using Domain.Models.Event;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IEventRepository : IBaseRepository<EventEntity , Event>
{
    Task<RepositoryResult<EventEntity>> GetEntityAsync(Expression<Func<EventEntity, bool>> where, params Expression<Func<EventEntity, object>>[] includes);
}
