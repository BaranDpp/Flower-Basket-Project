using FlowerBasketProject.Models.Entity;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace FlowerBasketProject.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}