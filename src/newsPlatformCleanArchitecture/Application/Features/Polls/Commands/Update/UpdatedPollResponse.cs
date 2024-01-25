using Core.Application.Responses;

namespace Application.Features.Polls.Commands.Update;

public class UpdatedPollResponse : IResponse
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}