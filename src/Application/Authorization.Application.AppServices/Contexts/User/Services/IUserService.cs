using Authorization.Contracts.Contexts.User;

namespace Authorization.Application.AppServices.Contexts.User.Services;

public interface IUserService
{
    public Task<Guid> RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken);

    public Task<string> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken);

    public Task<UserDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}