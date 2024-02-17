using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Articles.Queries.GetLatestArticlesByCategory;
public class GetLastArticleByCategoryItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string SubCategoryName { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? TotalLikes { get; set; }
}
