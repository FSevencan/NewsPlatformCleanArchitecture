using Core.Application.Responses;

namespace Application.Features.NewsVideos.Queries.GetById;

public class GetByIdNewsVideoResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoURL { get; set; }
}