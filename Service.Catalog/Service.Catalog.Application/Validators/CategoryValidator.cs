using FluentValidation;
using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.Validators;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("The category name is required.")
            .MaximumLength(50).WithMessage("The category name must be less than 50 characters.");

        RuleFor(category => category.ImageUrl)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out var _)).When(category => !string.IsNullOrEmpty(category.ImageUrl))
            .WithMessage("The image URL is not valid.");

        // If you have specific rules for the parent category, you can add them here.
        RuleFor(category => category.ParentCategoryId)
            .GreaterThanOrEqualTo(0).When(category => category.ParentCategoryId.HasValue)
            .WithMessage("The parent category ID must be a positive integer.");
    }
}