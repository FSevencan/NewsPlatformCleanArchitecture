using Core.Application.Responses;

namespace Application.Features.Advertisements.Commands.Delete;

public class DeletedAdvertisementResponse : IResponse
{
    public Guid Id { get; set; }
}