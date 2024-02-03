using FluentValidation;

namespace Application.Features.ArticleReactions.Commands.Create;

public class CreateArticleReactionCommandValidator : AbstractValidator<CreateArticleReactionCommand>
{
    public CreateArticleReactionCommandValidator()
    {
        RuleFor(c => c.ArticleId).NotEmpty();
        RuleFor(c => c.VoterIdentifier).NotEmpty();
       
    }
}