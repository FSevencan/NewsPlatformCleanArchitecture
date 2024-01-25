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

namespace Application.Features.ArticleTags.Commands.Update;

public class UpdateArticleTagCommand : IRequest<UpdatedArticleTagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }


    public string[] Roles => new[] { Admin, Write, ArticleTagsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticleTags";

    public class UpdateArticleTagCommandHandler : IRequestHandler<UpdateArticleTagCommand, UpdatedArticleTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly ArticleTagBusinessRules _articleTagBusinessRules;

        public UpdateArticleTagCommandHandler(IMapper mapper, IArticleTagRepository articleTagRepository,
                                         ArticleTagBusinessRules articleTagBusinessRules)
        {
            _mapper = mapper;
            _articleTagRepository = articleTagRepository;
            _articleTagBusinessRules = articleTagBusinessRules;
        }

        public async Task<UpdatedArticleTagResponse> Handle(UpdateArticleTagCommand request, CancellationToken cancellationToken)
        {
            ArticleTag? articleTag = await _articleTagRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _articleTagBusinessRules.ArticleTagShouldExistWhenSelected(articleTag);
            articleTag = _mapper.Map(request, articleTag);

            await _articleTagRepository.UpdateAsync(articleTag!);

            UpdatedArticleTagResponse response = _mapper.Map<UpdatedArticleTagResponse>(articleTag);
            return response;
        }
    }
}