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
			// iako update radi normalno i bez ovoga ovdje je prikaz manuelnog mapiranja, prednost repository patterna
			Product? productFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
			if (productFromDb != null) 
			{
				productFromDb.Title = product.Title;
				productFromDb.Description = product.Description;
				productFromDb.CategoryId = product.CategoryId;
				productFromDb.ListPrice = product.ListPrice;
				productFromDb.Price = product.Price;
				productFromDb.Price50 = product.Price50;
				productFromDb.Price100 = product.Price100;
				productFromDb.ISBN = product.ISBN;
				productFromDb.Author = product.Author;
				if (product.ImageUrl != null)
				{
					productFromDb.ImageUrl = product.ImageUrl;
				}
			}
		}
	}
}
