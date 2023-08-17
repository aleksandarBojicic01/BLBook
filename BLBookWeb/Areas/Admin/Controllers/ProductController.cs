using BLBook.DataAccess.Repository;
using BLBook.DataAccess.Repository.IRepository;
using BLBook.Models;
using BLBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BLBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _uow;
		public ProductController(IUnitOfWork ouw)
		{
			_uow = ouw;
		}
		public IActionResult Index()
		{
			List<Product> productsFromDb = _uow.ProductRepository.GetAll().ToList();
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
		public IActionResult Upsert(ProductVM obj, IFormFile file)
		{
			if (ModelState.IsValid)
			{
				_uow.ProductRepository.Add(obj.Product);
				_uow.Save();
				TempData["success"] = "Product created successfully!";
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

        public IActionResult Delete(int? id)
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
        public IActionResult Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductRepository.Delete(product);
                _uow.Save();
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
