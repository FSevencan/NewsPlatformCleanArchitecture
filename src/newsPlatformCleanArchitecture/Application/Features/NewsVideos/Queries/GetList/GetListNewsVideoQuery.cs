using Application.Features.NewsVideos.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.NewsVideos.Constants.NewsVideosOperationClaims;

namespace Application.Features.NewsVideos.Queries.GetList;

public class GetListNewsVideoQuery : IRequest<GetListResponse<GetListNewsVideoListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListNewsVideos({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetNewsVideos";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListNewsVideoQueryHandler : IRequestHandler<GetListNewsVideoQuery, GetListResponse<GetListNewsVideoListItemDto>>
    {
        private readonly INewsVideoRepository _newsVideoRepository;
        private readonly IMapper _mapper;

        public GetListNewsVideoQueryHandler(INewsVideoRepository newsVideoRepository, IMapper mapper)
        {
            _newsVideoRepository = newsVideoRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListNewsVideoListItemDto>> Handle(GetListNewsVideoQuery request, CancellationToken cancellationToken)
        {
            IPaginate<NewsVideo> newsVideos = await _newsVideoRepository.GetListAsync(
                orderBy: v=> v.OrderByDescending(v=>v.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListNewsVideoListItemDto> response = _mapper.Map<GetListResponse<GetListNewsVideoListItemDto>>(newsVideos);
            return response;
        }
    }
}