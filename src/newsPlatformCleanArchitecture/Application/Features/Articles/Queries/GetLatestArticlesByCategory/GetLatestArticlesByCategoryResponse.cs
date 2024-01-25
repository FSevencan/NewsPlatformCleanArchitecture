using Application.Features.Articles.Queries.GetList;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Articles.Queries.GetLatestArticlesByCategory;
public class GetLatestArticlesByCategoryResponse : IResponse
{
    public IEnumerable<GetLastArticleByCategoryItemDto> items { get; set; }
}
