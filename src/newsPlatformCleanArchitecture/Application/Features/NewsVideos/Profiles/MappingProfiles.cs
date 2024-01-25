using Application.Features.NewsVideos.Commands.Create;
using Application.Features.NewsVideos.Commands.Delete;
using Application.Features.NewsVideos.Commands.Update;
using Application.Features.NewsVideos.Queries.GetById;
using Application.Features.NewsVideos.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.NewsVideos.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<NewsVideo, CreateNewsVideoCommand>().ReverseMap();
        CreateMap<NewsVideo, CreatedNewsVideoResponse>().ReverseMap();
        CreateMap<NewsVideo, UpdateNewsVideoCommand>().ReverseMap();
        CreateMap<NewsVideo, UpdatedNewsVideoResponse>().ReverseMap();
        CreateMap<NewsVideo, DeleteNewsVideoCommand>().ReverseMap();
        CreateMap<NewsVideo, DeletedNewsVideoResponse>().ReverseMap();
        CreateMap<NewsVideo, GetByIdNewsVideoResponse>().ReverseMap();
        CreateMap<NewsVideo, GetListNewsVideoListItemDto>().ReverseMap();
        CreateMap<IPaginate<NewsVideo>, GetListResponse<GetListNewsVideoListItemDto>>().ReverseMap();
    }
}