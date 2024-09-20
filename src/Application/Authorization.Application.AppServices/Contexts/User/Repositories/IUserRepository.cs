using System.Linq.Expressions;
using Authorization.Contracts.Contexts.User;

namespace Authorization.Application.AppServices.Contexts.User.Repositories;

using User = Authorization.Domain.Entities.User;

public interface IUserRepository
{
    Task<Guid> RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken);
    Task<User?> FindUser(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
