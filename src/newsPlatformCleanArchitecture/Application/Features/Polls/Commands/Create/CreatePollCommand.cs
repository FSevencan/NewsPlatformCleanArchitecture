using Application.Features.Polls.Constants;
using Application.Features.Polls.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Polls.Constants.PollsOperationClaims;

namespace Application.Features.Polls.Commands.Create;

public class CreatePollCommand : IRequest<CreatedPollResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string[] Roles => new[] { Admin, Write, PollsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPolls";

    public class CreatePollCommandHandler : IRequestHandler<CreatePollCommand, CreatedPollResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;
        private readonly PollBusinessRules _pollBusinessRules;

        public CreatePollCommandHandler(IMapper mapper, IPollRepository pollRepository,
                                         PollBusinessRules pollBusinessRules)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
            _pollBusinessRules = pollBusinessRules;
        }

        public async Task<CreatedPollResponse> Handle(CreatePollCommand request, CancellationToken cancellationToken)
        {
            Poll poll = _mapper.Map<Poll>(request);

            await _pollRepository.AddAsync(poll);

            CreatedPollResponse response = _mapper.Map<CreatedPollResponse>(poll);
            return response;
        }
    }
}