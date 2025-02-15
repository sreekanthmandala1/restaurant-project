using FluentValidation;
using RestaurantMenuAPI.Models;

public class MenuItemValidator : AbstractValidator<MenuItem>
{
    public MenuItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero");
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required")
            .MaximumLength(50)
            .WithMessage("Category must not exceed 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(250)
            .WithMessage("Description must not exceed 250 characters");
    }
}
