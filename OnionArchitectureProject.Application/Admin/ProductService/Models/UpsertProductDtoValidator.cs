using FluentValidation;

namespace OnionArchitectureProject.Application.Admin.ProductService.Models;

public class UpsertProductDtoValidator : AbstractValidator<UpsertProductDto>
{
    public UpsertProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.")
            .Matches("^[a-zA-Z0-9\\s\\-\\.,]+$").WithMessage("Product name can only contain letters, numbers, spaces, hyphens, dots, and commas.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .Length(10, 1000).WithMessage("Product description must be between 10 and 1000 characters.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Product image URL is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.")
            .LessThan(1000000).WithMessage("Product price cannot exceed 1,000,000.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");
    }
}