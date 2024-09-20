using System.Linq.Expressions;

namespace Authorization.Infrastructure.Repository;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="TEntity">Сущность</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Получить все записи <see cref="TEntity"/>
    /// </summary>
    /// <returns>Все записи <see cref="TEntity"/></returns>
    IQueryable<TEntity> GetAll();
    
    /// <summary>
    /// Получить отфильтрованные записи <see cref="TEntity"/>
    /// </summary>
    /// <param name="filter">Выражение фильтрации</param>
    /// <returns>Отфильтрованные записи <see cref="TEntity"/></returns>
    IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);
    
    /// <summary>
    /// Получить объект <see cref="TEntity"/> по ID
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Запись <see cref="TEntity"/> из бд или null, если запись не найдена</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавить объект <see cref="TEntity"/> в бд
    /// </summary>
    /// <param name="model">Объект для добавления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет коллекцию сущностей <see cref="TEntity"/> в бд
    /// </summary>
    /// <param name="entities">Коллекция <see cref="TEntity"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить объект <see cref="TEntity"/> в бд
    /// </summary>
    /// <param name="model">Существующий объект с обновленными данными</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить объект <see cref="TEntity"/> из бд
    /// </summary>
    /// <param name="model">Существующий объект</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAsync(TEntity model, CancellationToken cancellationToken);
}