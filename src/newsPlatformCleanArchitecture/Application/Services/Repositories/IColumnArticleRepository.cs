using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IColumnArticleRepository : IAsyncRepository<ColumnArticle, Guid>, IRepository<ColumnArticle, Guid>
{
}