using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.PollVotes.Commands.Create;

public class CreatedPollVoteResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string VoterIdentifier { get; set; }
  
}