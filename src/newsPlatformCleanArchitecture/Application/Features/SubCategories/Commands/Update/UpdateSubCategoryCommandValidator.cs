using FluentValidation;

namespace Application.Features.SubCategories.Commands.Update;

public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
{
    public UpdateSubCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
       
    }
}