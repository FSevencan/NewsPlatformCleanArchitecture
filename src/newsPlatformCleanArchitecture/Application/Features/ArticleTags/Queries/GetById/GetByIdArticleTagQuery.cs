using Application.Features.ArticleTags.Constants;
using Application.Features.ArticleTags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ArticleTags.Constants.ArticleTagsOperationClaims;

namespace Application.Features.ArticleTags.Queries.GetById;

public class GetByIdArticleTagQuery : IRequest<GetByIdArticleTagResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdArticleTagQueryHandler : IRequestHandler<GetByIdArticleTagQuery, GetByIdArticleTagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly ArticleTagBusinessRules _articleTagBusinessRules;

        public GetByIdArticleTagQueryHandler(IMapper mapper, IArticleTagRepository articleTagRepository, ArticleTagBusinessRules articleTagBusinessRules)
        {
            _mapper = mapper;
            _articleTagRepository = articleTagRepository;
            _articleTagBusinessRules = articleTagBusinessRules;
        }

        public async Task<GetByIdArticleTagResponse> Handle(GetByIdArticleTagQuery request, CancellationToken cancellationToken)
        {
            ArticleTag? articleTag = await _articleTagRepository.GetAsync(predicate: at => at.Id == request.Id, cancellationToken: cancellationToken);
            await _articleTagBusinessRules.ArticleTagShouldExistWhenSelected(articleTag);

            GetByIdArticleTagResponse response = _mapper.Map<GetByIdArticleTagResponse>(articleTag);
            return response;
        }
    }
}