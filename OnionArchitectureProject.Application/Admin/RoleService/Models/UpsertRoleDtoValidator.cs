using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    private readonly IRoleValidationService roleValidationService;

    public UpsertRoleDtoValidator(IRoleValidationService roleValidationService)
    {
        this.roleValidationService = roleValidationService;

        ConfigureNameRules();
        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
    }

    private void ConfigureNameRules()
    {
        RuleFor(x => x.Name)
    .NotEmpty().WithMessage("Role name is required.")
    .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.")
    .Matches("^[a-zA-Z0-9_]+$").WithMessage("Role name can only contain letters, numbers, and underscores.")
    .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.");

        // Only validate uniqueness when creating a new role or when the name is being changed during update
        RuleFor(x => x.Name)
            .MustAsync(async (upsertRoleDto, name, cancellation) =>
            {
                // Skip uniqueness check if this is an update and the name hasn't changed
                if (upsertRoleDto.Id != null)
                {
                    var existingRole = await roleValidationService.GetRoleByIdAsync(upsertRoleDto.Id, cancellation);
                    if (existingRole != null && existingRole.Name == name)
                    {
                        return true;
                    }
                }
                return await roleValidationService.IsRoleNameUniqueAsync(name, cancellation);
            })
            .WithMessage("The role name '{PropertyValue}' is already registered. Please choose a different role name.");

    }
}