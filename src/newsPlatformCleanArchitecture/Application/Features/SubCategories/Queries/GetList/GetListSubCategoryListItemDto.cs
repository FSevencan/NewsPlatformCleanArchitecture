using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.SubCategories.Queries.GetList;

public class GetListSubCategoryListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
   
}