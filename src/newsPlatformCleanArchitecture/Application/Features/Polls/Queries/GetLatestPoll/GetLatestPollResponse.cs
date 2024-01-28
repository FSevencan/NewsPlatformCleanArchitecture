using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Polls.Queries.GetLatestPoll;
public class GetLatestPollResponse : IResponse
{
    public Guid Id { get; set; }

}