using Application.Features.ArticleTags.Constants;
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

namespace Application.Features.ArticleTags.Commands.Delete;

public class DeleteArticleTagCommand : IRequest<DeletedArticleTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticleTagsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleTags";

    public class DeleteArticleTagCommandHandler : IRequestHandler<DeleteArticleTagCommand, DeletedArticleTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly ArticleTagBusinessRules _articleTagBusinessRules;

        public DeleteArticleTagCommandHandler(IMapper mapper, IArticleTagRepository articleTagRepository,
                                         ArticleTagBusinessRules articleTagBusinessRules)
        {
            _mapper = mapper;
            _articleTagRepository = articleTagRepository;
            _articleTagBusinessRules = articleTagBusinessRules;
        }

        public async Task<DeletedArticleTagResponse> Handle(DeleteArticleTagCommand request, CancellationToken cancellationToken)
        {
            ArticleTag? articleTag = await _articleTagRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _articleTagBusinessRules.ArticleTagShouldExistWhenSelected(articleTag);

            await _articleTagRepository.DeleteAsync(articleTag!);

            DeletedArticleTagResponse response = _mapper.Map<DeletedArticleTagResponse>(articleTag);
            return response;
        }
    }
}