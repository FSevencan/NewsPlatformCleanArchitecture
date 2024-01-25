using FluentValidation;

namespace Application.Features.Articles.Commands.Delete;

public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    public DeleteArticleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}