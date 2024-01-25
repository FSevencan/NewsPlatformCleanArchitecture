using Core.Application.Responses;

namespace Application.Features.PollOptions.Commands.Delete;

public class DeletedPollOptionResponse : IResponse
{
    public Guid Id { get; set; }
}