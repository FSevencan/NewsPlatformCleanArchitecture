using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Article : Entity<Guid>
{
    public Guid SubcategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Summary { get; set; }
    public string? FeaturedImage { get; set; }
    public string? Slug { get; set; }
    public int? TotalLikes { get; set; } 
    public int? TotalDislikes { get; set; }

    public SubCategory? SubCategory { get; set; }

    public ICollection<ArticleTag>? ArticleTags { get; set; }
}
