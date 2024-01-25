using Application.Features.Subscribles.Constants;
using Application.Features.Subscribles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Subscribles.Constants.SubscriblesOperationClaims;

namespace Application.Features.Subscribles.Queries.GetById;

public class GetByIdSubscribleQuery : IRequest<GetByIdSubscribleResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdSubscribleQueryHandler : IRequestHandler<GetByIdSubscribleQuery, GetByIdSubscribleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubscribleRepository _subscribleRepository;
        private readonly SubscribleBusinessRules _subscribleBusinessRules;

        public GetByIdSubscribleQueryHandler(IMapper mapper, ISubscribleRepository subscribleRepository, SubscribleBusinessRules subscribleBusinessRules)
        {
            _mapper = mapper;
            _subscribleRepository = subscribleRepository;
            _subscribleBusinessRules = subscribleBusinessRules;
        }

        public async Task<GetByIdSubscribleResponse> Handle(GetByIdSubscribleQuery request, CancellationToken cancellationToken)
        {
            Subscrible? subscrible = await _subscribleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _subscribleBusinessRules.SubscribleShouldExistWhenSelected(subscrible);

            GetByIdSubscribleResponse response = _mapper.Map<GetByIdSubscribleResponse>(subscrible);
            return response;
        }
    }
}