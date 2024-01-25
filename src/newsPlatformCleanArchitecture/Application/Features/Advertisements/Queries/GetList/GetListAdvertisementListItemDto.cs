using Core.Application.Dtos;

namespace Application.Features.Advertisements.Queries.GetList;

public class GetListAdvertisementListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ClickCount { get; set; }
    public int ViewCount { get; set; }
}