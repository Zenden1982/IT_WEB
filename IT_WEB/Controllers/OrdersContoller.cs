using IT_WEB.Models;
using IT_WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_WEB.Controllers
{
    public class OrdersContoller : Controller
    {
        private readonly ApplicationDBContext context;

        public OrdersContoller(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult IndexOrders()
        {
            var orders = context.Orders.ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
			ViewBag.Clients = context.Clients.ToList();
			ViewBag.Products = context.Products.ToList();
			ViewBag.Shops = context.Shops.ToList();
			return View();
        }

        [HttpPost]
        public IActionResult Create(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                // Если модель не прошла валидацию, вернуть представление снова с моделью для исправления ошибок
                return View(orderDto);
            }

            // Создать новый объект заказа на основе данных из DTO
            Order order = new Order()
            {
                ClientId = orderDto.ClientId,
                ProductId = orderDto.ProductId,
                ShopId = orderDto.ShopId
            };

            // Добавить заказ в контекст данных
            context.Orders.Add(order);

            // Сохранить изменения в базе данных
            context.SaveChanges();

            // Перенаправить пользователя на страницу со списком заказов
            return RedirectToAction("IndexOrders", "OrdersContoller");
        }
        public IActionResult Edit(int id)
        {
            // Найти заказ по его идентификатору
            var order = context.Orders.Find(id);

            // Если заказ не найден, перенаправить на страницу со списком заказов
            if (order == null)
            {
                return RedirectToAction("IndexOrders", "OrdersContoller");
            }

            // Получаем данные о клиентах, продуктах и магазинах из базы данных
            var clients = context.Clients.ToList();
            var products = context.Products.ToList();
            var shops = context.Shops.ToList();

            // Передаем данные в представление через ViewBag
            ViewBag.Clients = clients;
            ViewBag.Products = products;
            ViewBag.Shops = shops;
            
            // Создать DTO объект заказа для передачи данных в представление
            var orderDto = new OrderDto()
            {
                ClientId = order.ClientId,
                ProductId = order.ProductId,
                ShopId = order.ShopId
            };

            // Передать идентификатор заказа в представление через ViewData
            ViewData["OrderId"] = order.Id;

            // Отобразить представление для редактирования заказа с данными из DTO
            return View(orderDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, OrderDto orderDto)
        {
            // Найти заказ по его идентификатору
            var order = context.Orders.Find(id);

            // Если заказ не найден, перенаправить на страницу со списком заказов
            if (order == null)
            {
                return RedirectToAction("IndexOrders", "OrdersContoller");
            }

            // Если модель не прошла валидацию, вернуть представление снова с моделью для исправления ошибок
            if (!ModelState.IsValid)
            {
                ViewData["OrderId"] = order.Id;
                return View(orderDto);
            }

            // Обновить данные заказа на основе данных из DTO
            order.ClientId = orderDto.ClientId;
            order.ProductId = orderDto.ProductId;
            order.ShopId = orderDto.ShopId;

            // Сохранить изменения в базе данных
            context.SaveChanges();

            // Перенаправить пользователя на страницу со списком заказов
            return RedirectToAction("IndexOrders", "OrdersContoller");
        }
        public IActionResult Delete(int id)
        {
            // Найти заказ по его идентификатору
            var order = context.Orders.Find(id);

            // Если заказ не найден, перенаправить на страницу со списком заказов
            if (order == null)
            {
                return RedirectToAction("IndexOrders", "OrdersContoller");
            }

            // Удалить заказ из контекста данных
            context.Orders.Remove(order);

            // Сохранить изменения в базе данных
            context.SaveChanges();

            // Перенаправить пользователя на страницу со списком заказов
            return RedirectToAction("IndexOrders", "OrdersContoller");
        }


    }
}
