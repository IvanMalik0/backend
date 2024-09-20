using Authorization.Infrastructure.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Hosts.DbMigrator.DbContext;

/// <summary>
/// Контекст БД для миграций.
/// </summary>
public class MigratorDbContext : AuthorizationDbContext
{
    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public MigratorDbContext(DbContextOptions options) : base(options)
    {
    }
}