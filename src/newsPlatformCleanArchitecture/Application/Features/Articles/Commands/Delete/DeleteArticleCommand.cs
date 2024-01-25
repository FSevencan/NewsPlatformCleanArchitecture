using Application.Features.Articles.Constants;
using Application.Features.Articles.Constants;
using Application.Features.Articles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Articles.Constants.ArticlesOperationClaims;

namespace Application.Features.Articles.Commands.Delete;

public class DeleteArticleCommand : IRequest<DeletedArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticlesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticles";

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, DeletedArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleBusinessRules _articleBusinessRules;

        public DeleteArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository,
                                         ArticleBusinessRules articleBusinessRules)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<DeletedArticleResponse> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            Article? article = await _articleRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _articleBusinessRules.ArticleShouldExistWhenSelected(article);

            await _articleRepository.DeleteAsync(article!);

            DeletedArticleResponse response = _mapper.Map<DeletedArticleResponse>(article);
            return response;
        }
    }
}