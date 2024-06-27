using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArchitectureProject.Authentication.Services.CurrentUserService;

namespace OnionArchitectureProject.Api.Controllers;

[Authorize]
[Route("api/currentuser")]
[ApiController]
public class CurrentUserController : ControllerBase
{
    private readonly ICurrentUserService currentUserService;

    public CurrentUserController(ICurrentUserService currentUserService)
    {
        this.currentUserService = currentUserService;
    }

    // GET: api/currentuser/name
    [HttpGet("name")]
    public IActionResult GetUserName()
    {
        return Ok(currentUserService.GetName());
    }

    // GET: api/currentuser/id
    [HttpGet("id")]
    public IActionResult GetUserId()
    {
        return Ok(currentUserService.GetId());
    }

    // GET: api/currentuser/email
    [HttpGet("email")]
    public IActionResult GetUserEmail()
    {
        return Ok(currentUserService.GetEmail());
    }
}
