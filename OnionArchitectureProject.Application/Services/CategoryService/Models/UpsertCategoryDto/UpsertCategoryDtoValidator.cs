using FluentValidation;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models.UpsertCategoryDto;
public class UpsertCategoryDtoValidator : AbstractValidator<UpsertCategoryDto>
{
    public UpsertCategoryDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
    }
}