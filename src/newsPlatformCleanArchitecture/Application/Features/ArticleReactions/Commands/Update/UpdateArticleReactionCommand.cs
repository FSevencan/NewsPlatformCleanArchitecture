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

namespace Application.Features.ArticleReactions.Commands.Update;

public class UpdateArticleReactionCommand : IRequest<UpdatedArticleReactionResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }
 

    public string[] Roles => new[] { Admin, Write, ArticleReactionsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleReactions";

    public class UpdateArticleReactionCommandHandler : IRequestHandler<UpdateArticleReactionCommand, UpdatedArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public UpdateArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,
                                         ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<UpdatedArticleReactionResponse> Handle(UpdateArticleReactionCommand request, CancellationToken cancellationToken)
        {
            ArticleReaction? articleReaction = await _articleReactionRepository.GetAsync(predicate: ar => ar.Id == request.Id, cancellationToken: cancellationToken);
            await _articleReactionBusinessRules.ArticleReactionShouldExistWhenSelected(articleReaction);
            articleReaction = _mapper.Map(request, articleReaction);

            await _articleReactionRepository.UpdateAsync(articleReaction!);

            UpdatedArticleReactionResponse response = _mapper.Map<UpdatedArticleReactionResponse>(articleReaction);
            return response;
        }
    }
}