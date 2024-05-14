using IT_WEB.Models;
using IT_WEB.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
