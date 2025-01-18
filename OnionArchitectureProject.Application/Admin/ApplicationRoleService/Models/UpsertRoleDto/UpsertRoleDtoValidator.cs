using FluentValidation;
using OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;

namespace OnionArchitectureProject.Application.Admin.ApplicationRoleService.Models.UpsertRoleDto;
public class UpsertRoleDtoValidator : AbstractValidator<UpsertRoleDto>
{
    public UpsertRoleDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
    }
}