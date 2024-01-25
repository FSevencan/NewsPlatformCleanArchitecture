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

namespace Application.Features.Subscribles.Commands.Update;

public class UpdateSubscribleCommand : IRequest<UpdatedSubscribleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }

    public string[] Roles => new[] { Admin, Write, SubscriblesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubscribles";

    public class UpdateSubscribleCommandHandler : IRequestHandler<UpdateSubscribleCommand, UpdatedSubscribleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscribleRepository _subscribleRepository;
        private readonly SubscribleBusinessRules _subscribleBusinessRules;

        public UpdateSubscribleCommandHandler(IMapper mapper, ISubscribleRepository subscribleRepository,
                                         SubscribleBusinessRules subscribleBusinessRules)
        {
            _mapper = mapper;
            _subscribleRepository = subscribleRepository;
            _subscribleBusinessRules = subscribleBusinessRules;
        }

        public async Task<UpdatedSubscribleResponse> Handle(UpdateSubscribleCommand request, CancellationToken cancellationToken)
        {
            Subscrible? subscrible = await _subscribleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _subscribleBusinessRules.SubscribleShouldExistWhenSelected(subscrible);
            subscrible = _mapper.Map(request, subscrible);

            await _subscribleRepository.UpdateAsync(subscrible!);

            UpdatedSubscribleResponse response = _mapper.Map<UpdatedSubscribleResponse>(subscrible);
            return response;
        }
    }
}