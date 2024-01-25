using FluentValidation;

namespace Application.Features.Columnists.Commands.Update;

public class UpdateColumnistCommandValidator : AbstractValidator<UpdateColumnistCommand>
{
    public UpdateColumnistCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Biography).NotEmpty();
        RuleFor(c => c.ProfilePicture).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.LinkedinLink).NotEmpty();
    }
}