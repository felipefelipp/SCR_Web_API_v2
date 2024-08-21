using Microsoft.EntityFrameworkCore;
using SCR_Web_API.Context;
using SCR_Web_API.Pagination;
using SCR_Web_API.Repositories.Interfaces;
using System.Linq.Expressions;
using X.PagedList;
using X.PagedList.Extensions;

namespace SCR_Web_API.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    public Repository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IPagedList<T>> GetAll(Parameters parameters)
    {
        var retorno = await _context.Set<T>().AsNoTracking().ToListAsync();
        var retornoQueryable = retorno.AsQueryable();
        var retornoOrdenado = retornoQueryable.ToPagedList(parameters.PageNumber, parameters.PageSize);
        return retornoOrdenado;
    }
    // Create, Update e Delete não precisam ser async pois realizam a operação na memória
    public T? Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }


    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

}
