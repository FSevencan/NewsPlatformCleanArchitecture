using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ColumnArticles.Queries.GetBySlug;
public class GetColumnArticleBySlugResponse : IResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? FeaturedImage { get; set; }
    public DateTime CreatedDate { get; set; }

}