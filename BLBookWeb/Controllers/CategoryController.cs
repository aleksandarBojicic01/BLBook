﻿using Microsoft.AspNetCore.Mvc;

namespace BLBookWeb.Controllers
{
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
