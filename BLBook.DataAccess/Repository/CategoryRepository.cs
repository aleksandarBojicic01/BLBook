using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLBook.DataAccess.Data;
using BLBook.DataAccess.Repository.IRepository;
using BLBook.Models;

namespace BLBook.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly AppDbContext _db;
		public CategoryRepository(AppDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
