using FluentValidation;

namespace Application.Features.Polls.Commands.Delete;

public class DeletePollCommandValidator : AbstractValidator<DeletePollCommand>
{
    public DeletePollCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}