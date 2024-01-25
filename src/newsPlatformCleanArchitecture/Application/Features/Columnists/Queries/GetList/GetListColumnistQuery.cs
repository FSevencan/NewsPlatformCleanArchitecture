using Application.Features.Columnists.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Columnists.Constants.ColumnistsOperationClaims;

namespace Application.Features.Columnists.Queries.GetList;

public class GetListColumnistQuery : IRequest<GetListResponse<GetListColumnistListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListColumnists({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetColumnists";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListColumnistQueryHandler : IRequestHandler<GetListColumnistQuery, GetListResponse<GetListColumnistListItemDto>>
    {
        private readonly IColumnistRepository _columnistRepository;
        private readonly IMapper _mapper;

        public GetListColumnistQueryHandler(IColumnistRepository columnistRepository, IMapper mapper)
        {
            _columnistRepository = columnistRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListColumnistListItemDto>> Handle(GetListColumnistQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Columnist> columnists = await _columnistRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListColumnistListItemDto> response = _mapper.Map<GetListResponse<GetListColumnistListItemDto>>(columnists);
            return response;
        }
    }
}