using FluentValidation;

namespace Application.Features.PollOptions.Commands.Delete;

public class DeletePollOptionCommandValidator : AbstractValidator<DeletePollOptionCommand>
{
    public DeletePollOptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}