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

public class GetArticlesByCategoryQuery : IRequest<List<GetArticleByCategoryListDto>>
{
    public string CategoryName { get; set; }
}
public class GetArticlesByCategoryQueryHandler : IRequestHandler<GetArticlesByCategoryQuery, List<GetArticleByCategoryListDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetArticlesByCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetArticleByCategoryListDto>> Handle(GetArticlesByCategoryQuery request, CancellationToken cancellationToken)
    {
        
        var categoryNameNormalized = request.CategoryName.Replace("-", " ").ToLower();

        IPaginate<Category> categories = await _categoryRepository.GetListAsync(
            predicate: c => c.Name.ToLower() == categoryNameNormalized, // Küçük harfe çevirerek karşılaştır
            include: c => c.Include(sc => sc.SubCategories).ThenInclude(a => a.Articles),
            cancellationToken: cancellationToken
        );

        // SubCategories altındaki Articles'ları liste haline getir.
        var articles = categories.Items
            .SelectMany(c => c.SubCategories)
            .SelectMany(sc => sc.Articles)
            .ToList();

        List<GetArticleByCategoryListDto> response = _mapper.Map<List<GetArticleByCategoryListDto>>(articles);
        return response;
    }
}