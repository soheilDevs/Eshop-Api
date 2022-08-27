using Common.Application;
using FluentValidation;

namespace Shop.Application.Categories.AddChild;

public class AddCategoryChildCommandValidator:AbstractValidator<AddCategoryChildCommand>
{
    public AddCategoryChildCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug)
            .NotNull().NotEmpty().WithMessage(ValidationMessages.required("Slug"));
    }
}