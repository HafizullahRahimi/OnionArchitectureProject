using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Application.Admin.UserService;
using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication;

namespace OnionArchitectureProject.Authentication.Services.Admin.UserService;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        try
        {
            var appUsers = await _userManager.Users
                .OrderByDescending(u => u.CreatedUtcDate)
                .ToListAsync();

            var userDtos = await Task.WhenAll(
                appUsers.Select(MapToUserDtoAsync)
            );
            return userDtos.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> CreateAsync(CreateUserDto createUserDto)
    {
        try
        {
            var userExists = await _userRepository.ExistsByUserNameAsync(createUserDto.UserName, CancellationToken.None);
            if (userExists)
            {
                return new OperationResult(false, $"User '{createUserDto.UserName}' already exists.");
            }

            var emailExists = await _userRepository.ExistsByEmailAsync(createUserDto.Email, CancellationToken.None);
            if (emailExists)
            {
                return new OperationResult(false, $"Email '{createUserDto.Email}' is already registered.");
            }

            var user = _mapper.Map<ApplicationUser>(createUserDto);
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return new OperationResult(false, errorMessage);
            }

            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<UserDto> MapToUserDtoAsync(ApplicationUser appUser)
    {
        var userDto = _mapper.Map<UserDto>(appUser);
        userDto.CreatedByUserName = await GetUserNameByIdAsync(appUser.CreatedBy) ?? "Unknown";
        userDto.ModifiedByUserName = await GetUserNameByIdAsync(appUser.ModifiedBy) ?? "Unknown";
        return userDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId) =>
        await _userRepository.GetUserNameByUserIdAsync(userId);
}
