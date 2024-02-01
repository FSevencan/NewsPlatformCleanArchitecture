using Core.Application.Dtos;

namespace Application.Features.NewsVideos.Queries.GetList;

public class GetListNewsVideoListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoURL { get; set; }
}