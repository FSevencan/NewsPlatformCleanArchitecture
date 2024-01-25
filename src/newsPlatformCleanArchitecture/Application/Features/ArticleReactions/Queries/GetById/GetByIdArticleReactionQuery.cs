using Application.Features.ArticleReactions.Constants;
using Application.Features.ArticleReactions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ArticleReactions.Constants.ArticleReactionsOperationClaims;

namespace Application.Features.ArticleReactions.Queries.GetById;
public class GetByIdArticleReactionQuery : IRequest<GetByIdArticleReactionResponse>/*, ISecuredRequest*/
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdArticleReactionQueryHandler : IRequestHandler<GetByIdArticleReactionQuery, GetByIdArticleReactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleReactionRepository _articleReactionRepository;
        private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

        public GetByIdArticleReactionQueryHandler(IMapper mapper, IArticleReactionRepository articleReactionRepository, ArticleReactionBusinessRules articleReactionBusinessRules)
        {
            _mapper = mapper;
            _articleReactionRepository = articleReactionRepository;
            _articleReactionBusinessRules = articleReactionBusinessRules;
        }

        public async Task<GetByIdArticleReactionResponse> Handle(GetByIdArticleReactionQuery request, CancellationToken cancellationToken)
        {
            ArticleReaction? articleReaction = await _articleReactionRepository.GetAsync(predicate: ar => ar.Id == request.Id, cancellationToken: cancellationToken);
            await _articleReactionBusinessRules.ArticleReactionShouldExistWhenSelected(articleReaction);

            GetByIdArticleReactionResponse response = _mapper.Map<GetByIdArticleReactionResponse>(articleReaction);
            return response;
        }
    }
}
