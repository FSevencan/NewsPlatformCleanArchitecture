using Application.Features.SubCategories.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.SubCategories.Constants.SubCategoriesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SubCategories.Queries.GetList;

public class GetListSubCategoryQuery : IRequest<GetListResponse<GetListSubCategoryListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListSubCategories({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetSubCategories";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSubCategoryQueryHandler : IRequestHandler<GetListSubCategoryQuery, GetListResponse<GetListSubCategoryListItemDto>>
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;

        public GetListSubCategoryQueryHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSubCategoryListItemDto>> Handle(GetListSubCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SubCategory> subCategories = await _subCategoryRepository.GetListAsync(
                include: c=> c.Include(category=> category.Category),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSubCategoryListItemDto> response = _mapper.Map<GetListResponse<GetListSubCategoryListItemDto>>(subCategories);
            return response;
        }
    }
}