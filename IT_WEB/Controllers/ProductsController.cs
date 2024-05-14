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


	}
}
