using Core.Application.Responses;

namespace Application.Features.Subscribles.Commands.Create;

public class CreatedSubscribleResponse : IResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
}