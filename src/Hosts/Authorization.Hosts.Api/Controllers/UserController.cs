using Authorization.Application.AppServices.Contexts.User.Services;
using Authorization.Contracts.Contexts.User;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Hosts.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.RegisterAsync(request, cancellationToken);

        return CreatedAtRoute(nameof(GetById), new { Id = result.ToString() }, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.LoginAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = nameof(GetById))]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }
}