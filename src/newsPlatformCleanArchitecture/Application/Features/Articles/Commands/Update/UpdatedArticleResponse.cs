using Core.Application.Responses;

namespace Application.Features.Articles.Commands.Update;

public class UpdatedArticleResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SubcategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string? Slug { get; set; }
    public int? TotalLikes { get; set; }
    public int? TotalDislikes { get; set; }
    
}