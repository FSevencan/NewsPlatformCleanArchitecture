using Application.Features.ArticleTags.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ArticleTags.Constants.ArticleTagsOperationClaims;

namespace Application.Features.ArticleTags.Queries.GetList;

public class GetListArticleTagQuery : IRequest<GetListResponse<GetListArticleTagListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListArticleTags({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetArticleTags";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListArticleTagQueryHandler : IRequestHandler<GetListArticleTagQuery, GetListResponse<GetListArticleTagListItemDto>>
    {
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly IMapper _mapper;

        public GetListArticleTagQueryHandler(IArticleTagRepository articleTagRepository, IMapper mapper)
        {
            _articleTagRepository = articleTagRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListArticleTagListItemDto>> Handle(GetListArticleTagQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ArticleTag> articleTags = await _articleTagRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListArticleTagListItemDto> response = _mapper.Map<GetListResponse<GetListArticleTagListItemDto>>(articleTags);
            return response;
        }
    }
}