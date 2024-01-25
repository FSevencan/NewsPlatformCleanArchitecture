using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ColumnArticleRepository : EfRepositoryBase<ColumnArticle, Guid, BaseDbContext>, IColumnArticleRepository
{
    public ColumnArticleRepository(BaseDbContext context) : base(context)
    {
    }
}