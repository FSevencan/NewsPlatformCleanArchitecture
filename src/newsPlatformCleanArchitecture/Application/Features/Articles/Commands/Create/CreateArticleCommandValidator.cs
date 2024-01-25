using FluentValidation;

namespace Application.Features.Articles.Commands.Create;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(c => c.SubcategoryId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.Summary).NotEmpty();
        RuleFor(c => c.FeaturedImage).NotEmpty();
      
       
    }
}