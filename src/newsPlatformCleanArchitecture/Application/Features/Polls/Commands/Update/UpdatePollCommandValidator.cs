using FluentValidation;

namespace Application.Features.Polls.Commands.Update;

public class UpdatePollCommandValidator : AbstractValidator<UpdatePollCommand>
{
    public UpdatePollCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Question).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
    }
}