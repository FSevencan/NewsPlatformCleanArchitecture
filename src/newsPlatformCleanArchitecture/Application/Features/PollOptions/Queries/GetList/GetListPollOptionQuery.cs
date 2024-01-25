using Application.Features.PollOptions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.PollOptions.Constants.PollOptionsOperationClaims;

namespace Application.Features.PollOptions.Queries.GetList;

public class GetListPollOptionQuery : IRequest<GetListResponse<GetListPollOptionListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPollOptions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPollOptions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPollOptionQueryHandler : IRequestHandler<GetListPollOptionQuery, GetListResponse<GetListPollOptionListItemDto>>
    {
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly IMapper _mapper;

        public GetListPollOptionQueryHandler(IPollOptionRepository pollOptionRepository, IMapper mapper)
        {
            _pollOptionRepository = pollOptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPollOptionListItemDto>> Handle(GetListPollOptionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<PollOption> pollOptions = await _pollOptionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPollOptionListItemDto> response = _mapper.Map<GetListResponse<GetListPollOptionListItemDto>>(pollOptions);
            return response;
        }
    }
}