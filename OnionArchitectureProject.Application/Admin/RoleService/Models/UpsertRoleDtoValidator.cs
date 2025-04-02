using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    private readonly IRoleValidationService roleValidationService;

    public UpsertRoleDtoValidator(IRoleValidationService roleValidationService)
    {
        this.roleValidationService = roleValidationService;
        

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Role name can only contain letters, numbers, and underscores.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.")
            .MustAsync(async (name, cancellation) => await roleValidationService.IsRoleNameUniqueAsync(name, cancellation))
            .WithMessage("The role name '{PropertyValue}' is already registered. Please choose a different role name.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }
}