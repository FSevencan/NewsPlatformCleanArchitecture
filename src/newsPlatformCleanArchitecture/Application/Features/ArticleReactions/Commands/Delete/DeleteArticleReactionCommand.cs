using Application.Features.ArticleReactions.Constants;
using Application.Features.ArticleReactions.Constants;
using Application.Features.ArticleReactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ArticleReactions.Constants.ArticleReactionsOperationClaims;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.ArticleReactions.Commands.Delete;

public class DeleteArticleReactionCommand : IRequest<DeletedArticleReactionResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ArticleId { get; set; }
    public string VoterIdentifier { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticleReactionsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleReactions";

    public class DeleteArticleReactionCommandHandler : IRequestHandler<DeleteArticleReactionCommand, DeletedArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public DeleteArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,IArticleRepository articleRepository,
                                         ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleRepository = articleRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<DeletedArticleReactionResponse> Handle(DeleteArticleReactionCommand request, CancellationToken cancellationToken)
        {
            var articleReaction = await _articleReactionRepository.GetAsync(
            ar => ar.ArticleId == request.ArticleId && ar.VoterIdentifier == request.VoterIdentifier,
            cancellationToken: cancellationToken
            );

            // Tepkinin var olduðunu kontrol et
            await _articleReactionBusinessRules.ArticleReactionShouldExistWhenSelected(articleReaction);

            // Tepkiyi sil
            await _articleReactionRepository.DeleteAsync(articleReaction);

            // Makalenin beðeni/beðenmeme sayýlarýný güncelle
            await _articleReactionBusinessRules.UpdateArticleReactionCountsOnDelete(articleReaction.ArticleId, articleReaction.IsLiked, cancellationToken);

            var response = _mapper.Map<DeletedArticleReactionResponse>(articleReaction);
            return response;
        }
    }
}