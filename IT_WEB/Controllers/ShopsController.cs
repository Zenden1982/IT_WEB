using IT_WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_WEB.Controllers
{
    public class ShopsController : Controller
    {

        private readonly ApplicationDBContext context;
        public ShopsController(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult IndexShop()
        {
            var shops = context.Shops.ToList();
            return View(shops);
        }
    }
}
