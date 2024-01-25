using Core.Application.Responses;

namespace Application.Features.ColumnArticles.Commands.Delete;

public class DeletedColumnArticleResponse : IResponse
{
    public Guid Id { get; set; }
}