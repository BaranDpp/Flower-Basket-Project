using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowerBasketProject.Models.Entity;
using System.Threading.Tasks;
using FlowerBasketProject.Models;

[Authorize(Roles = "Admin")]
public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var model = new ProfileViewModel
        {
            Username = user.UserName,
            Email = user.Email,
            FullName = user.FullName
        };

        return View(model);
    }
}