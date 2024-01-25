using Application.Features.SubCategories.Constants;
using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SubCategories.Constants.SubCategoriesOperationClaims;

namespace Application.Features.SubCategories.Queries.GetById;

public class GetByIdSubCategoryQuery : IRequest<GetByIdSubCategoryResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdSubCategoryQueryHandler : IRequestHandler<GetByIdSubCategoryQuery, GetByIdSubCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

        public GetByIdSubCategoryQueryHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository, SubCategoryBusinessRules subCategoryBusinessRules)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryBusinessRules = subCategoryBusinessRules;
        }

        public async Task<GetByIdSubCategoryResponse> Handle(GetByIdSubCategoryQuery request, CancellationToken cancellationToken)
        {
            SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _subCategoryBusinessRules.SubCategoryShouldExistWhenSelected(subCategory);

            GetByIdSubCategoryResponse response = _mapper.Map<GetByIdSubCategoryResponse>(subCategory);
            return response;
        }
    }
}