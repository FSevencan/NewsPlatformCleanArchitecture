using Core.Application.Responses;

public class GetByArticleAndVoterResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public bool IsLiked { get; set; }
    public string VoterIdentifier { get; set; }
}