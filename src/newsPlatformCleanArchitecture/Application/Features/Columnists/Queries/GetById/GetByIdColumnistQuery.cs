using Application.Features.Columnists.Constants;
using Application.Features.Columnists.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Columnists.Constants.ColumnistsOperationClaims;

namespace Application.Features.Columnists.Queries.GetById;

public class GetByIdColumnistQuery : IRequest<GetByIdColumnistResponse>
{
    public Guid Id { get; set; }


    public class GetByIdColumnistQueryHandler : IRequestHandler<GetByIdColumnistQuery, GetByIdColumnistResponse>
    {
        private readonly IMapper _mapper;
        private readonly IColumnistRepository _columnistRepository;
        private readonly ColumnistBusinessRules _columnistBusinessRules;

        public GetByIdColumnistQueryHandler(IMapper mapper, IColumnistRepository columnistRepository, ColumnistBusinessRules columnistBusinessRules)
        {
            _mapper = mapper;
            _columnistRepository = columnistRepository;
            _columnistBusinessRules = columnistBusinessRules;
        }

        public async Task<GetByIdColumnistResponse> Handle(GetByIdColumnistQuery request, CancellationToken cancellationToken)
        {
            Columnist? columnist = await _columnistRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _columnistBusinessRules.ColumnistShouldExistWhenSelected(columnist);

            GetByIdColumnistResponse response = _mapper.Map<GetByIdColumnistResponse>(columnist);
            return response;
        }
    }
}