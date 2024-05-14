using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
	public class ProductDto
	{
		[Required, MaxLength(200)]
		public string Name { get; set; } = "";

		[Required, MaxLength(100)]
		public string Brand { get; set; } = "";

		[Required, MaxLength(100)]
		public string Category { get; set; } = "";
		
		[Required]
		public decimal Price { get; set; }
		
		[Required]
		public string Description { get; set; } = "";

		public IFormFile? ImageFile { get; set; }
	}
}
