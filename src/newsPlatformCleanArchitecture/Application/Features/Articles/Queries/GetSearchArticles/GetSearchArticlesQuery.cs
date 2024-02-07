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

namespace Application.Features.Articles.Queries.GetSearchArticles;
public class GetSearchArticlesQuery : IRequest<GetListResponse<GetSearchArticleListDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string SearchTerm { get; set; }

    public bool BypassCache => false;
    public string CacheKey => $"GetSearchArticles-{SearchTerm}-{PageRequest.PageIndex}-{PageRequest.PageSize}";
    public string CacheGroupKey => "GetArticles";
    public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(5);

    public class GetListArticleQueryHandler : IRequestHandler<GetSearchArticlesQuery, GetListResponse<GetSearchArticleListDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetListArticleQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetSearchArticleListDto>> Handle(GetSearchArticlesQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Article> articles = await _articleRepository.GetListAsync(
                predicate: a => a.Title.Contains(request.SearchTerm) ||
                                a.SubCategory.Name.Contains(request.SearchTerm) ||
                                a.ArticleTags.Any(tag => tag.Tag.Name.Contains(request.SearchTerm)),
                include: s => s.Include(subCategory => subCategory.SubCategory)
                               .Include(a => a.ArticleTags)
                               .ThenInclude(at => at.Tag),
                orderBy: o => o.OrderByDescending(o => o.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetSearchArticleListDto> response = _mapper.Map<GetListResponse<GetSearchArticleListDto>>(articles);
            return response;
        }
    }
}