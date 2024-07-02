using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Models;
using OnionArchitectureProject.Authentication.Services.AccountService.Profiles;

namespace OnionArchitectureProject.Authentication.Services.AccountService;
public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;

    public AccountService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<ApplicationUserProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<ResponseDto> RegisterUserAsync(RegisterDto registerDto)
    {
        var existingUser = await FindUserByEmailAsync(registerDto.Email);
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

    public async Task<LoginResponseDto> LoginUserAsync(LoginDto loginDto)
    {
        var user = await FindUserByNameAsync(loginDto.UserName);
        if (user == null) return new LoginResponseDto(false, $"The password or username is incorrect");

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.RememberMe, false);

        if (result.Succeeded)
        {
            return new LoginResponseDto(true, $"Login Succeeded ", user);
        }
        else if (result.RequiresTwoFactor)
        {
            return new LoginResponseDto(false, $"LoginWith2fa", user);

        }
        else if (result.IsLockedOut)
        {
            return new LoginResponseDto(false, $"Your account has been locked");

        }

        return new LoginResponseDto(false, $"The password or username is incorrect");
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<ApplicationUser?> FindUserByEmailAsync(string email) =>
        await _userManager.FindByEmailAsync(email);

    private async Task<ApplicationUser?> FindUserByNameAsync(string name) =>
        await _userManager.FindByNameAsync(name);
}