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
using System.Threading;

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
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public CreateArticleReactionCommandHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository,
                                         IArticleRepository articleRepository,
                                         ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleRepository = articleRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<CreatedArticleReactionResponse> Handle(CreateArticleReactionCommand request, CancellationToken cancellationToken)
        {

            await _articleReactionBusinessRules.ValidateReactionDoesNotExist(request.ArticleId, request.VoterIdentifier, cancellationToken);

            var articleReaction = _mapper.Map<ArticleReaction>(request);
            await _articleReactionRepository.AddAsync(articleReaction);

            await _articleReactionBusinessRules.UpdateArticleReactionCounts(request.ArticleId, request.IsLiked, cancellationToken);

           
            var response = _mapper.Map<CreatedArticleReactionResponse>(articleReaction);
            return response;
        }

    }
}



