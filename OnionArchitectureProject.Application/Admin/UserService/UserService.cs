using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
using OnionArchitectureProject.Application.Admin.CategoryService.Models;
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

    public async Task<OperationResult> CreateAsync(UpsertUserDto upsertUserDto)
    {
        try
        {
            var userNameExists = await userRepository.UserNameExistsAsync(upsertUserDto.UserName);
            if (userNameExists)
            {
                return new OperationResult(false, $"User '{upsertUserDto.UserName}' already exists.");
            }

            var emailExists = await userRepository.EmailExistsAsync(upsertUserDto.Email);
            if (emailExists)
            {
                return new OperationResult(false, $"Email '{upsertUserDto.Email}' is already registered.");
            }

            var user = mapper.Map<User>(upsertUserDto);
            await userRepository.CreateAsync(user, upsertUserDto.Password);

            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(UpsertUserDto upsertUserDto)
    {
        try
        {
            var existingUser = await userRepository.GetByIdAsync(upsertUserDto.Id);
            if (existingUser == null)
            {
                return new OperationResult(false, $"User with ID '{upsertUserDto.Id}' not found.");
            }

            if (existingUser.UserName != upsertUserDto.UserName)
            {
                var userNameExists = await userRepository.UserNameExistsAsync(upsertUserDto.UserName);
                if (userNameExists)
                {
                    return new OperationResult(false, $"Username '{upsertUserDto.UserName}' is already taken.");
                }
            }

            if (existingUser.Email != upsertUserDto.Email)
            {
                var emailExists = await userRepository.EmailExistsAsync(upsertUserDto.Email);
                if (emailExists)
                {
                    return new OperationResult(false, $"Email '{upsertUserDto.Email}' is already registered.");
                }
            }

            mapper.Map(upsertUserDto, existingUser);

            // Update password if provided
            if (!string.IsNullOrEmpty(upsertUserDto.Password))
            {
                await userRepository.ChangePasswordAsync(existingUser.Id, upsertUserDto.Password);
            }

            await userRepository.UpdateAsync(existingUser);

            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<OperationResult> DeleteAsync(string userId)
    {
        try
        {
            var existingUser = await userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                return new OperationResult(false, $"User with ID '{userId}' not found.");
            }

            await userRepository.DeleteAsync(existingUser);
            return new OperationResult(true, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public UpsertUserDto MapToUpsertUserDto(UserDto userDto)
    {
        return mapper.Map<UpsertUserDto>(userDto);
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