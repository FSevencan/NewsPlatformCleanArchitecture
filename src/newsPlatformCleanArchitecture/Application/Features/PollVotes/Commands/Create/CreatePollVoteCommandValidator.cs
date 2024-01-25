using FluentValidation;

namespace Application.Features.PollVotes.Commands.Create;

public class CreatePollVoteCommandValidator : AbstractValidator<CreatePollVoteCommand>
{
    public CreatePollVoteCommandValidator()
    {
        RuleFor(c => c.PollId).NotEmpty();
        RuleFor(c => c.PollOptionId).NotEmpty();
        RuleFor(c => c.VoterIdentifier).NotEmpty();
        
    }
}