using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.PollVotes.Queries.GetList;

public class GetListPollVoteListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string VoterIdentifier { get; set; }
  
}