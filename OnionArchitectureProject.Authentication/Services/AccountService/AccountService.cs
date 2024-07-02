using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Profiles;

namespace OnionArchitectureProject.Authentication.Services.AccountService;
public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public AccountService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<ApplicationUserProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<ResponseDto> RegisterUserAsync(RegisterDto registerDto)
    {
        var existingUser = await FindUserByEmail(registerDto.Email);
        if (existingUser != null)
        {
            return new ResponseDto(false, $"The email:'{registerDto.Email}' already exists.");
            throw new Exception($"The email: '{registerDto.Email}' already exists.");
        }

        var user = _mapper.Map<ApplicationUser>(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return new ResponseDto(false, result.Errors.First().Description);
        }

        await _userManager.AddToRoleAsync(user, "Employee");
        return new ResponseDto(true, $"{user.Id}");
    }

    private async Task<ApplicationUser?> FindUserByEmail(string email) =>
        await _userManager.FindByEmailAsync(email);    
    
    private async Task<ApplicationUser?> FindUserByName(string name) =>
        await _userManager.FindByNameAsync(name);
}