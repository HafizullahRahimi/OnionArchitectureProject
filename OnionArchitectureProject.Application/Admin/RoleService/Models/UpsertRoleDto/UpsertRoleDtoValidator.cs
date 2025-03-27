using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    private readonly IRoleService _roleService;

    public UpsertRoleDtoValidator(IRoleService roleService)
    {
        _roleService = roleService;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Role name can only contain letters, numbers, and underscores.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.")
            .MustAsync(async (name, cancellation) => !await _roleService.ExistsByRoleNameAsync(name, cancellation))
            .WithMessage("The role name '{PropertyValue}' is already registered. Please choose a different role name.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }
}