using Application.Features.Articles.Queries.GetList;
using Application.Features.Categories.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetArticlesByCategory;

public class GetArticlesByCategoryQuery : IRequest<GetListResponse<GetArticleByCategoryListDto>>
{
    public PageRequest PageRequest { get; set; }
    public string CategoryName { get; set; } 

  
    public class GetArticlesByCategoryQueryHandler : IRequestHandler<GetArticlesByCategoryQuery, GetListResponse<GetArticleByCategoryListDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesByCategoryQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetArticleByCategoryListDto>> Handle(GetArticlesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryNameNormalized = request.CategoryName.Replace("-", " ").ToLower();

            IPaginate<Article> articles = await _articleRepository.GetListAsync(
                predicate: a => a.SubCategory.Category.Name.ToLower() == categoryNameNormalized, 
                include: s => s.Include(subCategory => subCategory.SubCategory),
                orderBy: o => o.OrderByDescending(o => o.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetArticleByCategoryListDto> response = _mapper.Map<GetListResponse<GetArticleByCategoryListDto>>(articles);
            return response;
        }
    }
}
