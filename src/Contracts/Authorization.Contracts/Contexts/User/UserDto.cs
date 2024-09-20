namespace Authorization.Contracts.Contexts.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
}