using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
	public class ProductDto
	{
		[Required(ErrorMessage = "Поле \"Название\" является обязательным для заполнения.")]
		[MaxLength(200)]
		public string Name { get; set; } = "";

		[Required(ErrorMessage = "Поле \"Бренд\" является обязательным для заполнения.")]
		[MaxLength(100)]
		public string Brand { get; set; } = "";

		[Required(ErrorMessage = "Поле \"Категория\" является обязательным для заполнения.")]
		[MaxLength(100)]
		public string Category { get; set; } = "";

		[Required(ErrorMessage = "Поле \"Цена\" является обязательным для заполнения.")]
		public decimal? Price { get; set; }

		[Required(ErrorMessage = "Поле \"Описание\" является обязательным для заполнения.")]
		public string Description { get; set; } = "";

		public IFormFile? ImageFile { get; set; }
	}
}
