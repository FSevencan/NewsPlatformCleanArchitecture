using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISubCategoryRepository : IAsyncRepository<SubCategory, Guid>, IRepository<SubCategory, Guid>
{
}