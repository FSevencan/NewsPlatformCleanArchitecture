using Application.Features.Articles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Articles.Constants.ArticlesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Queries.GetList;

public class GetListArticleQuery : IRequest<GetListResponse<GetListArticleListItemDto>>
{
    public PageRequest PageRequest { get; set; }


    public class GetListArticleQueryHandler : IRequestHandler<GetListArticleQuery, GetListResponse<GetListArticleListItemDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetListArticleQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListArticleListItemDto>> Handle(GetListArticleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Article> articles = await _articleRepository.GetListAsync(
                include: s=> s.Include(subCategory=> subCategory.SubCategory),
                orderBy: o=> o.OrderByDescending(o=> o.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListArticleListItemDto> response = _mapper.Map<GetListResponse<GetListArticleListItemDto>>(articles);
            return response;
        }
    }
}