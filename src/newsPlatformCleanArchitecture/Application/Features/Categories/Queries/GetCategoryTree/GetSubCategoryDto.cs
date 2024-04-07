using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetCategoryTree;
public class GetSubCategoryDto :IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
