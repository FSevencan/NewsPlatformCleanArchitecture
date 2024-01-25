using FluentValidation;

namespace Application.Features.ArticleReactions.Commands.Delete;

public class DeleteArticleReactionCommandValidator : AbstractValidator<DeleteArticleReactionCommand>
{
    public DeleteArticleReactionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}