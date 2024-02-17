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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Application.Features.ColumnArticles.Queries.GetList;

public class GetListColumnArticleQuery : IRequest<GetListResponse<GetListColumnArticleListItemDto>> 
{
    public PageRequest PageRequest { get; set; }
    public Guid? CategoryId { get; set; }

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
            // Filtreleme fonksiyonu oluþturuyoruz. CategoryId null ise tüm sonuçlar dönecek þekilde ayarlandý.
            Expression<Func<ColumnArticle, bool>>? filter = request.CategoryId.HasValue
                ? a => a.CategoryId == request.CategoryId
                : null;

            // Filtreleme fonksiyonunu GetListAsync metoduna geçirdik
            IPaginate<ColumnArticle> columnArticles = await _columnArticleRepository.GetListAsync(
                predicate: filter,
                include: c=> c.Include(c=> c.Columnist).Include(c=> c.Category),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            
            GetListResponse<GetListColumnArticleListItemDto> response = _mapper.Map<GetListResponse<GetListColumnArticleListItemDto>>(columnArticles);
            return response;
        }
    }
}