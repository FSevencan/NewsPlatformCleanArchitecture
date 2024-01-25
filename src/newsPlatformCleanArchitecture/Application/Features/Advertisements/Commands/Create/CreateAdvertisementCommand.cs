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

namespace Application.Features.Advertisements.Commands.Create;

public class CreateAdvertisementCommand : IRequest<CreatedAdvertisementResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int ClickCount { get; set; }
    public int ViewCount { get; set; }

    public string[] Roles => new[] { Admin, Write, AdvertisementsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAdvertisements";

    public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, CreatedAdvertisementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly AdvertisementBusinessRules _advertisementBusinessRules;

        public CreateAdvertisementCommandHandler(IMapper mapper, IAdvertisementRepository advertisementRepository,
                                         AdvertisementBusinessRules advertisementBusinessRules)
        {
            _mapper = mapper;
            _advertisementRepository = advertisementRepository;
            _advertisementBusinessRules = advertisementBusinessRules;
        }

        public async Task<CreatedAdvertisementResponse> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            Advertisement advertisement = _mapper.Map<Advertisement>(request);

            await _advertisementRepository.AddAsync(advertisement);

            CreatedAdvertisementResponse response = _mapper.Map<CreatedAdvertisementResponse>(advertisement);
            return response;
        }
    }
}