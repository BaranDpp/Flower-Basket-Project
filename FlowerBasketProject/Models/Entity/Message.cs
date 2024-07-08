namespace FlowerBasketProject.Models.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}