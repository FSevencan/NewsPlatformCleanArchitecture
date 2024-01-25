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

namespace Application.Features.SubCategories.Commands.Create;

public class CreateSubCategoryCommand : IRequest<CreatedSubCategoryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }

    public string[] Roles => new[] { Admin, Write, SubCategoriesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetSubCategories";

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, CreatedSubCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

        public CreateSubCategoryCommandHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository,
                                         SubCategoryBusinessRules subCategoryBusinessRules)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryBusinessRules = subCategoryBusinessRules;
        }

        public async Task<CreatedSubCategoryResponse> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            SubCategory subCategory = _mapper.Map<SubCategory>(request);

            await _subCategoryRepository.AddAsync(subCategory);

            CreatedSubCategoryResponse response = _mapper.Map<CreatedSubCategoryResponse>(subCategory);
            return response;
        }
    }
}