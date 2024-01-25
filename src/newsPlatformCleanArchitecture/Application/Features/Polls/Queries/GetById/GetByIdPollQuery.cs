using Application.Features.Polls.Constants;
using Application.Features.Polls.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Polls.Constants.PollsOperationClaims;

namespace Application.Features.Polls.Queries.GetById;

public class GetByIdPollQuery : IRequest<GetByIdPollResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPollQueryHandler : IRequestHandler<GetByIdPollQuery, GetByIdPollResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;
        private readonly PollBusinessRules _pollBusinessRules;

        public GetByIdPollQueryHandler(IMapper mapper, IPollRepository pollRepository, PollBusinessRules pollBusinessRules)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
            _pollBusinessRules = pollBusinessRules;
        }

        public async Task<GetByIdPollResponse> Handle(GetByIdPollQuery request, CancellationToken cancellationToken)
        {
            Poll? poll = await _pollRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _pollBusinessRules.PollShouldExistWhenSelected(poll);

            GetByIdPollResponse response = _mapper.Map<GetByIdPollResponse>(poll);
            return response;
        }
    }
}