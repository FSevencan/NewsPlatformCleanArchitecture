using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.ColumnArticles.Commands.Create;

public class CreatedColumnArticleResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ColumnistId { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string FeaturedImage { get; set; }
   
}