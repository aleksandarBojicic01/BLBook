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
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly AppDbContext _db;
		public ProductRepository(AppDbContext db) : base(db)
		{
			this._db = db;
		}

		public void Update(Product product)
		{
			_db.Update(product);
		}
	}
}
