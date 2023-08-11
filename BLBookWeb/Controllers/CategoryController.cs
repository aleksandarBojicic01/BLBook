﻿using BLBookWeb.Data;
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
	}
}
