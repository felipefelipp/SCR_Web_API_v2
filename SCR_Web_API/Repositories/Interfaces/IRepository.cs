using SCR_Web_API.Pagination;
using System.Linq.Expressions;
using X.PagedList;

namespace SCR_Web_API.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IPagedList<T>> GetAll(Parameters parameters);
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    T? Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
