using Core.Application.Responses;

namespace Application.Features.Subscribles.Commands.Delete;

public class DeletedSubscribleResponse : IResponse
{
    public Guid Id { get; set; }
}