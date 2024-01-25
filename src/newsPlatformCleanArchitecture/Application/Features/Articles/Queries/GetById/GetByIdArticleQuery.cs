using Application.Features.Articles.Constants;
using Application.Features.Articles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Articles.Constants.ArticlesOperationClaims;

namespace Application.Features.Articles.Queries.GetById;

public class GetByIdArticleQuery : IRequest<GetByIdArticleResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdArticleQueryHandler : IRequestHandler<GetByIdArticleQuery, GetByIdArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ArticleBusinessRules _articleBusinessRules;

        public GetByIdArticleQueryHandler(IMapper mapper, IArticleRepository articleRepository, ArticleBusinessRules articleBusinessRules)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _articleBusinessRules = articleBusinessRules;
        }

        public async Task<GetByIdArticleResponse> Handle(GetByIdArticleQuery request, CancellationToken cancellationToken)
        {
            Article? article = await _articleRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _articleBusinessRules.ArticleShouldExistWhenSelected(article);

            GetByIdArticleResponse response = _mapper.Map<GetByIdArticleResponse>(article);
            return response;
        }
    }
}