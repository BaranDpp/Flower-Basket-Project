using static System.Net.Mime.MediaTypeNames;

namespace FlowerBasketProject.Models.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsNew { get; set; }
        public Category? Category { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}