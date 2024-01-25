using Core.Application.Responses;

namespace Application.Features.PollVotes.Commands.Delete;

public class DeletedPollVoteResponse : IResponse
{
    public Guid Id { get; set; }
}