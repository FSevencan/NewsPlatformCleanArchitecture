using Application.Features.ColumnArticles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ColumnArticles.Constants.ColumnArticlesOperationClaims;

namespace Application.Features.ColumnArticles.Queries.GetList;

public class GetListColumnArticleQuery : IRequest<GetListResponse<GetListColumnArticleListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListColumnArticles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetColumnArticles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListColumnArticleQueryHandler : IRequestHandler<GetListColumnArticleQuery, GetListResponse<GetListColumnArticleListItemDto>>
    {
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly IMapper _mapper;

        public GetListColumnArticleQueryHandler(IColumnArticleRepository columnArticleRepository, IMapper mapper)
        {
            _columnArticleRepository = columnArticleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListColumnArticleListItemDto>> Handle(GetListColumnArticleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ColumnArticle> columnArticles = await _columnArticleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListColumnArticleListItemDto> response = _mapper.Map<GetListResponse<GetListColumnArticleListItemDto>>(columnArticles);
            return response;
        }
    }
}