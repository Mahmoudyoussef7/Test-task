using DAL.Interfaces;
using EFL.Data;
using EFL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected ClientsDbContext _context;
    protected DbSet<T> DbSet { get; set; }

    public BaseRepository(ClientsDbContext context)
    {
        _context = context;
        DbSet = _context.Set<T>();
    }

    public virtual T Add(T entity)
    {
        DbSet.Add(entity);
        return entity;
    }

    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual IQueryable<T> GetAll()
    {
        return DbSet.AsQueryable();
    }

    public virtual T GetById(int id)
    {
        return DbSet.Find(id);
    }

    public virtual IQueryable<T> GetByExpression(Expression<Func<T, bool>> criteria)
    {
        return DbSet.Where(criteria);
    }

    public virtual T Update(T entity)
    {
        DbSet.Update(entity);
        return entity;
    }

    public IQueryable<T> Search(Expression<Func<T, bool>> criteria)
    {
        return DbSet.Where(criteria);
    }
}
