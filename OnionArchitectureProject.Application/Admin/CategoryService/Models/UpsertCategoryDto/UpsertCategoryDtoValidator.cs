using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.CategoryService.Models.UpsertCategoryDto;
public class UpsertCategoryDtoValidator : AbstractValidator<UpsertCategoryDto>
{
    public UpsertCategoryDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
    }
}