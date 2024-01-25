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

namespace Application.Features.PollOptions.Commands.Update;

public class UpdatePollOptionCommand : IRequest<UpdatedPollOptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public string OptionText { get; set; }
    public int VoteCount { get; set; }
    public Poll Poll { get; set; }

    public string[] Roles => new[] { Admin, Write, PollOptionsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollOptions";

    public class UpdatePollOptionCommandHandler : IRequestHandler<UpdatePollOptionCommand, UpdatedPollOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly PollOptionBusinessRules _pollOptionBusinessRules;

        public UpdatePollOptionCommandHandler(IMapper mapper, IPollOptionRepository pollOptionRepository,
                                         PollOptionBusinessRules pollOptionBusinessRules)
        {
            _mapper = mapper;
            _pollOptionRepository = pollOptionRepository;
            _pollOptionBusinessRules = pollOptionBusinessRules;
        }

        public async Task<UpdatedPollOptionResponse> Handle(UpdatePollOptionCommand request, CancellationToken cancellationToken)
        {
            PollOption? pollOption = await _pollOptionRepository.GetAsync(predicate: po => po.Id == request.Id, cancellationToken: cancellationToken);
            await _pollOptionBusinessRules.PollOptionShouldExistWhenSelected(pollOption);
            pollOption = _mapper.Map(request, pollOption);

            await _pollOptionRepository.UpdateAsync(pollOption!);

            UpdatedPollOptionResponse response = _mapper.Map<UpdatedPollOptionResponse>(pollOption);
            return response;
        }
    }
}