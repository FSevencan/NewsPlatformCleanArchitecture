using Core.Application.Responses;

namespace Application.Features.Subscribles.Commands.Update;

public class UpdatedSubscribleResponse : IResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
}