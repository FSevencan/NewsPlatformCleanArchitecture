using Application.Features.Advertisements.Constants;
using Application.Features.Advertisements.Constants;
using Application.Features.Advertisements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Advertisements.Constants.AdvertisementsOperationClaims;

namespace Application.Features.Advertisements.Commands.Delete;

public class DeleteAdvertisementCommand : IRequest<DeletedAdvertisementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AdvertisementsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAdvertisements";

    public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommand, DeletedAdvertisementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly AdvertisementBusinessRules _advertisementBusinessRules;

        public DeleteAdvertisementCommandHandler(IMapper mapper, IAdvertisementRepository advertisementRepository,
                                         AdvertisementBusinessRules advertisementBusinessRules)
        {
            _mapper = mapper;
            _advertisementRepository = advertisementRepository;
            _advertisementBusinessRules = advertisementBusinessRules;
        }

        public async Task<DeletedAdvertisementResponse> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _advertisementBusinessRules.AdvertisementShouldExistWhenSelected(advertisement);

            await _advertisementRepository.DeleteAsync(advertisement!);

            DeletedAdvertisementResponse response = _mapper.Map<DeletedAdvertisementResponse>(advertisement);
            return response;
        }
    }
}