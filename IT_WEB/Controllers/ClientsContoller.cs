using IT_WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_WEB.Controllers
{
    public class ClientsContoller : Controller
    {
        private readonly ApplicationDBContext context;
        public ClientsContoller(ApplicationDBContext context)
        {
            this.context = context;
        }
        public IActionResult IndexClients()
        {
            var clients = context.Clients.ToList();
            return View(clients);
        }
    }
}
