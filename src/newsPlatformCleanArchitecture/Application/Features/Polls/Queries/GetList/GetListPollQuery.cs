using Application.Features.Polls.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Polls.Constants.PollsOperationClaims;

namespace Application.Features.Polls.Queries.GetList;

public class GetListPollQuery : IRequest<GetListResponse<GetListPollListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPolls({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPolls";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPollQueryHandler : IRequestHandler<GetListPollQuery, GetListResponse<GetListPollListItemDto>>
    {
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;

        public GetListPollQueryHandler(IPollRepository pollRepository, IMapper mapper)
        {
            _pollRepository = pollRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPollListItemDto>> Handle(GetListPollQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Poll> polls = await _pollRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPollListItemDto> response = _mapper.Map<GetListResponse<GetListPollListItemDto>>(polls);
            return response;
        }
    }
}