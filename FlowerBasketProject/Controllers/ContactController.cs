using Microsoft.AspNetCore.Mvc;

namespace FlowerBasketProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
