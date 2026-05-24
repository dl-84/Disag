using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shoootz.Repositories;

internal interface IRepository<T>
    where T : class
{
    Task AddAsync(T entity);

    Task DeleteAsync(T entity);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task SaveAsync();
}
