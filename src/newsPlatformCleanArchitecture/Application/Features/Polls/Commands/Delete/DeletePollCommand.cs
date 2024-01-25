using Application.Features.Polls.Constants;
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

namespace Application.Features.Polls.Commands.Delete;

public class DeletePollCommand : IRequest<DeletedPollResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PollsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPolls";

    public class DeletePollCommandHandler : IRequestHandler<DeletePollCommand, DeletedPollResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollRepository _pollRepository;
        private readonly PollBusinessRules _pollBusinessRules;

        public DeletePollCommandHandler(IMapper mapper, IPollRepository pollRepository,
                                         PollBusinessRules pollBusinessRules)
        {
            _mapper = mapper;
            _pollRepository = pollRepository;
            _pollBusinessRules = pollBusinessRules;
        }

        public async Task<DeletedPollResponse> Handle(DeletePollCommand request, CancellationToken cancellationToken)
        {
            Poll? poll = await _pollRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _pollBusinessRules.PollShouldExistWhenSelected(poll);

            await _pollRepository.DeleteAsync(poll!);

            DeletedPollResponse response = _mapper.Map<DeletedPollResponse>(poll);
            return response;
        }
    }
}