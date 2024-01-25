using FluentValidation;

namespace Application.Features.SubCategories.Commands.Create;

public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
       
    }
}