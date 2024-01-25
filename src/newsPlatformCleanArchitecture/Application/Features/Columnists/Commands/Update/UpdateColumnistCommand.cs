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

namespace Application.Features.Columnists.Commands.Update;

public class UpdateColumnistCommand : IRequest<UpdatedColumnistResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public string Email { get; set; }
    public string LinkedinLink { get; set; }

    public string[] Roles => new[] { Admin, Write, ColumnistsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnists";

    public class UpdateColumnistCommandHandler : IRequestHandler<UpdateColumnistCommand, UpdatedColumnistResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnistRepository _columnistRepository;
        private readonly ColumnistBusinessRules _columnistBusinessRules;

        public UpdateColumnistCommandHandler(IMapper mapper, IColumnistRepository columnistRepository,
                                         ColumnistBusinessRules columnistBusinessRules)
        {
            _mapper = mapper;
            _columnistRepository = columnistRepository;
            _columnistBusinessRules = columnistBusinessRules;
        }

        public async Task<UpdatedColumnistResponse> Handle(UpdateColumnistCommand request, CancellationToken cancellationToken)
        {
            Columnist? columnist = await _columnistRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _columnistBusinessRules.ColumnistShouldExistWhenSelected(columnist);
            columnist = _mapper.Map(request, columnist);

            await _columnistRepository.UpdateAsync(columnist!);

            UpdatedColumnistResponse response = _mapper.Map<UpdatedColumnistResponse>(columnist);
            return response;
        }
    }
}