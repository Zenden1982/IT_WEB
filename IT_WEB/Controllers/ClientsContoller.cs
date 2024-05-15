using IT_WEB.Models;
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
        public IActionResult Create()
        {
            return View();
        }

		//Обновление
		[HttpPost]
		public IActionResult Create(ClientDto clientDto)
		{
			if (!ModelState.IsValid)
			{
				return View(clientDto);
			}
			// сохранение нового клиента в базе данных
			Client client = new Client()
			{
				Name = clientDto.Name,
				Number = clientDto.Number
			};
			context.Clients.Add(client);
			context.SaveChanges();
			return RedirectToAction("IndexClients", "ClientsContoller");
		}

        public IActionResult Edit(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("IndexClients");
            }

            var clientDto = new ClientDto()
            {
                Name = client.Name,
                Number = client.Number
            };
            ViewData["ClientId"] = client.Id;

            return View(clientDto);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, ClientDto clientDto)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("IndexClients", "ClientsContoller");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ClientId"] = client.Id;

                return View(clientDto);
            }
            // обновляем клиента в базе данных
            client.Name = clientDto.Name;
            client.Number = clientDto.Number;
            context.SaveChanges();
            return RedirectToAction("IndexClients", "ClientsContoller");
        }
        public IActionResult Delete(int id)
        {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                return RedirectToAction("IndexClients", "ClientsContoller");
            }
            context.Clients.Remove(client);
            context.SaveChanges();
            return RedirectToAction("IndexClients", "ClientsContoller");
        }


    }
}
