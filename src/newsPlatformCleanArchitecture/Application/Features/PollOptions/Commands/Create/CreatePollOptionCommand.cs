using Application.Features.PollOptions.Constants;
using Application.Features.PollOptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.PollOptions.Constants.PollOptionsOperationClaims;

namespace Application.Features.PollOptions.Commands.Create;

public class CreatePollOptionCommand : IRequest<CreatedPollOptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid PollId { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
  

    public string[] Roles => new[] { Admin, Write, PollOptionsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollOptions";

    public class CreatePollOptionCommandHandler : IRequestHandler<CreatePollOptionCommand, CreatedPollOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly PollOptionBusinessRules _pollOptionBusinessRules;

        public CreatePollOptionCommandHandler(IMapper mapper, IPollOptionRepository pollOptionRepository,
                                         PollOptionBusinessRules pollOptionBusinessRules)
        {
            _mapper = mapper;
            _pollOptionRepository = pollOptionRepository;
            _pollOptionBusinessRules = pollOptionBusinessRules;
        }

        public async Task<CreatedPollOptionResponse> Handle(CreatePollOptionCommand request, CancellationToken cancellationToken)
        {
            PollOption pollOption = _mapper.Map<PollOption>(request);

            await _pollOptionRepository.AddAsync(pollOption);

            CreatedPollOptionResponse response = _mapper.Map<CreatedPollOptionResponse>(pollOption);
            return response;
        }
    }
}