using Application.Features.Subscribles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Subscribles.Constants.SubscriblesOperationClaims;

namespace Application.Features.Subscribles.Queries.GetList;

public class GetListSubscribleQuery : IRequest<GetListResponse<GetListSubscribleListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListSubscribles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetSubscribles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSubscribleQueryHandler : IRequestHandler<GetListSubscribleQuery, GetListResponse<GetListSubscribleListItemDto>>
    {
        private readonly ISubscribleRepository _subscribleRepository;
        private readonly IMapper _mapper;

        public GetListSubscribleQueryHandler(ISubscribleRepository subscribleRepository, IMapper mapper)
        {
            _subscribleRepository = subscribleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubscribleListItemDto>> Handle(GetListSubscribleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Subscrible> subscribles = await _subscribleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubscribleListItemDto> response = _mapper.Map<GetListResponse<GetListSubscribleListItemDto>>(subscribles);
            return response;
        }
    }
}