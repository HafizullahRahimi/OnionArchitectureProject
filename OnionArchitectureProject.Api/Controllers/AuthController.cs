using Microsoft.AspNetCore.Mvc;
using OnionArchitectureProject.Authentication.Services.AuthService;
using OnionArchitectureProject.Authentication.Services.AuthService.Models;

namespace OnionArchitectureProject.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(AuthRequest request)
    {
        var result = await _authService.Login(request);
        if (result != null)
        {
            return Ok(result.Token);
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegisterationRequest request)
    {
        return Ok(await _authService.Register(request));
    }
}
