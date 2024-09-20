using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Application.AppServices.Contexts.User.Repositories;
using Authorization.Application.AppServices.Helpers;
using Authorization.Contracts.Contexts.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Application.AppServices.Contexts.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<Guid> RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.FindUser(x => x.Login == request.Login, cancellationToken) is not null)
        {
            throw new Exception("Пользователь с таким логином уже существует");
        }

        var id = await _repository.RegisterAsync(request, cancellationToken);
        return id;
    }

    public async Task<string> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.FindUser(x => x.Login == request.Login, cancellationToken);
        if (existingUser is null) throw new Exception("Неверный логин или пароль");

        var password = PasswordHelper.HashPassword(request.Password);
        
        if (password != existingUser.Password) throw new Exception("Неверный логин или пароль");

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, existingUser.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, existingUser.Role)
        };

        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: creds 
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }
}