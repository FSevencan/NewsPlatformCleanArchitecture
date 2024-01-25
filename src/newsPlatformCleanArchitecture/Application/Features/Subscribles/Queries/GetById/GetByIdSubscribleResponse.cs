using Core.Application.Responses;

namespace Application.Features.Subscribles.Queries.GetById;

public class GetByIdSubscribleResponse : IResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
}