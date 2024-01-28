using Application.Features.Articles.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Articles.Queries.GetMixedLatestArticles;
public class GetLatestArticlesExcludingCategoriesQuery : IRequest<GetListResponse<GetListArticleListItemDto>>, ICachableRequest
{
    public int MaxResult { get; set; }
    public string[] ExcludeCategories { get; set; }

    public bool BypassCache { get; set; }
   
    public string CacheKey => $"GetMixedLatestArticles({MaxResult},[{string.Join(",", ExcludeCategories)}])";
    public string CacheGroupKey => "GetMixedArticles";
    public TimeSpan? SlidingExpiration { get; set; }
}

public class GetMixedLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesExcludingCategoriesQuery, GetListResponse<GetListArticleListItemDto>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetMixedLatestArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListArticleListItemDto>> Handle(GetLatestArticlesExcludingCategoriesQuery request, CancellationToken cancellationToken)
    {
        
        var articles = await _articleRepository.GetListAsync(
            include: s => s.Include(subCategory => subCategory.SubCategory),
            predicate: article => !request.ExcludeCategories.Contains(article.SubCategory.Name),
            size: request.MaxResult,
            orderBy: x => x.OrderByDescending(a => a.CreatedDate),
            cancellationToken: cancellationToken
        );

        GetListResponse<GetListArticleListItemDto> response = _mapper.Map<GetListResponse<GetListArticleListItemDto>>(articles);
        return response;
    }
}