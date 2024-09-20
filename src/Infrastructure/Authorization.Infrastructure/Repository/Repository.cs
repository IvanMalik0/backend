using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Repository;

/// <inheritdoc/>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    /// <summary>
    /// Создает экземпляр объекта
    /// </summary>
    /// <param name="dbContext"></param>
    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }
    
    /// <inheritdoc/>
    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));
        return DbSet.Where(filter);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (!entities.Any())
        {
            throw new ArgumentException("Коллекция сущностей пустая", nameof(entities));
        }
        await DbSet.AddRangeAsync(entities, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        DbSet.Update(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        DbSet.Remove(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}