using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.PollOptions.Queries.GetList;

public class GetListPollOptionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
    
}