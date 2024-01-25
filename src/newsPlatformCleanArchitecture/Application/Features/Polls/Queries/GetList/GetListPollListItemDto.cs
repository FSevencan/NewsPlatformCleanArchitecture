using Core.Application.Dtos;

namespace Application.Features.Polls.Queries.GetList;

public class GetListPollListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}