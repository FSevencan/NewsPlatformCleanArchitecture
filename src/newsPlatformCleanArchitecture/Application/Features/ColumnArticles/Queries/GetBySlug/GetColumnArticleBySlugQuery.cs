using Application.Features.Articles.Queries.GetById;
using Application.Features.Articles.Rules;
using Application.Features.ColumnArticles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Utilities;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ColumnArticles.Queries.GetBySlug;
public class GetColumnArticleBySlugQuery : IRequest<GetColumnArticleBySlugResponse>
{
    public string Slug { get; set; }

    public class GetArticleBySlugQueryHandler : IRequestHandler<GetColumnArticleBySlugQuery, GetColumnArticleBySlugResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

        public GetArticleBySlugQueryHandler(IMapper mapper, IColumnArticleRepository columnArticleRepository, ColumnArticleBusinessRules columnArticleBusinessRules)
        {
            _columnArticleRepository = columnArticleRepository;
            _mapper = mapper;
            _columnArticleBusinessRules = columnArticleBusinessRules;
        }

        public async Task<GetColumnArticleBySlugResponse> Handle(GetColumnArticleBySlugQuery request, CancellationToken cancellationToken)
        {
            
            var columnArticle = await _columnArticleRepository.GetAsync(
                predicate: a => a.Title.ToLower().Replace(" ", "-") == request.Slug,
                include: a => a.Include(Category => Category.Category),
                cancellationToken: cancellationToken
            );

            await _columnArticleBusinessRules.ColumnArticleShouldExistWhenSelected(columnArticle);

            GetColumnArticleBySlugResponse response = _mapper.Map<GetColumnArticleBySlugResponse>(columnArticle);
            return response;
        }
    }
}