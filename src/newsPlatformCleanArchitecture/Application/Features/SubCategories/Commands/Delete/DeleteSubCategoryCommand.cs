using Application.Features.SubCategories.Constants;
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

namespace Application.Features.SubCategories.Commands.Delete;

public class DeleteSubCategoryCommand : IRequest<DeletedSubCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, SubCategoriesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubCategories";

    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, DeletedSubCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

        public DeleteSubCategoryCommandHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository,
                                         SubCategoryBusinessRules subCategoryBusinessRules)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryBusinessRules = subCategoryBusinessRules;
        }

        public async Task<DeletedSubCategoryResponse> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _subCategoryBusinessRules.SubCategoryShouldExistWhenSelected(subCategory);

            await _subCategoryRepository.DeleteAsync(subCategory!);

            DeletedSubCategoryResponse response = _mapper.Map<DeletedSubCategoryResponse>(subCategory);
            return response;
        }
    }
}