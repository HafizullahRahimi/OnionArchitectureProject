using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.UserService.Models;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    private readonly IUserValidationService userValidationService;

    public CreateUserDtoValidator(IUserValidationService userValidationService)
    {
        this.userValidationService = userValidationService ?? throw new ArgumentNullException(nameof(userValidationService));

        ConfigureUserNameRules();
        ConfigureEmailRules();
        ConfigurePasswordRules();
    }

    private void ConfigureUserNameRules()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
            .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens")
            .MustAsync(async (userName, cancellation) =>
                await userValidationService.IsUserNameUniqueAsync(userName, cancellation))
            .WithMessage("The username '{PropertyValue}' is already in use. Please choose a different username.");
    }

    private void ConfigureEmailRules()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("A valid email address is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .MustAsync(async (email, cancellation) =>
                await userValidationService.IsEmailUniqueAsync(email, cancellation))
            .WithMessage("The email '{PropertyValue}' is already registered. Please use a different email address.");
    }

    private void ConfigurePasswordRules()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Please confirm your password")
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}