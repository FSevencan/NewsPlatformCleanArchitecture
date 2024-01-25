using FluentValidation;

namespace Application.Features.PollVotes.Commands.Update;

public class UpdatePollVoteCommandValidator : AbstractValidator<UpdatePollVoteCommand>
{
    public UpdatePollVoteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PollId).NotEmpty();
        RuleFor(c => c.PollOptionId).NotEmpty();
        RuleFor(c => c.VoterIdentifier).NotEmpty();
        
    }
}