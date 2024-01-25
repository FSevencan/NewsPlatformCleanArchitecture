using Application.Features.Advertisements.Constants;
using Application.Features.Advertisements.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Advertisements.Constants.AdvertisementsOperationClaims;

namespace Application.Features.Advertisements.Queries.GetById;

public class GetByIdAdvertisementQuery : IRequest<GetByIdAdvertisementResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAdvertisementQueryHandler : IRequestHandler<GetByIdAdvertisementQuery, GetByIdAdvertisementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly AdvertisementBusinessRules _advertisementBusinessRules;

        public GetByIdAdvertisementQueryHandler(IMapper mapper, IAdvertisementRepository advertisementRepository, AdvertisementBusinessRules advertisementBusinessRules)
        {
            _mapper = mapper;
            _advertisementRepository = advertisementRepository;
            _advertisementBusinessRules = advertisementBusinessRules;
        }

        public async Task<GetByIdAdvertisementResponse> Handle(GetByIdAdvertisementQuery request, CancellationToken cancellationToken)
        {
            Advertisement? advertisement = await _advertisementRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _advertisementBusinessRules.AdvertisementShouldExistWhenSelected(advertisement);

            GetByIdAdvertisementResponse response = _mapper.Map<GetByIdAdvertisementResponse>(advertisement);
            return response;
        }
    }
}