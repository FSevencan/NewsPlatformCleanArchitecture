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

namespace Application.Features.ColumnArticles.Commands.Update;

public class UpdateColumnArticleCommand : IRequest<UpdatedColumnArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ColumnistId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string FeaturedImage { get; set; }
    

    public string[] Roles => new[] { Admin, Write, ColumnArticlesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnArticles";

    public class UpdateColumnArticleCommandHandler : IRequestHandler<UpdateColumnArticleCommand, UpdatedColumnArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

        public UpdateColumnArticleCommandHandler(IMapper mapper, IColumnArticleRepository columnArticleRepository,
                                         ColumnArticleBusinessRules columnArticleBusinessRules)
        {
            _mapper = mapper;
            _columnArticleRepository = columnArticleRepository;
            _columnArticleBusinessRules = columnArticleBusinessRules;
        }

        public async Task<UpdatedColumnArticleResponse> Handle(UpdateColumnArticleCommand request, CancellationToken cancellationToken)
        {
            ColumnArticle? columnArticle = await _columnArticleRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _columnArticleBusinessRules.ColumnArticleShouldExistWhenSelected(columnArticle);
            columnArticle = _mapper.Map(request, columnArticle);

            await _columnArticleRepository.UpdateAsync(columnArticle!);

            UpdatedColumnArticleResponse response = _mapper.Map<UpdatedColumnArticleResponse>(columnArticle);
            return response;
        }
    }
}