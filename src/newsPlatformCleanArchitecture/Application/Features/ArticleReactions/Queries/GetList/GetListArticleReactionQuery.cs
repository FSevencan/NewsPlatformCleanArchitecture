using Application.Features.ArticleReactions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ArticleReactions.Constants.ArticleReactionsOperationClaims;

namespace Application.Features.ArticleReactions.Queries.GetList;

public class GetListArticleReactionQuery : IRequest<GetListResponse<GetListArticleReactionListItemDto>>/*, ISecuredRequest*/, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListArticleReactions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetArticleReactions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListArticleReactionQueryHandler : IRequestHandler<GetListArticleReactionQuery, GetListResponse<GetListArticleReactionListItemDto>>
    {
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly IMapper _mapper;

        public GetListArticleReactionQueryHandler(IArticleReactionRepository articleReactionRepository, IMapper mapper)
        {
            _articleReactionRepository = articleReactionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListArticleReactionListItemDto>> Handle(GetListArticleReactionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ArticleReaction> articleReactions = await _articleReactionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListArticleReactionListItemDto> response = _mapper.Map<GetListResponse<GetListArticleReactionListItemDto>>(articleReactions);
            return response;
        }
    }
}