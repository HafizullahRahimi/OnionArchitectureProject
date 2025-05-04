using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.UserService.Models;

public class UserRoleAssignmentDtoValidator : AbstractValidator<UserRoleAssignmentDto>
{
    public UserRoleAssignmentDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}