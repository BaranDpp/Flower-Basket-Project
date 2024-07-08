namespace FlowerBasketProject.Models.Entity
{
    public class Cart
    {
        public List<CartItem> Products { get; set; } = new List<CartItem>();
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}