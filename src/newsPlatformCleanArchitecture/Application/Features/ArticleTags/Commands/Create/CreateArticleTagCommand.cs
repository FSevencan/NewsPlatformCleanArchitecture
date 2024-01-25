using Application.Features.ArticleTags.Constants;
using Application.Features.ArticleTags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ArticleTags.Constants.ArticleTagsOperationClaims;

namespace Application.Features.ArticleTags.Commands.Create;

public class CreateArticleTagCommand : IRequest<CreatedArticleTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }


    public string[] Roles => new[] { Admin, Write, ArticleTagsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleTags";

    public class CreateArticleTagCommandHandler : IRequestHandler<CreateArticleTagCommand, CreatedArticleTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly ArticleTagBusinessRules _articleTagBusinessRules;

        public CreateArticleTagCommandHandler(IMapper mapper, IArticleTagRepository articleTagRepository,
                                         ArticleTagBusinessRules articleTagBusinessRules)
        {
            _mapper = mapper;
            _articleTagRepository = articleTagRepository;
            _articleTagBusinessRules = articleTagBusinessRules;
        }

        public async Task<CreatedArticleTagResponse> Handle(CreateArticleTagCommand request, CancellationToken cancellationToken)
        {
            ArticleTag articleTag = _mapper.Map<ArticleTag>(request);

            await _articleTagRepository.AddAsync(articleTag);

            CreatedArticleTagResponse response = _mapper.Map<CreatedArticleTagResponse>(articleTag);
            return response;
        }
    }
}