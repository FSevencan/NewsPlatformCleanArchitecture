using FluentValidation;

namespace Application.Features.Columnists.Commands.Delete;

public class DeleteColumnistCommandValidator : AbstractValidator<DeleteColumnistCommand>
{
    public DeleteColumnistCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}