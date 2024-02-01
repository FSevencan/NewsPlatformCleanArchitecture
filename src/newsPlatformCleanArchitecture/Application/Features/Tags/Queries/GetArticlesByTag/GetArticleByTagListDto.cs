using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tags.Queries.GetArticlesByTag;
public class GetArticleByTagListDto : IDto
{
    public Guid Id { get; set; }
    public string? SubCategoryName { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? FeaturedImage { get; set; }
    public DateTime? CreatedDate { get; set; }


}
