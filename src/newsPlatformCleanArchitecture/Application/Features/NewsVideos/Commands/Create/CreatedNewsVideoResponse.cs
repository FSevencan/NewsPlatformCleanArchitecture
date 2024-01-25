using Core.Application.Responses;

namespace Application.Features.NewsVideos.Commands.Create;

public class CreatedNewsVideoResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string VideoURL { get; set; }
}