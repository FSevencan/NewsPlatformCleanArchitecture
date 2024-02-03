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
using Application.Features.Articles.Rules;

namespace Application.Features.ArticleReactions.Commands.Update;

public class UpdateArticleReactionCommand : IRequest<UpdatedArticleReactionResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ArticleId { get; set; }
    public string VoterIdentifier { get; set; }
    public bool IsLiked { get; set; }


    public string[] Roles => new[] { Admin, Write, ArticleReactionsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleReactions";

    public class UpdateArticleReactionCommandHandler : IRequestHandler<UpdateArticleReactionCommand, UpdatedArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;
        private readonly ArticleBusinessRules _articleBusinessRules;
        public UpdateArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,
                                         IArticleRepository articleRepository, ArticleBusinessRules articleBusinessRules ,ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleRepository = articleRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<UpdatedArticleReactionResponse> Handle(UpdateArticleReactionCommand request, CancellationToken cancellationToken)
        {
            // ArticleId ve VoterIdentifier kullanarak tepkiyi ve makaleyi bul
            var articleReaction = await _articleReactionRepository.GetAsync(
                ar => ar.ArticleId == request.ArticleId && ar.VoterIdentifier == request.VoterIdentifier,
                cancellationToken: cancellationToken
            );

            // Tepkinin ve makalenin var olduðunu kontrol et
            await _articleReactionBusinessRules.ArticleReactionShouldExistWhenSelected(articleReaction);

            var article = await _articleRepository.GetAsync(
                a => a.Id == request.ArticleId,
                cancellationToken: cancellationToken
            );

            await _articleBusinessRules.ArticleShouldExistWhenSelected(article);

            // Makale tepki sayýlarýný güncelle
            await _articleReactionBusinessRules.UpdateArticleReactionCountsOnUpdate(article, articleReaction, request.IsLiked, cancellationToken);

            // Tepkinin IsLiked durumunu güncelle ve tepkiyi güncelle
            articleReaction.IsLiked = request.IsLiked;
            await _articleReactionRepository.UpdateAsync(articleReaction);

            // Yanýtý oluþtur ve dön
            UpdatedArticleReactionResponse response = _mapper.Map<UpdatedArticleReactionResponse>(articleReaction);
            return response;
        }
    }
}