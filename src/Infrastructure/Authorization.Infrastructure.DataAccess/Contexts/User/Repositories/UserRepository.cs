using System.Linq.Expressions;
using Authorization.Application.AppServices.Contexts.User.Repositories;
using Authorization.Application.AppServices.Helpers;
using Authorization.Contracts.Contexts.User;
using Authorization.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Authorization.Application.AppServices.Contexts.User.Common;


namespace Authorization.Infrastructure.DataAccess.Contexts.User.Repositories;

using User = Authorization.Domain.Entities.User;

public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;

    public UserRepository(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var entity = new User{
            Id = new Guid(),
            Login = request.Login,
            Password = PasswordHelper.HashPassword(request.Password),
            Role = UserConstants.DefaultAuthorizationRole};

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task<User?> FindUser(Expression<Func<User, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user =  await _repository.GetByIdAsync(id, cancellationToken);
        if (user is null) return null;
        var dto = new UserDto
        {
            Id = user.Id,
            Login = user.Login,
            Role = user.Role
        };

        return dto;
    }
}