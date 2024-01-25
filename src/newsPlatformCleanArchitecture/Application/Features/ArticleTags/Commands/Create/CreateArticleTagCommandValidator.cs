using FluentValidation;

namespace Application.Features.ArticleTags.Commands.Create;

public class CreateArticleTagCommandValidator : AbstractValidator<CreateArticleTagCommand>
{
    public CreateArticleTagCommandValidator()
    {
        RuleFor(c => c.ArticleId).NotEmpty();
        RuleFor(c => c.TagId).NotEmpty();
        
    }
}