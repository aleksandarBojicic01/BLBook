using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLBook.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(string? includeProperties = null);
		T GetSingle(Expression<Func<T, bool>> filter, string? includeProperties = null);
		void Add(T obj);
		void Delete(T obj);
		void DeleteRange(IEnumerable<T> objects);
	}
}
