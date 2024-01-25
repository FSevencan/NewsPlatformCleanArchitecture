using Application.Features.Articles.Constants;
using Application.Features.Articles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Articles.Constants.ArticlesOperationClaims;
using Microsoft.EntityFrameworkCore;
using Core.Application.Utilities;

namespace Application.Features.Articles.Queries.GetById;

public class GetArticleBySlugQuery : IRequest<GetArticleBySlugResponse>
{
    public string Slug { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, GetArticleBySlugResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleBusinessRules _articleBusinessRules;

        public GetArticleBySlugQueryHandler(IMapper mapper, IArticleRepository articleRepository, ArticleBusinessRules articleBusinessRules)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<GetArticleBySlugResponse> Handle(GetArticleBySlugQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetAsync(
                predicate: a => a.Slug == request.Slug,
                include: a => a.Include(subCategory => subCategory.SubCategory)
                .Include(t=> t.ArticleTags).ThenInclude(tags=> tags.Tag),
                cancellationToken: cancellationToken
            );

            await _articleBusinessRules.ArticleShouldExistWhenSelected(article);

            GetArticleBySlugResponse response = _mapper.Map<GetArticleBySlugResponse>(article);
            return response;
        }
    }
}