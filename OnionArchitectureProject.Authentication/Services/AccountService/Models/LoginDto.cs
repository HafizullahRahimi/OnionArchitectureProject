using System.ComponentModel.DataAnnotations;

namespace OnionArchitectureProject.Authentication.Services.AccountService.Models;
public class LoginDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}