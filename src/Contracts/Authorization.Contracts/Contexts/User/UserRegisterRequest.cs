namespace Authorization.Contracts.Contexts.User;

public class UserRegisterRequest
{
    //public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}