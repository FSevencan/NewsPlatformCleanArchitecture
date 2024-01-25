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

namespace Application.Features.ArticleReactions.Commands.Create;

public class CreateArticleReactionCommand : IRequest<CreatedArticleReactionResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }
  

    public string[] Roles => new[] { Admin, Write, ArticleReactionsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleReactions";

    public class CreateArticleReactionCommandHandler : IRequestHandler<CreateArticleReactionCommand, CreatedArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly IArticleRepository _articleRepository; // Eklendi
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public CreateArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,
                                         IArticleRepository articleRepository, // Eklendi
                                         ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleRepository = articleRepository; // Eklendi
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<CreatedArticleReactionResponse> Handle(CreateArticleReactionCommand request, CancellationToken cancellationToken)
        {
            // TEST ASAMASINDA DUZELTILECEK //


            var existingReaction = await _articleReactionRepository.GetAsync(
                ar => ar.ArticleId == request.ArticleId && ar.VoterIdentifier == request.VoterIdentifier,
                cancellationToken: cancellationToken
            );

            var article = await _articleRepository.GetAsync(
                a => a.Id == request.ArticleId, // Makaleyi ArticleId'ye göre filtreleme
                cancellationToken: cancellationToken
            );

            if (existingReaction != null)
            {
                // Kullanýcýnýn mevcut tepkisini kaldýr ve sayýlarý güncelle
                if (existingReaction.IsLiked && request.IsLiked)
                {
                    // Kullanýcý beðeniyi geri alýyor
                    article.TotalLikes = (article.TotalLikes ?? 0) - 1;
                }
                else if (!existingReaction.IsLiked && !request.IsLiked)
                {
                    // Kullanýcý beðenmeme'yi geri alýyor
                    article.TotalDislikes = (article.TotalDislikes ?? 0) - 1;
                }

                await _articleReactionRepository.DeleteAsync(existingReaction);
            }
            else
            {
                // Yeni tepki oluþtur ve sayýlarý güncelle
                var articleReaction = _mapper.Map<ArticleReaction>(request);
                await _articleReactionRepository.AddAsync(articleReaction);

                if (request.IsLiked)
                {
                    article.TotalLikes = (article.TotalLikes ?? 0) + 1;
                }
                else
                {
                    article.TotalDislikes = (article.TotalDislikes ?? 0) + 1;
                }
            }

            // Makale güncellemelerini kaydet
            await _articleRepository.UpdateAsync(article);


            var response = _mapper.Map<CreatedArticleReactionResponse>(existingReaction);
            return response;
        }
    }
}



