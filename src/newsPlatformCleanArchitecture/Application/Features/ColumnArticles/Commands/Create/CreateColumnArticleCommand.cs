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

namespace Application.Features.ColumnArticles.Commands.Create;

public class CreateColumnArticleCommand : IRequest<CreatedColumnArticleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ColumnistId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string FeaturedImage { get; set; }


    public string[] Roles => new[] { Admin, Write, ColumnArticlesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnArticles";

    public class CreateColumnArticleCommandHandler : IRequestHandler<CreateColumnArticleCommand, CreatedColumnArticleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnArticleRepository _columnArticleRepository;
        private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

        public CreateColumnArticleCommandHandler(IMapper mapper, IColumnArticleRepository columnArticleRepository,
                                         ColumnArticleBusinessRules columnArticleBusinessRules)
        {
            _mapper = mapper;
            _columnArticleRepository = columnArticleRepository;
            _columnArticleBusinessRules = columnArticleBusinessRules;
        }

        public async Task<CreatedColumnArticleResponse> Handle(CreateColumnArticleCommand request, CancellationToken cancellationToken)
        {
            ColumnArticle columnArticle = _mapper.Map<ColumnArticle>(request);

            await _columnArticleRepository.AddAsync(columnArticle);

            CreatedColumnArticleResponse response = _mapper.Map<CreatedColumnArticleResponse>(columnArticle);
            return response;
        }
    }
}