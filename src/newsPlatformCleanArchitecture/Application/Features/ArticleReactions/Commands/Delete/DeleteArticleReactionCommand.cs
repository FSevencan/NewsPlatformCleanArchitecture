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

namespace Application.Features.ArticleReactions.Commands.Delete;

public class DeleteArticleReactionCommand : IRequest<DeletedArticleReactionResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticleReactionsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleReactions";

    public class DeleteArticleReactionCommandHandler : IRequestHandler<DeleteArticleReactionCommand, DeletedArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public DeleteArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,
                                         ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<DeletedArticleReactionResponse> Handle(DeleteArticleReactionCommand request, CancellationToken cancellationToken)
        {
            ArticleReaction? articleReaction = await _articleReactionRepository.GetAsync(predicate: ar => ar.Id == request.Id, cancellationToken: cancellationToken);
            await _articleReactionBusinessRules.ArticleReactionShouldExistWhenSelected(articleReaction);

            await _articleReactionRepository.DeleteAsync(articleReaction!);

            DeletedArticleReactionResponse response = _mapper.Map<DeletedArticleReactionResponse>(articleReaction);
            return response;
        }
    }
}