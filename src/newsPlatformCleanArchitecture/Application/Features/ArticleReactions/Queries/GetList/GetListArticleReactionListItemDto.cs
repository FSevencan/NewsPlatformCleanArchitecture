using Core.Application.Dtos;

namespace Application.Features.ArticleReactions.Queries.GetList;

public class GetListArticleReactionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }
    
}