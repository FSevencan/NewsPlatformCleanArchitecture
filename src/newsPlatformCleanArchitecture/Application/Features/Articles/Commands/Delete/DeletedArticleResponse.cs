using Core.Application.Responses;

namespace Application.Features.Articles.Commands.Delete;

public class DeletedArticleResponse : IResponse
{
    public Guid Id { get; set; }
}