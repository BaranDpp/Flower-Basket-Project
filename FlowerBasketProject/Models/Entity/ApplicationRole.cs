using Microsoft.AspNetCore.Identity;

namespace FlowerBasketProject.Models.Entity
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}