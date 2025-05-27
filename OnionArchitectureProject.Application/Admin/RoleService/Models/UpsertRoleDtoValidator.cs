using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    private readonly IRoleValidationService roleValidationService;

    public UpsertRoleDtoValidator(IRoleValidationService roleValidationService)
    {
        this.roleValidationService = roleValidationService ?? throw new ArgumentNullException(nameof(roleValidationService));

        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        ConfigureNameValidation();
        ConfigureNameUniquenessValidation();
        ConfigureDescriptionValidation();
    }

    private void ConfigureNameValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name is required.")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Role name can only contain letters, numbers, and underscores.");
    }

    private void ConfigureNameUniquenessValidation()
    {
        RuleFor(x => x.Name)
            .MustAsync(ValidateNameUniquenessAsync)
            .WithMessage("The role name '{PropertyValue}' is already registered. Please choose a different role name.");
    }

    private void ConfigureDescriptionValidation()
    {
        RuleFor(x => x.Description)
            .MaximumLength(200)
            .WithMessage("Description cannot exceed 200 characters.");
    }

    private async Task<bool> ValidateNameUniquenessAsync(UpsertRoleDto dto, string name, CancellationToken cancellation)
    {
        // Skip uniqueness check if this is an update and the name hasn't changed
        if (dto.Id != null)
        {
            var existingRole = await roleValidationService.GetRoleByIdAsync(dto.Id, cancellation);
            if (existingRole != null && existingRole.Name == name)
            {
                return true;
            }
        }

        return await roleValidationService.IsRoleNameUniqueAsync(name, cancellation);
    }
}