using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArchitectureProject.Authentication.Services.TokenService;

namespace OnionArchitectureProject.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    // GET: api/Token
    [HttpGet("UserName")]
    public IActionResult GetUserName()
    {
        return Ok(_tokenService.GetCurrentUserName());
    }

    // GET: api/Token
    [HttpGet("UserId")]
    public IActionResult GetUserId()
    {
        return Ok(_tokenService.GetCurrentUserId());
    }

    // GET: api/Token
    [HttpGet("UserEmail")]
    public IActionResult GetUserEmail()
    {
        return Ok(_tokenService.GetCurrentUserEmail());
    }
}
