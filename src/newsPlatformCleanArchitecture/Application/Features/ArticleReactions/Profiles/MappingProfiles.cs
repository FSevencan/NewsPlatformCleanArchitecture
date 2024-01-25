using Application.Features.ArticleReactions.Commands.Create;
using Application.Features.ArticleReactions.Commands.Delete;
using Application.Features.ArticleReactions.Commands.Update;
using Application.Features.ArticleReactions.Queries.GetById;
using Application.Features.ArticleReactions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ArticleReactions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ArticleReaction, CreateArticleReactionCommand>().ReverseMap();
        CreateMap<ArticleReaction, CreatedArticleReactionResponse>().ReverseMap();
        CreateMap<ArticleReaction, UpdateArticleReactionCommand>().ReverseMap();
        CreateMap<ArticleReaction, UpdatedArticleReactionResponse>().ReverseMap();
        CreateMap<ArticleReaction, DeleteArticleReactionCommand>().ReverseMap();
        CreateMap<ArticleReaction, DeletedArticleReactionResponse>().ReverseMap();
        CreateMap<ArticleReaction, GetByIdArticleReactionResponse>().ReverseMap();
        CreateMap<ArticleReaction, GetListArticleReactionListItemDto>().ReverseMap();
        CreateMap<IPaginate<ArticleReaction>, GetListResponse<GetListArticleReactionListItemDto>>().ReverseMap();

        CreateMap<ArticleReaction, GetByArticleAndVoterResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.ArticleId))
            .ForMember(dest => dest.IsLiked, opt => opt.MapFrom(src => src.IsLiked))
            .ForMember(dest => dest.VoterIdentifier, opt => opt.MapFrom(src => src.VoterIdentifier));

    }
}