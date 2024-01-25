using FluentValidation;

namespace Application.Features.ArticleTags.Commands.Delete;

public class DeleteArticleTagCommandValidator : AbstractValidator<DeleteArticleTagCommand>
{
    public DeleteArticleTagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}