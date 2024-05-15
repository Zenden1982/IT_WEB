using IT_WEB.Models;
using IT_WEB.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace IT_WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext context;
		private readonly IWebHostEnvironment environment;

		public ProductsController(ApplicationDBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
			this.environment = environment;
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

		//Обновление
		[HttpPost]
		public IActionResult Create(ProductDto productDto)
		{
			if (productDto.ImageFile == null)
			{
				ModelState.AddModelError("ImageFile", "Требуется файл изображения");
			}

			if (productDto.Price == default)
			{
				ModelState.AddModelError("Price", "Поле \"Цена\" является обязательным для заполнения");
			}
			

			if (!ModelState.IsValid)
			{
				return View(productDto);
			}

			// save the image file
			string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			newFileName += Path.GetExtension(productDto.ImageFile!.FileName);
			string imageFullPath = environment.WebRootPath + "/base/" + newFileName;
			using (var stream = System.IO.File.Create(imageFullPath))
			{
				productDto.ImageFile.CopyTo(stream);
			}
			// сохранение новых продуктов в бд
			Product product = new Product()
			{
				Name = productDto.Name,
				Brand = productDto.Brand,
				Category = productDto.Category,
				Price = productDto.Price,
				Description = productDto.Description,
				ImageFileName = newFileName,
				CreatedAt = DateTime.Now,
			};

			context.Products.Add(product);
			context.SaveChanges();
			return RedirectToAction("Index", "Products");
		}

		public IActionResult Edit(int id)
		{
			var product = context.Products.Find(id);
			if (product == null)
			{
				return RedirectToAction("index", "Products");
			}

			var productDto = new ProductDto()
			{
				Name = product.Name,
				Brand = product.Brand,
				Category = product.Category,
				Price = product.Price,
				Description = product.Description,
			};
			ViewData["ProductId"] = product.Id;
			ViewData["ImageFileName"] = product.ImageFileName;
			ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
			
			return View(productDto);
		}

		[HttpPost]
		public IActionResult Edit(int id, ProductDto productDto)
		{
			var product = context.Products.Find(id);
			if (product == null)
			{
				return RedirectToAction("Index", "Products");
			}
			if (!ModelState.IsValid)
			{
				ViewData["ProductId"] = product.Id;
				ViewData["ImageFileName"] = product.ImageFileName;
				ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");

				return View(productDto);
			}
			// обновляем изображение, если загружено новое
			string newFileName = product.ImageFileName;
			if (productDto.ImageFile != null)
			{
				newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(productDto.ImageFile.FileName);
				string imageFullPath = environment.WebRootPath + "/base/" + newFileName;
				using (var stream = System.IO.File.Create(imageFullPath))
				{
					productDto.ImageFile.CopyTo(stream);
				}
				// удаление старого изоражения
				string oldImageFullPath = environment.WebRootPath + "/base/" + product.ImageFileName;
				System.IO.File.Delete(oldImageFullPath);
			}
			//обновляем продукт в бд
			product.Name = productDto.Name;
			product.Brand = productDto.Brand;
			product.Category = productDto.Category;
			product.Price = productDto.Price;
			product.Description = productDto.Description;
			product.ImageFileName = newFileName;
			context.SaveChanges();
			return RedirectToAction("Index","Products");
		}
	}
}
