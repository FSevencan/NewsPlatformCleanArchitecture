using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.SubCategories.Commands.Create;

public class CreatedSubCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
  
}