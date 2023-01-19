﻿using EduHome.Core.Interfaces;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> FindAll()
    {
        return Table.AsQueryable().AsNoTracking();
    }
    //fgdfg
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
            return Table.Where(expression);
        return Table.Where(expression).AsNoTracking();
    }
    public async Task<T?> FindByIdAsync(int id)
    {
            return  await Table.FindAsync(id);
    }


    public async Task CreateAsync(T entity)
    {
         await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }
    public void Delete(T entity)
    {
      Table.Remove(entity);

    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();  
    }

    Task<T> IRepository<T>.CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
