using System.Text;
using Authorization.Application.AppServices.Contexts.User.Repositories;
using Authorization.Application.AppServices.Contexts.User.Services;
using Authorization.Infrastructure.DataAccess;
using Authorization.Infrastructure.DataAccess.Contexts.User.Repositories;
using Authorization.Infrastructure.DataAccess.DbContexts;
using Authorization.Infrastructure.DataAccess.Interfaces;
using Authorization.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return serviceCollection;
    }

    public static IServiceCollection AddDbContextAndConfigureIt(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator<AuthorizationDbContext>, AuthorizationDbContextConfiguration>();
        serviceCollection.AddDbContext<AuthorizationDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<AuthorizationDbContext>>()
                .Configure((DbContextOptionsBuilder<AuthorizationDbContext>)dbOptions)));
        serviceCollection.AddScoped((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<AuthorizationDbContext>()));

        return serviceCollection;
    }
    
    public static IServiceCollection AddAuthenticationWithJwtToken(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = configuration["Jwt:Issuer"],
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = configuration["Jwt:Audience"],
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

        return serviceCollection;
    }
}