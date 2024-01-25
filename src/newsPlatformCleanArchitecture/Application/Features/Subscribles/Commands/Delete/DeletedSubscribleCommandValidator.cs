using FluentValidation;

namespace Application.Features.Subscribles.Commands.Delete;

public class DeleteSubscribleCommandValidator : AbstractValidator<DeleteSubscribleCommand>
{
    public DeleteSubscribleCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}