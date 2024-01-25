using Core.Application.Dtos;

namespace Application.Features.Subscribles.Queries.GetList;

public class GetListSubscribleListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
}