using FluentValidation;

namespace OnionArchitectureProject.Application.Services.CategoryService.Models.CategoryDto;
public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
    }
}