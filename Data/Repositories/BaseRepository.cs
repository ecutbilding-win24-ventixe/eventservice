using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using Data.Interfaces;
using Domain.Extensions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;
    private IDbContextTransaction? _transaction;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    #region Transaction

    public virtual async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public virtual async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public virtual async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    #endregion Transaction

    public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Message = "Entity can't be null." };

        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Message = ex.Message };

        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TModel>>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _table;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.ToListAsync();

        var result = entities.Select(entity => entity.MapTo<TModel>());
        return new RepositoryResult<IEnumerable<TModel>> { Succeeded = true, StatusCode = 200, Result = result };
    }

    public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _table;

        if (where != null)
            query = query.Where(where);

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        if (sortBy != null)
            query = orderByDescending
                ? query.OrderByDescending(sortBy)
                : query.OrderBy(sortBy);

        var entities = await query.Select(selector).ToListAsync();

        var result = entities.Select(entity => entity!.MapTo<TSelect>());
        return new RepositoryResult<IEnumerable<TSelect>> { Succeeded = true, StatusCode = 200, Result = result };
    }

    public virtual async Task<RepositoryResult<TModel>> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {

        IQueryable<TEntity> query = _table;

        if (includes != null && includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(where);

        if (entity == null)
            return new RepositoryResult<TModel> { Succeeded = false, StatusCode = 404, Message = "Entity not found" };
        var result = entity.MapTo<TModel>();
        return new RepositoryResult<TModel> { Succeeded = true, StatusCode = 200, Result = result };
    }

    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _table.AnyAsync(findBy);
        return !exists
            ? new RepositoryResult<bool> { Succeeded = false, StatusCode = 404, Message = "Entity not found" }
            : new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
    }

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Message = "Entity can't be null." };

        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Message = ex.Message };

        }
    }

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Message = "Entity can't be null." };

        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Message = ex.Message };
        }
    }
}
