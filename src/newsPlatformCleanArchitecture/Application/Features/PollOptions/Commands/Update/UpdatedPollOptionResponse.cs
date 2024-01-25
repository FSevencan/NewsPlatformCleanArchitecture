using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.PollOptions.Commands.Update;

public class UpdatedPollOptionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
   
}