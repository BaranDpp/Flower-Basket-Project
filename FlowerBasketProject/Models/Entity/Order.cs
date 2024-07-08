namespace FlowerBasketProject.Models.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}