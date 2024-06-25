using System.ComponentModel.DataAnnotations;

namespace OnionArchitecture.Authentication.Services.AuthService.Models;
public class RegisterationRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = String.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [Required]
    [MinLength(6)]
    public string UserName { get; set; } = String.Empty;
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = String.Empty;
}