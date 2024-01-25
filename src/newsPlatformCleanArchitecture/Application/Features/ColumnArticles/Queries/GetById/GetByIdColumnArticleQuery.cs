using Application.Features.ColumnArticles.Constants;
using Application.Features.ColumnArticles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ColumnArticles.Constants.ColumnArticlesOperationClaims;

namespace Application.Features.ColumnArticles.Queries.GetById;

public class GetByIdColumnArticleQuery : IRequest<GetByIdColumnArticleResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdColumnArticleQueryHandler : IRequestHandler<GetByIdColumnArticleQuery, GetByIdColumnArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

        public GetByIdColumnArticleQueryHandler(IMapper mapper, IColumnArticleRepository columnArticleRepository, ColumnArticleBusinessRules columnArticleBusinessRules)
        {
            _mapper = mapper;
            _columnArticleRepository = columnArticleRepository;
            _columnArticleBusinessRules = columnArticleBusinessRules;
        }

        public async Task<GetByIdColumnArticleResponse> Handle(GetByIdColumnArticleQuery request, CancellationToken cancellationToken)
        {
            ColumnArticle? columnArticle = await _columnArticleRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _columnArticleBusinessRules.ColumnArticleShouldExistWhenSelected(columnArticle);

            GetByIdColumnArticleResponse response = _mapper.Map<GetByIdColumnArticleResponse>(columnArticle);
            return response;
        }
    }
}