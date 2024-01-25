using Core.Application.Responses;

namespace Application.Features.Polls.Commands.Create;

public class CreatedPollResponse : IResponse
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}