using FluentValidation;

namespace Application.Features.PollVotes.Commands.Delete;

public class DeletePollVoteCommandValidator : AbstractValidator<DeletePollVoteCommand>
{
    public DeletePollVoteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}