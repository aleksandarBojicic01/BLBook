﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLBook.DataAccess.Data;
using BLBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BLBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(AppDbContext db)
		{
			_db = db;
			dbSet = _db.Set<T>();
			_db.Products.Include(u => u.Category);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var prop in includeProperties
					.Split(',', StringSplitOptions.RemoveEmptyEntries)) 
				{
					query = query.Include(prop);
				}
			}
			return query.ToList();
		}

		public T GetSingle(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var prop in includeProperties
                    .Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.FirstOrDefault();
		}

		public void Add(T obj)
		{
			dbSet.Add(obj);
		}

		public void Delete(T obj)
		{
			dbSet.Remove(obj);
		}

		public void DeleteRange(IEnumerable<T> objects)
		{
			dbSet.RemoveRange(objects);
		}
	}
}
