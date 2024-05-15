using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
    public class OrderDto
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ShopId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
