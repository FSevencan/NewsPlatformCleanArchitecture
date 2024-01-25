using FluentValidation;

namespace Application.Features.PollOptions.Commands.Create;

public class CreatePollOptionCommandValidator : AbstractValidator<CreatePollOptionCommand>
{
    public CreatePollOptionCommandValidator()
    {
        RuleFor(c => c.PollId).NotEmpty();
        RuleFor(c => c.OptionText).NotEmpty();
        RuleFor(c => c.VoteCount).NotEmpty();
       
    }
}