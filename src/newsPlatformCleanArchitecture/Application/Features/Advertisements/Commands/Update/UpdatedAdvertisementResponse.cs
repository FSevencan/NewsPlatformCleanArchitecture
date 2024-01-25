using Core.Application.Responses;

namespace Application.Features.Advertisements.Commands.Update;

public class UpdatedAdvertisementResponse : IResponse
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