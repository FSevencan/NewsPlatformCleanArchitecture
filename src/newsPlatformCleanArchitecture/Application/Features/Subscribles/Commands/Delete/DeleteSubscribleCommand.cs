using Application.Features.Subscribles.Constants;
using Application.Features.Subscribles.Constants;
using Application.Features.Subscribles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Subscribles.Constants.SubscriblesOperationClaims;

namespace Application.Features.Subscribles.Commands.Delete;

public class DeleteSubscribleCommand : IRequest<DeletedSubscribleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, SubscriblesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubscribles";

    public class DeleteSubscribleCommandHandler : IRequestHandler<DeleteSubscribleCommand, DeletedSubscribleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscribleRepository _subscribleRepository;
        private readonly SubscribleBusinessRules _subscribleBusinessRules;

        public DeleteSubscribleCommandHandler(IMapper mapper, ISubscribleRepository subscribleRepository,
                                         SubscribleBusinessRules subscribleBusinessRules)
        {
            _mapper = mapper;
            _subscribleRepository = subscribleRepository;
            _subscribleBusinessRules = subscribleBusinessRules;
        }

        public async Task<DeletedSubscribleResponse> Handle(DeleteSubscribleCommand request, CancellationToken cancellationToken)
        {
            Subscrible? subscrible = await _subscribleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _subscribleBusinessRules.SubscribleShouldExistWhenSelected(subscrible);

            await _subscribleRepository.DeleteAsync(subscrible!);

            DeletedSubscribleResponse response = _mapper.Map<DeletedSubscribleResponse>(subscrible);
            return response;
        }
    }
}