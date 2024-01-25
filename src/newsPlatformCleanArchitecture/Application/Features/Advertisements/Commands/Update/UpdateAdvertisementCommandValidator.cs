using FluentValidation;

namespace Application.Features.Advertisements.Commands.Update;

public class UpdateAdvertisementCommandValidator : AbstractValidator<UpdateAdvertisementCommand>
{
    public UpdateAdvertisementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty();
        RuleFor(c => c.RedirectUrl).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.ClickCount).NotEmpty();
        RuleFor(c => c.ViewCount).NotEmpty();
    }
}