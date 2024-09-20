namespace Authorization.Contracts.Contexts.User;

public class UserLoginRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}