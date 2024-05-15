using IT_WEB.Models;
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
        public IActionResult Create()
        {
            return View();
        }
        
        //Обновление
        [HttpPost]
        public IActionResult Create(ShopDto shopDto)
        {
            if (!ModelState.IsValid)
            {
                return View(shopDto);
            }
            // сохранение новых продуктов в бд
            Shop shop = new Shop()
            {
                Name = shopDto.Name,
                Description = shopDto.Description,
                address = shopDto.address,
              
            };
            context.Shops.Add(shop);
            context.SaveChanges();
            return RedirectToAction("IndexShop", "Shops");
        }
    }
}
