using BLBook.DataAccess.Repository.IRepository;
using BLBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLBookWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepo;

		public CategoryController(ICategoryRepository repo)
		{
			_categoryRepo = repo;
		}
		public IActionResult Index()
		{
			List<Category> categories = _categoryRepo.GetAll().ToList();
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
				_categoryRepo.Add(category);
				_categoryRepo.Save();
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

			Category? category = _categoryRepo.GetSingle(u=>u.Id == id);
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
				_categoryRepo.Update(category);
				_categoryRepo.Save();
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

			Category? category = _categoryRepo.GetSingle(u => u.Id == id);
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
				_categoryRepo.Delete(category);
				_categoryRepo.Save();
				TempData["success"] = "Category deleted successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
