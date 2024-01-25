using FluentValidation;

namespace Application.Features.SubCategories.Commands.Delete;

public class DeleteSubCategoryCommandValidator : AbstractValidator<DeleteSubCategoryCommand>
{
    public DeleteSubCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}