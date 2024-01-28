using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.ColumnArticles.Queries.GetList;

public class GetListColumnArticleListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ColumnistId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string FeaturedImage { get; set; }
    
}