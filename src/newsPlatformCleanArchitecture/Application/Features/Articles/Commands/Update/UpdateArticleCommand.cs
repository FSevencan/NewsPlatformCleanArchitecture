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

namespace Application.Features.Articles.Commands.Update;

public class UpdateArticleCommand : IRequest<UpdatedArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid SubcategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string? Slug { get; set; }
    public int? TotalLikes { get; set; }
    public int? TotalDislikes { get; set; }
    public SubCategory SubCategory { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticlesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticles";

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, UpdatedArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleBusinessRules _articleBusinessRules;

        public UpdateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository,
                                         ArticleBusinessRules articleBusinessRules)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<UpdatedArticleResponse> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            Article? article = await _articleRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _articleBusinessRules.ArticleShouldExistWhenSelected(article);
            article = _mapper.Map(request, article);

            await _articleRepository.UpdateAsync(article!);

            UpdatedArticleResponse response = _mapper.Map<UpdatedArticleResponse>(article);
            return response;
        }
    }
}