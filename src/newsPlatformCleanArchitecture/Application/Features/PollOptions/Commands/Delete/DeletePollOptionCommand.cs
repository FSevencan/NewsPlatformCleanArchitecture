using Application.Features.PollOptions.Constants;
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

namespace Application.Features.PollOptions.Commands.Delete;

public class DeletePollOptionCommand : IRequest<DeletedPollOptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PollOptionsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollOptions";

    public class DeletePollOptionCommandHandler : IRequestHandler<DeletePollOptionCommand, DeletedPollOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly PollOptionBusinessRules _pollOptionBusinessRules;

        public DeletePollOptionCommandHandler(IMapper mapper, IPollOptionRepository pollOptionRepository,
                                         PollOptionBusinessRules pollOptionBusinessRules)
        {
            _mapper = mapper;
            _pollOptionRepository = pollOptionRepository;
            _pollOptionBusinessRules = pollOptionBusinessRules;
        }

        public async Task<DeletedPollOptionResponse> Handle(DeletePollOptionCommand request, CancellationToken cancellationToken)
        {
            PollOption? pollOption = await _pollOptionRepository.GetAsync(predicate: po => po.Id == request.Id, cancellationToken: cancellationToken);
            await _pollOptionBusinessRules.PollOptionShouldExistWhenSelected(pollOption);

            await _pollOptionRepository.DeleteAsync(pollOption!);

            DeletedPollOptionResponse response = _mapper.Map<DeletedPollOptionResponse>(pollOption);
            return response;
        }
    }
}