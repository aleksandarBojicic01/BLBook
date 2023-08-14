using System;
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
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}

		// moguc problem 5:10:00 -------
		// prepravljeno
		public T GetSingle(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
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
