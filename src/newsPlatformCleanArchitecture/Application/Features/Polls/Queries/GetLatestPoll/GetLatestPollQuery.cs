using Application.Features.Polls.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Polls.Queries.GetLatestPoll;
public class GetLatestPollQuery : IRequest<GetLatestPollResponse>
{

    public class GetLatestPollQueryHandler : IRequestHandler<GetLatestPollQuery, GetLatestPollResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;
        private readonly PollBusinessRules _pollBusinessRules;

        public GetLatestPollQueryHandler(IMapper mapper, IPollRepository pollRepository, PollBusinessRules pollBusinessRules)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
            _pollBusinessRules = pollBusinessRules;
        }

        public async Task<GetLatestPollResponse> Handle(GetLatestPollQuery request, CancellationToken cancellationToken)
        {
            Poll? latestPoll = await _pollRepository.GetLatestPollAsync(cancellationToken);

            GetLatestPollResponse response = _mapper.Map<GetLatestPollResponse>(latestPoll);
            return response;
        }
    }
}