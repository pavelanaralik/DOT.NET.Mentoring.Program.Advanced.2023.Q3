using FluentValidation;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.Validators;

public class ProductItemValidator : AbstractValidator<ProductItemDto>
{
    public ProductItemValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("The item name is required.")
            .MaximumLength(50).WithMessage("The item name must be less than 50 characters.");

        RuleFor(item => item.Description)
            .NotEmpty().When(item => !string.IsNullOrEmpty(item.Description))
            .WithMessage("The description cannot be empty if provided.");

        RuleFor(item => item.ImageUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var _)).When(item => !string.IsNullOrEmpty(item.ImageUrl))
            .WithMessage("The image URL is not valid.");

        RuleFor(item => item.Price)
            .GreaterThan(0).WithMessage("The price must be greater than 0.");

        RuleFor(item => item.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("The amount must be a positive integer.");

        RuleFor(item => item.CategoryId)
            .NotEmpty().WithMessage("The item must be assigned to a category.");
    }
}