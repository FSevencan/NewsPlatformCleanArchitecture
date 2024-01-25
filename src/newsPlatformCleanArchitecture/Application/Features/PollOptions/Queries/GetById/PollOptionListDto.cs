using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PollOptions.Queries.GetById;
public class PollOptionListDto : IDto
{
    public Guid Id { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
}
