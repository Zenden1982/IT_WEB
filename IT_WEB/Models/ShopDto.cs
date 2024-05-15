using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
	public class ShopDto
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public string Description { get; set; }
		
		[Required]
		public string address { get; set; }
	}
}
