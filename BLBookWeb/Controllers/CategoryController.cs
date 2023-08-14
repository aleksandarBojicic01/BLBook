using BLBookWeb.Data;
using BLBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLBookWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly AppDbContext _db;

		public CategoryController(AppDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Category> categories = _db.Categories.ToList();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Add(category);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(category);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		public IActionResult Delete(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Remove(category);
				_db.SaveChanges();
				TempData["success"] = "Category deleted successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
