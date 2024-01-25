using Application.Features.Columnists.Constants;
using Application.Features.Columnists.Constants;
using Application.Features.Columnists.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Columnists.Constants.ColumnistsOperationClaims;

namespace Application.Features.Columnists.Commands.Delete;

public class DeleteColumnistCommand : IRequest<DeletedColumnistResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ColumnistsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnists";

    public class DeleteColumnistCommandHandler : IRequestHandler<DeleteColumnistCommand, DeletedColumnistResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnistRepository _columnistRepository;
        private readonly ColumnistBusinessRules _columnistBusinessRules;

        public DeleteColumnistCommandHandler(IMapper mapper, IColumnistRepository columnistRepository,
                                         ColumnistBusinessRules columnistBusinessRules)
        {
            _mapper = mapper;
            _columnistRepository = columnistRepository;
            _columnistBusinessRules = columnistBusinessRules;
        }

        public async Task<DeletedColumnistResponse> Handle(DeleteColumnistCommand request, CancellationToken cancellationToken)
        {
            Columnist? columnist = await _columnistRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _columnistBusinessRules.ColumnistShouldExistWhenSelected(columnist);

            await _columnistRepository.DeleteAsync(columnist!);

            DeletedColumnistResponse response = _mapper.Map<DeletedColumnistResponse>(columnist);
            return response;
        }
    }
}