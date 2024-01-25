using Core.Application.Responses;

namespace Application.Features.Polls.Queries.GetById;

public class GetByIdPollResponse : IResponse
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}