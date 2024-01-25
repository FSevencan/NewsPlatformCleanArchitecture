using FluentValidation;

namespace Application.Features.ColumnArticles.Commands.Delete;

public class DeleteColumnArticleCommandValidator : AbstractValidator<DeleteColumnArticleCommand>
{
    public DeleteColumnArticleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}