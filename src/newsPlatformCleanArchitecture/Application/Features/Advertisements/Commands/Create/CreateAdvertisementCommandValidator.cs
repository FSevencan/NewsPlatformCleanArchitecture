using FluentValidation;

namespace Application.Features.Advertisements.Commands.Create;

public class CreateAdvertisementCommandValidator : AbstractValidator<CreateAdvertisementCommand>
{
    public CreateAdvertisementCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty();
        RuleFor(c => c.RedirectUrl).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.ClickCount).NotEmpty();
        RuleFor(c => c.ViewCount).NotEmpty();
    }
}