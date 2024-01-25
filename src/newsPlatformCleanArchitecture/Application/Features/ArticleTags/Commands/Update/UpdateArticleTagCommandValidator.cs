using FluentValidation;

namespace Application.Features.ArticleTags.Commands.Update;

public class UpdateArticleTagCommandValidator : AbstractValidator<UpdateArticleTagCommand>
{
    public UpdateArticleTagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ArticleId).NotEmpty();
        RuleFor(c => c.TagId).NotEmpty();
       
    }
}