using Application.Features.SubCategories.Constants;
using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SubCategories.Constants.SubCategoriesOperationClaims;

namespace Application.Features.SubCategories.Commands.Update;

public class UpdateSubCategoryCommand : IRequest<UpdatedSubCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
   

    public string[] Roles => new[] { Admin, Write, SubCategoriesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubCategories";

    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, UpdatedSubCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

        public UpdateSubCategoryCommandHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository,
                                         SubCategoryBusinessRules subCategoryBusinessRules)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryBusinessRules = subCategoryBusinessRules;
        }

        public async Task<UpdatedSubCategoryResponse> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _subCategoryBusinessRules.SubCategoryShouldExistWhenSelected(subCategory);
            subCategory = _mapper.Map(request, subCategory);

            await _subCategoryRepository.UpdateAsync(subCategory!);

            UpdatedSubCategoryResponse response = _mapper.Map<UpdatedSubCategoryResponse>(subCategory);
            return response;
        }
    }
}