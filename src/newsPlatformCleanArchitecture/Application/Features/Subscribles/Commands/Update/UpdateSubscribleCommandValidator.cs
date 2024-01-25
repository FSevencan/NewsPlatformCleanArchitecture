using FluentValidation;

namespace Application.Features.Subscribles.Commands.Update;

public class UpdateSubscribleCommandValidator : AbstractValidator<UpdateSubscribleCommand>
{
    public UpdateSubscribleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.IsConfirmed).NotEmpty();
    }
}