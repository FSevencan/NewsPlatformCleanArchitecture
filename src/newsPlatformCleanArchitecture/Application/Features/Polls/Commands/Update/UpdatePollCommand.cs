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

namespace Application.Features.Polls.Commands.Update;

public class UpdatePollCommand : IRequest<UpdatedPollResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public string[] Roles => new[] { Admin, Write, PollsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPolls";

    public class UpdatePollCommandHandler : IRequestHandler<UpdatePollCommand, UpdatedPollResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;
        private readonly PollBusinessRules _pollBusinessRules;

        public UpdatePollCommandHandler(IMapper mapper, IPollRepository pollRepository,
                                         PollBusinessRules pollBusinessRules)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
            _pollBusinessRules = pollBusinessRules;
        }

        public async Task<UpdatedPollResponse> Handle(UpdatePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await _pollRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _pollBusinessRules.PollShouldExistWhenSelected(poll);
            poll = _mapper.Map(request, poll);

            await _pollRepository.UpdateAsync(poll!);

            UpdatedPollResponse response = _mapper.Map<UpdatedPollResponse>(poll);
            return response;
        }
    }
}