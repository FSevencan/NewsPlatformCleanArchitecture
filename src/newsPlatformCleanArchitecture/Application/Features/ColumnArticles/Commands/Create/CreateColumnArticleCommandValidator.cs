using FluentValidation;

namespace Application.Features.ColumnArticles.Commands.Create;

public class CreateColumnArticleCommandValidator : AbstractValidator<CreateColumnArticleCommand>
{
    public CreateColumnArticleCommandValidator()
    {
        RuleFor(c => c.ColumnistId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.FeaturedImage).NotEmpty();
        RuleFor(c => c.Columnist).NotEmpty();
        RuleFor(c => c.Category).NotEmpty();
    }
}