using Application.Features.Contacts.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Contacts.Constants.ContactsOperationClaims;

namespace Application.Features.Contacts.Queries.GetList;

public class GetListContactQuery : IRequest<GetListResponse<GetListContactListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContacts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContacts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContactQueryHandler : IRequestHandler<GetListContactQuery, GetListResponse<GetListContactListItemDto>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetListContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContactListItemDto>> Handle(GetListContactQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Contact> contacts = await _contactRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContactListItemDto> response = _mapper.Map<GetListResponse<GetListContactListItemDto>>(contacts);
            return response;
        }
    }
}