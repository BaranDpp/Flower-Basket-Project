using FlowerBasketProject.Models.Context;
using FlowerBasketProject.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class ShopController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ShopController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var products = _context.Products.Include(x => x.Images).ToList();
        return View(products);
    }

    public IActionResult AddToCart(int id)
    {
        var product = _context.Products.Include(x => x.Images).FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var cart = GetCart();
        var cartItem = cart.Products.FirstOrDefault(p => p.ProductId == id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            cart.Products.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = 1,
                Price = product.Price
            });
        }
        SaveCart(cart);

        TempData["AlertMessage"] = $"{product.Name} has been added to your cart!";

        return RedirectToAction("Index");
    }

    public IActionResult Cart()
    {
        var cart = GetCart();
        return View(cart);
    }

    public async Task<IActionResult> Checkout()
    {
        var cart = GetCart();
        var user = await _userManager.GetUserAsync(User); // Get the current user
        if (user == null)
        {
            return Unauthorized();
        }

        var order = new Order
        {
            OrderDate = DateTime.Now,
            UserId = user.Id, // Use the user's ID
            OrderDetails = cart.Products.Select(p => new OrderDetail
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        ClearCart();

        return RedirectToAction("OrderSuccess");
    }

    public IActionResult OrderSuccess()
    {
        return View();
    }

    private Cart GetCart()
    {
        var cart = HttpContext.Session.GetString("Cart");
        if (cart == null)
        {
            return new Cart();
        }

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        return JsonConvert.DeserializeObject<Cart>(cart, settings);
    }

    private void SaveCart(Cart cart)
    {
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, settings));
    }

    private void ClearCart()
    {
        HttpContext.Session.Remove("Cart");
    }
}