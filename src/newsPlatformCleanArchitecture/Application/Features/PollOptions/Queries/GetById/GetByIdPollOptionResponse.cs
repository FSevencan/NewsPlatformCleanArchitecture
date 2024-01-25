using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.PollOptions.Queries.GetById;

public class GetByIdPollOptionResponse : IResponse
{
 
    public ICollection<PollOptionListDto> PollOptions { get; set; }
   
}