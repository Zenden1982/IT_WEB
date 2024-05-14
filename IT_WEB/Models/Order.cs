namespace IT_WEB.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ShopId { get; set; }
        public int ProductId { get; set; }
    }
}
