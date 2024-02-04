using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Articles.Queries.GetMostLikedArticles;
public class GetMostLikedArticleListDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string SubCategoryName { get; set; }
    public string Slug { get; set; }
    public int TotalLikes { get; set; }
    public DateTime? CreatedDate { get; set; }
}