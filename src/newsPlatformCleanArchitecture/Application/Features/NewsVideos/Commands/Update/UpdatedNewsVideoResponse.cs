using Core.Application.Responses;

namespace Application.Features.NewsVideos.Commands.Update;

public class UpdatedNewsVideoResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string VideoURL { get; set; }
}