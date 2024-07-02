using System.ComponentModel.DataAnnotations;

namespace OnionArchitectureProject.Authentication.Services.AccountService.Models;
public class RegisterDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string RePassword { get; set; } = string.Empty;
}