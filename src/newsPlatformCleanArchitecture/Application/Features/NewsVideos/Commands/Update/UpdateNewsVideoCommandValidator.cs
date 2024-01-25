using FluentValidation;

namespace Application.Features.NewsVideos.Commands.Update;

public class UpdateNewsVideoCommandValidator : AbstractValidator<UpdateNewsVideoCommand>
{
    public UpdateNewsVideoCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.VideoURL).NotEmpty();
    }
}