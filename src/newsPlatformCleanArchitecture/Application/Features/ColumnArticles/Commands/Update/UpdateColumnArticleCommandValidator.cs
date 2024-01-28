using FluentValidation;

namespace Application.Features.ColumnArticles.Commands.Update;

public class UpdateColumnArticleCommandValidator : AbstractValidator<UpdateColumnArticleCommand>
{
    public UpdateColumnArticleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ColumnistId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.FeaturedImage).NotEmpty();
       
    }
}