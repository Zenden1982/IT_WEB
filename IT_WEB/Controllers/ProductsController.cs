using IT_WEB.Models;
using IT_WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext context;
        public ProductsController(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var products = context.Products.OrderByDescending(p => p.Id).ToList();
            return View(products);
        }

		public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(ProductDto productDto)
		{
			if (productDto.ImageFile == null)
            {
				ModelState.AddModelError("ImageFile", "Требуется файл изображения");
			}
			
			if (!ModelState.IsValid)
            {
                return View(productDto);    
            }
			
            return RedirectToAction("Index", "Products");
		}
	}
}
