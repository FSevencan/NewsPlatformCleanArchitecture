using FluentValidation;

namespace Application.Features.Advertisements.Commands.Delete;

public class DeleteAdvertisementCommandValidator : AbstractValidator<DeleteAdvertisementCommand>
{
    public DeleteAdvertisementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}