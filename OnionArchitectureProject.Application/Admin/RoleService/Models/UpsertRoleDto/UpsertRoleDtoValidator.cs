using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.RoleService.Models.UpsertRoleDto;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    public UpsertRoleDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
    }
}