using Microsoft.AspNetCore.Mvc;

namespace IT_WEB.Controllers
{
    public class OrdersContoller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
