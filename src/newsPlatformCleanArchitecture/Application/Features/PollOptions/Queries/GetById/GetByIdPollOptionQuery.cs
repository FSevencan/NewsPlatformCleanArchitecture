using Application.Features.PollOptions.Constants;
using Application.Features.PollOptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.PollOptions.Constants.PollOptionsOperationClaims;
using Core.Persistence.Paging;

namespace Application.Features.PollOptions.Queries.GetById;

public class GetByIdPollOptionQuery : IRequest<GetByIdPollOptionResponse>
{
    public Guid PollId { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPollOptionQueryHandler : IRequestHandler<GetByIdPollOptionQuery, GetByIdPollOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly PollOptionBusinessRules _pollOptionBusinessRules;

        public GetByIdPollOptionQueryHandler(IMapper mapper, IPollOptionRepository pollOptionRepository, PollOptionBusinessRules pollOptionBusinessRules)
        {
            _mapper = mapper;
            _pollOptionRepository = pollOptionRepository;
            _pollOptionBusinessRules = pollOptionBusinessRules;
        }

        public async Task<GetByIdPollOptionResponse> Handle(GetByIdPollOptionQuery request, CancellationToken cancellationToken)
        {
            
            IPaginate<PollOption> pollOptions = await _pollOptionRepository.GetListAsync(
                predicate: po => po.PollId == request.PollId,
                cancellationToken: cancellationToken
            );
           
            ICollection<PollOptionListDto> pollOptionDtos = _mapper.Map<ICollection<PollOptionListDto>>(pollOptions.Items);

            GetByIdPollOptionResponse response = new GetByIdPollOptionResponse
            {
                PollOptions = pollOptionDtos
            };
            return response;
        }
    }
}