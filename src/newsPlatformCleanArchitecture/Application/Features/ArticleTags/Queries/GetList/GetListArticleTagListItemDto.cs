using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.ArticleTags.Queries.GetList;

public class GetListArticleTagListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public Guid TagId { get; set; }
   
}