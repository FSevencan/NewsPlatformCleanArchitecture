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

namespace Application.Features.Advertisements.Commands.Update;

public class UpdateAdvertisementCommand : IRequest<UpdatedAdvertisementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ClickCount { get; set; }
    public int ViewCount { get; set; }

    public string[] Roles => new[] { Admin, Write, AdvertisementsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAdvertisements";

    public class UpdateAdvertisementCommandHandler : IRequestHandler<UpdateAdvertisementCommand, UpdatedAdvertisementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly AdvertisementBusinessRules _advertisementBusinessRules;

        public UpdateAdvertisementCommandHandler(IMapper mapper, IAdvertisementRepository advertisementRepository,
                                         AdvertisementBusinessRules advertisementBusinessRules)
        {
            _mapper = mapper;
            _advertisementRepository = advertisementRepository;
            _advertisementBusinessRules = advertisementBusinessRules;
        }

        public async Task<UpdatedAdvertisementResponse> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _advertisementBusinessRules.AdvertisementShouldExistWhenSelected(advertisement);
            advertisement = _mapper.Map(request, advertisement);

            await _advertisementRepository.UpdateAsync(advertisement!);

            UpdatedAdvertisementResponse response = _mapper.Map<UpdatedAdvertisementResponse>(advertisement);
            return response;
        }
    }
}