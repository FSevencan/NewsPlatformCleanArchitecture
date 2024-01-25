using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.SubCategories.Commands.Update;

public class UpdatedSubCategoryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
   
}