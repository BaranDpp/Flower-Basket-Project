using FlowerBasketProject.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerBasketProject.Views.Shared.Components.ShopSection
{
    public class ShopSectionViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ShopSectionViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var products = _context.Products.Include(x => x.Images).Take(8).ToList();
            return View(products);
        }
    }
}