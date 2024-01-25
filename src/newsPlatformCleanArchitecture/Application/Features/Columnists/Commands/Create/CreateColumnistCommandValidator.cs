using FluentValidation;

namespace Application.Features.Columnists.Commands.Create;

public class CreateColumnistCommandValidator : AbstractValidator<CreateColumnistCommand>
{
    public CreateColumnistCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Biography).NotEmpty();
        RuleFor(c => c.ProfilePicture).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.LinkedinLink).NotEmpty();
    }
}