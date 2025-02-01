using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureProject.Authentication.Models;
using OnionArchitectureProject.Domain.Authentication.Users;

namespace OnionArchitectureProject.Authentication.Repositories.UserRepository;
public class UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper) : IUserRepository
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IMapper mapper = mapper;

    public async Task<List<User>> GetAllAsync()
    {
        var appUsers = await userManager.Users.ToListAsync();
        return mapper.Map<List<User>>(appUsers);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        var appUser = await userManager.FindByIdAsync(id);
        return appUser != null ? mapper.Map<User>(appUser) : null;
    }

    public async Task<bool> ExistAsync(string id)
    {
        var appUser = await GetAppUserByIdAsync(id);
        return appUser != null;
    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        var appUser = await userManager.Users
              .IgnoreQueryFilters()
              .FirstOrDefaultAsync(r => r.UserName == userName);
        return appUser != null;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var appUser = await userManager.FindByEmailAsync(email);
        return appUser != null;
    }

    public async Task<string?> GetUserNemeByIdAsync(string id)
    {
        var appUser = await GetAppUserByIdAsync(id);
        return appUser?.UserName ?? null;
    }

    public async Task<User> CreateAsync(User entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        var appUser = mapper.Map<ApplicationUser>(entity);
        await userManager.CreateAsync(appUser);
        return entity;
    }

    public async Task UpdateAsync(User entity)
    {
        var existingAppUser = await GetAppUserByIdAsync(entity.Id);
        if (existingAppUser != null)
        {
            mapper.Map(entity, existingAppUser);
            await userManager.UpdateAsync(existingAppUser);
        }
    }

    public async Task DeleteAsync(User entity) =>
        await DeleteAsync(entity.Id);

    public async Task DeleteAsync(string id)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.DeleteAsync(existingAppUser);
    }


    #region User status
    public async Task LockUserAsync(string id, int daysToLock)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.SetLockoutEndDateAsync(existingAppUser, DateTimeOffset.UtcNow.AddDays(daysToLock));
    }

    public async Task UnlockUserAsync(string id)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.SetLockoutEndDateAsync(existingAppUser, null);
    }

    public async Task SetUserStatusAsync(string id, bool isEnabled)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
        {
            existingAppUser.LockoutEnabled = !isEnabled;
            await userManager.UpdateAsync(existingAppUser);
        }
    }
    #endregion

    #region Password management
    public async Task ChangePasswordAsync(string id, string currentPassword, string newPassword)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.ChangePasswordAsync(existingAppUser, currentPassword, newPassword);
    }

    public async Task<string?> GeneratePasswordResetTokenAsync(string email)
    {
        var existingAppUser = await GetAppUserByEmailAsync(email);
        if (existingAppUser != null)
            return await userManager.GeneratePasswordResetTokenAsync(existingAppUser);
        return null;
    }

    public async Task ResetPasswordAsync(string id, string token, string newPassword)
    {
        var existingAppUser = await GetAppUserByIdAsync(id);
        if (existingAppUser != null)
            await userManager.ResetPasswordAsync(existingAppUser, token, newPassword);
    }
    #endregion

    private async Task<ApplicationUser?> GetAppUserByIdAsync(string id) =>
       await userManager.FindByIdAsync(id);
    private async Task<ApplicationUser?> GetAppUserByEmailAsync(string email) =>
       await userManager.FindByEmailAsync(email);
}