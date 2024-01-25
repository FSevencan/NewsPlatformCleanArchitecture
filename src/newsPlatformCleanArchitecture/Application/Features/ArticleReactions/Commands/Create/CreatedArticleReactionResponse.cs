using Core.Application.Responses;

namespace Application.Features.ArticleReactions.Commands.Create;

public class CreatedArticleReactionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }
 
}