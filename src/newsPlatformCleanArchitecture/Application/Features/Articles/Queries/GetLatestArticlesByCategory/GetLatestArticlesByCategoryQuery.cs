using Application.Features.Articles.Queries.GetList;
using Application.Services.Articles;
using Application.Services.Repositories;
using Core.Application.Utilities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Articles.Queries.GetLatestArticlesByCategory;
public class GetLatestArticlesByCategoryQuery : IRequest<GetLatestArticlesByCategoryResponse>
{
    public string SubCategoryName { get; set; }
    public int MaxResult { get; set; }
}

public class GetLatestArticlesByCategoryQueryHandler : IRequestHandler<GetLatestArticlesByCategoryQuery, GetLatestArticlesByCategoryResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetLatestArticlesByCategoryQueryHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<GetLatestArticlesByCategoryResponse> Handle(GetLatestArticlesByCategoryQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetListAsync(
            include: s => s.Include(subCategory => subCategory.SubCategory),
            predicate: a => a.SubCategory.Name == request.SubCategoryName,
            orderBy: q => q.OrderByDescending(a => a.CreatedDate),
            size: request.MaxResult,
            cancellationToken: cancellationToken
        );

        var mappedArticles = _mapper.Map<IEnumerable<GetLastArticleByCategoryItemDto>>(articles.Items);

       
        foreach (var article in mappedArticles)
        {
            article.Slug = Slug.CreateSlug(article.Title); 
        }

        return new GetLatestArticlesByCategoryResponse { items = mappedArticles };
    }
}
