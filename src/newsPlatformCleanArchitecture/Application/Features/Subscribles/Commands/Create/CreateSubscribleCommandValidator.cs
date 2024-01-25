using FluentValidation;

namespace Application.Features.Subscribles.Commands.Create;

public class CreateSubscribleCommandValidator : AbstractValidator<CreateSubscribleCommand>
{
    public CreateSubscribleCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.IsConfirmed).NotEmpty();
    }
}