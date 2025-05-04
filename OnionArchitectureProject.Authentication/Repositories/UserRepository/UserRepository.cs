using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Authentication.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;

    public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var appUsers = await userManager.Users
            .OrderByDescending(u => u.CreatedUtcDate)
            .ToListAsync();
        return mapper.Map<List<User>>(appUsers);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        var appUser = await userManager.FindByIdAsync(id);
        return appUser != null ? mapper.Map<User>(appUser) : null;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var appUser = await userManager.FindByEmailAsync(email);
        return appUser != null ? mapper.Map<User>(appUser) : null;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var appUser = await userManager.Users
              .FirstOrDefaultAsync(r => r.UserName == userName);
        return appUser != null ? mapper.Map<User>(appUser) : null;
    }

    public async Task<bool> ExistAsync(string id)
    {
        var user = await GetByIdAsync(id);
        return user != null;
    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        var user = await GetByUserNameAsync(userName);
        return user != null;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await GetByEmailAsync(email);
        return user != null;
    }

    public async Task<User> CreateAsync(User user, string password)
    {
        user.Id = Guid.NewGuid().ToString();
        var appUser = mapper.Map<ApplicationUser>(user);
        await userManager.CreateAsync(appUser, password);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        var existingAppUser = await GetAppUserByIdAsync(user.Id);
        if (existingAppUser != null)
        {
            mapper.Map(user, existingAppUser);
            await userManager.UpdateAsync(existingAppUser);
        }
    }

    public async Task DeleteAsync(string id)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.DeleteAsync(existingAppUser);
    }

    public async Task DeleteAsync(User user) =>
       await DeleteAsync(user.Id);

    public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
    {
        var appUser = await GetAppUserByIdAsync(userId);
        if (appUser == null)
            return false;

        var hasPassword = await userManager.HasPasswordAsync(appUser);
        if (hasPassword)
        {
            var removeResult = await userManager.RemovePasswordAsync(appUser);
            if (!removeResult.Succeeded)
                return false;
        }

        var addResult = await userManager.AddPasswordAsync(appUser, newPassword);
        return addResult.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var appUser = await GetAppUserByIdAsync(userId);
        if (appUser == null)
            return false;

        var result = await userManager.ChangePasswordAsync(appUser, currentPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<IList<string>?> GetRolesAsync(string userId)
    {
        var appUser = await GetAppUserByIdAsync(userId);
        return appUser is null
            ? null
            : await userManager.GetRolesAsync(appUser);
    }

    public async Task AddToRolesAsync(string userId, IList<string> newRoles)
    {
        var appUser = await GetAppUserByIdAsync(userId);
        if (appUser != null)
            await userManager.AddToRolesAsync(appUser, newRoles);
    }

    public async Task RemoveFromRolesAsync(string userId, IList<string> currentRoles)
    {
        var appUser = await GetAppUserByIdAsync(userId);
        if (appUser != null)
            await userManager.RemoveFromRolesAsync(appUser, currentRoles);
    }

    public async Task UpdateUserRolesAsync(string userId, IList<string> newRoles)
    {
        var currentRoles = await GetRolesAsync(userId);
        if (currentRoles?.Count > 0)
        {
            await RemoveFromRolesAsync(userId, currentRoles);
        }

        if (newRoles.Count > 0)
        {
            await AddToRolesAsync(userId, newRoles);
        }
    }

    private async Task<ApplicationUser?> GetAppUserByIdAsync(string userId) =>
        await userManager.FindByIdAsync(userId);
}