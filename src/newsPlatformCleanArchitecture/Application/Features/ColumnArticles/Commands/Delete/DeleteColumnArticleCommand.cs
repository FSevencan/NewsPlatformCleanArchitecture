using Application.Features.ColumnArticles.Constants;
using Application.Features.ColumnArticles.Constants;
using Application.Features.ColumnArticles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ColumnArticles.Constants.ColumnArticlesOperationClaims;

namespace Application.Features.ColumnArticles.Commands.Delete;

public class DeleteColumnArticleCommand : IRequest<DeletedColumnArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ColumnArticlesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnArticles";

    public class DeleteColumnArticleCommandHandler : IRequestHandler<DeleteColumnArticleCommand, DeletedColumnArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

        public DeleteColumnArticleCommandHandler(IMapper mapper, IColumnArticleRepository columnArticleRepository,
                                         ColumnArticleBusinessRules columnArticleBusinessRules)
        {
            _mapper = mapper;
            _columnArticleRepository = columnArticleRepository;
            _columnArticleBusinessRules = columnArticleBusinessRules;
        }

        public async Task<DeletedColumnArticleResponse> Handle(DeleteColumnArticleCommand request, CancellationToken cancellationToken)
        {
            ColumnArticle? columnArticle = await _columnArticleRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _columnArticleBusinessRules.ColumnArticleShouldExistWhenSelected(columnArticle);

            await _columnArticleRepository.DeleteAsync(columnArticle!);

            DeletedColumnArticleResponse response = _mapper.Map<DeletedColumnArticleResponse>(columnArticle);
            return response;
        }
    }
}