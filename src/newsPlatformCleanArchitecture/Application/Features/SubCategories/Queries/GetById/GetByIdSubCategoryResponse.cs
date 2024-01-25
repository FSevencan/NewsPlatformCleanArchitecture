using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.SubCategories.Queries.GetById;

public class GetByIdSubCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}