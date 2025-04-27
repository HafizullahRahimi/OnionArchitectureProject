namespace OnionArchitectureProject.Application.Admin.UserService.Models;
public class UpdateUserDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}