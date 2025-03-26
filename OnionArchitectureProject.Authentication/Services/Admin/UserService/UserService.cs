using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Application.Admin.UserService;
using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Authentication.Models;

namespace OnionArchitectureProject.Authentication.Services.Admin.UserService;
public class UserService(UserManager<ApplicationUser> userManager, IMapper mapper) : IUserService
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IMapper mapper = mapper;

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var appUsers = await userManager.Users.OrderByDescending(u => u.CreatedUtcDate).ToListAsync();
        return mapper.Map<List<UserDto>>(appUsers);
    }
}
