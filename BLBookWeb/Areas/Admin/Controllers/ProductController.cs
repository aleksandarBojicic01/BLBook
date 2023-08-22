using BLBook.DataAccess.Repository;
using BLBook.DataAccess.Repository.IRepository;
using BLBook.Models;
using BLBook.Models.ViewModels;
using BLBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BLBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
	{
		private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork ouw, IWebHostEnvironment web)
		{
            _webHostEnvironment = web;
			_uow = ouw;
		}
		public IActionResult Index()
		{
			List<Product> productsFromDb = _uow.ProductRepository.GetAll("Category").ToList();
			return View(productsFromDb);
		}

		public IActionResult Upsert(int? id)
		{
            IEnumerable<SelectListItem> CategoryList = _uow.CategoryRepository.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _uow.ProductRepository.GetSingle(u => u.Id == id);
                return View(productVM);
            }
            
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM obj, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        string oldImg = Path.Combine(wwwRootPath, obj.Product.ImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImg))
                        {
                            System.IO.File.Delete(oldImg);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (obj.Product.Id == 0)
                {
                    _uow.ProductRepository.Add(obj.Product);
                    TempData["success"] = "Product created successfully!";
                } 
                else
                {
                    _uow.ProductRepository.Update(obj.Product);
                    TempData["success"] = "Product updated successfully!";
                }

				_uow.Save();
				return RedirectToAction("Index");
			}
            else
            {
                obj.CategoryList = _uow.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
		}

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _uow.ProductRepository.GetSingle(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductRepository.Update(product);
                _uow.Save();
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        // stara logika
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? product = _uow.ProductRepository.GetSingle(u => u.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}
        
        //[HttpPost]
        //public IActionResult Delete(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _uow.ProductRepository.Delete(product);
        //        _uow.Save();
        //        TempData["success"] = "Product deleted successfully!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        #region API CALLS

        public IActionResult GetAll()
        {
            List<Product> productsFromDb = _uow.ProductRepository.GetAll("Category").ToList();
            return Json( new { data = productsFromDb });
        }

    
        public IActionResult Delete(int? id)
        {
            Product? productToBeDeleted = _uow.ProductRepository.GetSingle(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _uow.ProductRepository.Delete(productToBeDeleted);
            _uow.Save();

            return Json(new { success = true, message = "Delete Successful!" });
        }

        #endregion
    }
}
