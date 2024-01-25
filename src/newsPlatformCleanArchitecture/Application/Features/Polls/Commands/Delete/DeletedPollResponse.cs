using Core.Application.Responses;

namespace Application.Features.Polls.Commands.Delete;

public class DeletedPollResponse : IResponse
{
    public Guid Id { get; set; }
}