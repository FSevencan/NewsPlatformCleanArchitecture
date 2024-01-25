using Core.Application.Responses;

namespace Application.Features.ArticleReactions.Queries.GetById;

public class GetByIdArticleReactionResponse : IResponse
{
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }

}