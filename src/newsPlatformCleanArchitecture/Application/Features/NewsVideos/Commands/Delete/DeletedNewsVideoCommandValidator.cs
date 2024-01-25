using FluentValidation;

namespace Application.Features.NewsVideos.Commands.Delete;

public class DeleteNewsVideoCommandValidator : AbstractValidator<DeleteNewsVideoCommand>
{
    public DeleteNewsVideoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}