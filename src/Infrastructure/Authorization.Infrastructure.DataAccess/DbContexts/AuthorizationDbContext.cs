using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.DataAccess.DbContexts;

/// <summary>
/// Основной контекст БД.
/// </summary>
public class AuthorizationDbContext : DbContext
{
    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public AuthorizationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}