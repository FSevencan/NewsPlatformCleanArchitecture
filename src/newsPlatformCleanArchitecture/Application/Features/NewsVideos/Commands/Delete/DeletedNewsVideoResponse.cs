using Core.Application.Responses;

namespace Application.Features.NewsVideos.Commands.Delete;

public class DeletedNewsVideoResponse : IResponse
{
    public Guid Id { get; set; }
}