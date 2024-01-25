using Application.Features.Advertisements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Advertisements.Constants.AdvertisementsOperationClaims;

namespace Application.Features.Advertisements.Queries.GetList;

public class GetListAdvertisementQuery : IRequest<GetListResponse<GetListAdvertisementListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAdvertisements({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAdvertisements";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAdvertisementQueryHandler : IRequestHandler<GetListAdvertisementQuery, GetListResponse<GetListAdvertisementListItemDto>>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;

        public GetListAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAdvertisementListItemDto>> Handle(GetListAdvertisementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Advertisement> advertisements = await _advertisementRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAdvertisementListItemDto> response = _mapper.Map<GetListResponse<GetListAdvertisementListItemDto>>(advertisements);
            return response;
        }
    }
}