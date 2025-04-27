using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.UserService.Models;
using OnionArchitectureProject.Application.Common.Models;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Application.Admin.UserService;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IServiceScopeFactory scopeFactory;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IServiceScopeFactory scopeFactory)
    {
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        try
        {
            var users = await userRepository.GetAllAsync();
            var userDtos = await Task.WhenAll(
                users.Select(MapToUserDtoAsync)
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
            var userNameExists = await userRepository.UserNameExistsAsync(createUserDto.UserName);
            if (userNameExists)
            {
                return new OperationResult(false, $"User '{createUserDto.UserName}' already exists.");
            }

            var emailExists = await userRepository.EmailExistsAsync(createUserDto.Email);
            if (emailExists)
            {
                return new OperationResult(false, $"Email '{createUserDto.Email}' is already registered.");
            }

            var user = mapper.Map<User>(createUserDto);
            await userRepository.CreateAsync(user, createUserDto.Password);

            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(UpdateUserDto updateUserDto)
    {
        try
        {
            var existingUser = await userRepository.GetByIdAsync(updateUserDto.Id);
            if (existingUser == null)
            {
                return new OperationResult(false, $"User with ID '{updateUserDto.Id}' not found.");
            }

            if (existingUser.UserName != updateUserDto.UserName)
            {
                var userNameExists = await userRepository.UserNameExistsAsync(updateUserDto.UserName);
                if (userNameExists)
                {
                    return new OperationResult(false, $"Username '{updateUserDto.UserName}' is already taken.");
                }
            }

            if (existingUser.Email != updateUserDto.Email)
            {
                var emailExists = await userRepository.EmailExistsAsync(updateUserDto.Email);
                if (emailExists)
                {
                    return new OperationResult(false, $"Email '{updateUserDto.Email}' is already registered.");
                }
            }

            mapper.Map(updateUserDto, existingUser);

            // Update password if provided
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                await userRepository.ChangePasswordAsync(existingUser.Id, updateUserDto.Password);
            }

            await userRepository.UpdateAsync(existingUser);

            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<UserDto> MapToUserDtoAsync(User user)
    {
        var userDto = mapper.Map<UserDto>(user);
        userDto.CreatedByUserName = await GetUserNameByIdAsync(user.CreatedBy) ?? "Unknown";
        userDto.ModifiedByUserName = await GetUserNameByIdAsync(user.ModifiedBy) ?? "Unknown";
        return userDto;
    }

    private async Task<string?> GetUserNameByIdAsync(string userId)
    {
        using var scope = scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var user = await userRepository.GetByIdAsync(userId);
        return user?.UserName;
    }
}