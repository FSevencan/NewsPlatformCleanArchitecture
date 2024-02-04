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

namespace Application.Features.Articles.Queries.GetMostLikedArticles;
public class GetMostLikedArticlesQuery : IRequest<GetListResponse<GetMostLikedArticleListDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => $"GetMostLikedArticles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMostLikedArticles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetMostLikedArticlesQueryHandler : IRequestHandler<GetMostLikedArticlesQuery, GetListResponse<GetMostLikedArticleListDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetMostLikedArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetMostLikedArticleListDto>> Handle(GetMostLikedArticlesQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Article> articles = await _articleRepository.GetListAsync(
                include: s => s.Include(subCategory => subCategory.SubCategory),
                orderBy: o => o.OrderByDescending(o => o.TotalLikes),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetMostLikedArticleListDto> response = _mapper.Map<GetListResponse<GetMostLikedArticleListDto>>(articles);
            return response;
        }
    }
}