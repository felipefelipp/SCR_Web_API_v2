using Microsoft.EntityFrameworkCore;
using SCR_Web_API.Context;
using SCR_Web_API.Pagination;
using SCR_Web_API.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SCR_Web_API.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public PagedList<T> GetAll(Parameters parameters)
    {
        var retorno = _context.Set<T>().AsNoTracking().AsQueryable();
        var retornoOrdenado = PagedList<T>.ToPagedList(retorno, parameters.PageNumber, parameters.PageSize);
        return retornoOrdenado;
    }
    public T? Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Remove(entity);
        return entity;
    }


    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
    }

}
