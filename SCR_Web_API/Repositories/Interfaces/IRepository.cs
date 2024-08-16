using SCR_Web_API.Pagination;
using System.Linq.Expressions;

namespace SCR_Web_API.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    PagedList<T> GetAll(Parameters parameters);
    T? Get(Expression<Func<T, bool>> predicate);
    T? Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
