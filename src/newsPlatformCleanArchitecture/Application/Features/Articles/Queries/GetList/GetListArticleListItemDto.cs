using Core.Application.Dtos;

namespace Application.Features.Articles.Queries.GetList;

public class GetListArticleListItemDto : IDto
{
    public Guid Id { get; set; }
    public string SubCategoryName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string? Slug { get; set; }
    public int? TotalLikes { get; set; }
    public int? TotalDislikes { get; set; }
    public DateTime? CreatedDate { get; set; }

}