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

namespace Application.Features.Subscribles.Commands.Create;

public class CreateSubscribleCommand : IRequest<CreatedSubscribleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }

    public string[] Roles => new[] { Admin, Write, SubscriblesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubscribles";

    public class CreateSubscribleCommandHandler : IRequestHandler<CreateSubscribleCommand, CreatedSubscribleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscribleRepository _subscribleRepository;
        private readonly SubscribleBusinessRules _subscribleBusinessRules;

        public CreateSubscribleCommandHandler(IMapper mapper, ISubscribleRepository subscribleRepository,
                                         SubscribleBusinessRules subscribleBusinessRules)
        {
            _mapper = mapper;
            _subscribleRepository = subscribleRepository;
            _subscribleBusinessRules = subscribleBusinessRules;
        }

        public async Task<CreatedSubscribleResponse> Handle(CreateSubscribleCommand request, CancellationToken cancellationToken)
        {
            Subscrible subscrible = _mapper.Map<Subscrible>(request);

            await _subscribleRepository.AddAsync(subscrible);

            CreatedSubscribleResponse response = _mapper.Map<CreatedSubscribleResponse>(subscrible);
            return response;
        }
    }
}