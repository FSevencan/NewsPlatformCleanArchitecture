using FluentValidation;

namespace Application.Features.NewsVideos.Commands.Create;

public class CreateNewsVideoCommandValidator : AbstractValidator<CreateNewsVideoCommand>
{
    public CreateNewsVideoCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.VideoURL).NotEmpty();
    }
}