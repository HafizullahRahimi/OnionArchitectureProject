using FluentValidation;
using OnionArchitectureProject.Application.Services.ProductService.Models.UpsertProductDto;

namespace OnionArchitectureProject.Web.Validators;
public class UpsertProductDtoValidator : AbstractValidator<UpsertProductDto>
{
    public UpsertProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Image).NotEmpty().WithMessage("Image URL is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category ID is required.");
    }
}