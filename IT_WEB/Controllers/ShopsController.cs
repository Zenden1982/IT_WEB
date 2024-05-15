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
        public IActionResult Edit(int id)
        {
            var shop = context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("IndexShop");
            }

            var shopDto = new ShopDto()
            {
                Name = shop.Name,
                Description = shop.Description,
                address = shop.address
            };
            ViewData["ShopId"] = shop.Id;

            return View(shopDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ShopDto shopDto)
        {
            var shop = context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("IndexShop", "Shops");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ShopId"] = shop.Id;

                return View(shopDto);
            }
            // обновляем магазин в базе данных
            shop.Name = shopDto.Name;
            shop.Description = shopDto.Description;
            shop.address = shopDto.address;
            context.SaveChanges();
            return RedirectToAction("IndexShop", "Shops");
        }
        public IActionResult Delete(int id)
        {
            var shop = context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("IndexShop", "Shops");
            }
            context.Shops.Remove(shop);
            context.SaveChanges();
            return RedirectToAction("IndexShop", "Shops");
        }

    }
}
