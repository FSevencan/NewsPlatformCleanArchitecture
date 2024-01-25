using FluentValidation;

namespace Application.Features.Articles.Commands.Update;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SubcategoryId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.Summary).NotEmpty();
        RuleFor(c => c.FeaturedImage).NotEmpty();
        RuleFor(c => c.Slug).NotEmpty();
        RuleFor(c => c.TotalLikes).NotEmpty();
        RuleFor(c => c.TotalDislikes).NotEmpty();
        RuleFor(c => c.SubCategory).NotEmpty();
    }
}