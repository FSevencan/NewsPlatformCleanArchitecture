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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetCategoryTree;
public class GetCategoryTreeQuery : IRequest<GetListResponse<GetCategoryDto>>
{
    public PageRequest PageRequest { get; set; }
}

public class GetCategoryTreeQueryHandler : IRequestHandler<GetCategoryTreeQuery, GetListResponse<GetCategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryTreeQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetCategoryDto>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
    {
        IPaginate<Category> categories = await _categoryRepository.GetListAsync(
            include: c => c.Include(sc => sc.SubCategories),
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            cancellationToken: cancellationToken
        );

        GetListResponse<GetCategoryDto> response = _mapper.Map<GetListResponse<GetCategoryDto>>(categories);
        return response;

    }
}