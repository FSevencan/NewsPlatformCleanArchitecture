using Application.Features.Columnists.Commands.Create;
using Application.Features.Columnists.Commands.Delete;
using Application.Features.Columnists.Commands.Update;
using Application.Features.Columnists.Queries.GetById;
using Application.Features.Columnists.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Columnists.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Columnist, CreateColumnistCommand>().ReverseMap();
        CreateMap<Columnist, CreatedColumnistResponse>().ReverseMap();
        CreateMap<Columnist, UpdateColumnistCommand>().ReverseMap();
        CreateMap<Columnist, UpdatedColumnistResponse>().ReverseMap();
        CreateMap<Columnist, DeleteColumnistCommand>().ReverseMap();
        CreateMap<Columnist, DeletedColumnistResponse>().ReverseMap();
        CreateMap<Columnist, GetByIdColumnistResponse>().ReverseMap();
        CreateMap<Columnist, GetListColumnistListItemDto>().ReverseMap();
        CreateMap<IPaginate<Columnist>, GetListResponse<GetListColumnistListItemDto>>().ReverseMap();
    }
}