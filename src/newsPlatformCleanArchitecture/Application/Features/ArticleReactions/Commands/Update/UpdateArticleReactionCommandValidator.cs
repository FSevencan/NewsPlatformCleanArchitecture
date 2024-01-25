using FluentValidation;

namespace Application.Features.ArticleReactions.Commands.Update;

public class UpdateArticleReactionCommandValidator : AbstractValidator<UpdateArticleReactionCommand>
{
    public UpdateArticleReactionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ArticleId).NotEmpty();
        RuleFor(c => c.IsLiked).NotEmpty();
        RuleFor(c => c.VoterIdentifier).NotEmpty();
    
    }
}