using Core.Application.Responses;

namespace Application.Features.SubCategories.Commands.Delete;

public class DeletedSubCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
}