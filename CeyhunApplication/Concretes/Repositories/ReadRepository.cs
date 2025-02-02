﻿using CeyhunApplication.Abstractions.Repositories;
using CeyhunApplication.Data;
using CeyhunApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CeyhunApplication.Concretes.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly ApplicationDbContext _context;

    public ReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();


    public IQueryable<T> GetAll(bool asNoTracking = false, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        IQueryable<T> entity = query;
        return entity;
    }

    public T? GetById(int? id, bool asNoTracking = false, params string[] includes)
    {
        var queryable = Table.AsQueryable();

        if (asNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        foreach (string include in includes)
        {
            queryable = queryable.Include(include);
        }

        T? entity = queryable.FirstOrDefault(e => e.Id == id);
        return entity;

    }
}
