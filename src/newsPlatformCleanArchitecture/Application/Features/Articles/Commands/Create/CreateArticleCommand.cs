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
using Core.Application.Utilities;

namespace Application.Features.Articles.Commands.Create;

public class CreateArticleCommand : IRequest<CreatedArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid SubcategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public ICollection<Guid>? TagIds { get; set; }

    public string[] Roles => new[] { Admin, Write, ArticlesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetArticles";

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreatedArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleBusinessRules _articleBusinessRules;

        public CreateArticleCommandHandler(IMapper mapper, IArticleRepository articleRepository,
                                         ArticleBusinessRules articleBusinessRules)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<CreatedArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            Article article = _mapper.Map<Article>(request);

            article.Slug = Slug.CreateSlug(request.Title);

            article.ArticleTags = request.TagIds.Select(TagId => new ArticleTag
            {
                TagId = TagId,
                CreatedDate = DateTime.Now,

            }).ToList();

            await _articleRepository.AddAsync(article);

            CreatedArticleResponse response = _mapper.Map<CreatedArticleResponse>(article);
            return response;
        }
    }
}