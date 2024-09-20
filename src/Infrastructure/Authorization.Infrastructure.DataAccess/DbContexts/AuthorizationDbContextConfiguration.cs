using Authorization.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authorization.Infrastructure.DataAccess.DbContexts;

/// <summary>
/// Конфигурация <see cref="AuthorizationDbContext"/> контекста 
/// </summary>
public class AuthorizationDbContextConfiguration : IDbContextOptionsConfigurator<AuthorizationDbContext>
{
    private const string PostgresConnectionStringName = "PostgresAuthorizationDb";
    
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Конструктор <see cref="AuthorizationDbContextConfiguration"/>
    /// </summary>
    /// <param name="configuration">Конфигурации</param>
    /// <param name="loggerFactory">Фабрика логгеров</param>
    public AuthorizationDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }
    
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">Строка подключения не найдена в конфигурациях</exception>
    public void Configure(DbContextOptionsBuilder<AuthorizationDbContext> options)
    {
        var connectionString = _configuration.GetConnectionString(PostgresConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Не найдена строка подключения с именем '{PostgresConnectionStringName}'");
        options.UseNpgsql(connectionString);
        options.UseLoggerFactory(_loggerFactory);
        options.UseLazyLoadingProxies();
    }
}