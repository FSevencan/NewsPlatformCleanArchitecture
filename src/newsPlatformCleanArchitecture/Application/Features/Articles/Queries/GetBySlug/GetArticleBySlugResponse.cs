using Application.Features.Tags.Queries.GetList;
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Articles.Queries.GetById;

public class GetArticleBySlugResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SubCategoryId { get; set; }
    public string SubcategoryName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string? slug { get; set; }
    public int? TotalLikes { get; set; }
    public int? TotalDislikes { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<GetListTagListItemDto> Tags { get; set; }

}