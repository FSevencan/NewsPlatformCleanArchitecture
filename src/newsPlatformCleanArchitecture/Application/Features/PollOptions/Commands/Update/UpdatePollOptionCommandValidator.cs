using FluentValidation;

namespace Application.Features.PollOptions.Commands.Update;

public class UpdatePollOptionCommandValidator : AbstractValidator<UpdatePollOptionCommand>
{
    public UpdatePollOptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PollId).NotEmpty();
        RuleFor(c => c.OptionText).NotEmpty();
        RuleFor(c => c.VoteCount).NotEmpty();
      ;
    }
}