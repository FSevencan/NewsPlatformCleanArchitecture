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

namespace Application.Features.Columnists.Commands.Create;

public class CreateColumnistCommand : IRequest<CreatedColumnistResponse> ,ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ProfilePicture { get; set; }
    public string Email { get; set; }
    public string LinkedinLink { get; set; }

    public string[] Roles => new[] { Admin, Write, ColumnistsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetColumnists";

    public class CreateColumnistCommandHandler : IRequestHandler<CreateColumnistCommand, CreatedColumnistResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnistRepository _columnistRepository;
        private readonly ColumnistBusinessRules _columnistBusinessRules;

        public CreateColumnistCommandHandler(IMapper mapper, IColumnistRepository columnistRepository,
                                         ColumnistBusinessRules columnistBusinessRules)
        {
            _mapper = mapper;
            _columnistRepository = columnistRepository;
            _columnistBusinessRules = columnistBusinessRules;
        }

        public async Task<CreatedColumnistResponse> Handle(CreateColumnistCommand request, CancellationToken cancellationToken)
        {
            Columnist columnist = _mapper.Map<Columnist>(request);

            await _columnistRepository.AddAsync(columnist);

            CreatedColumnistResponse response = _mapper.Map<CreatedColumnistResponse>(columnist);
            return response;
        }
    }
}