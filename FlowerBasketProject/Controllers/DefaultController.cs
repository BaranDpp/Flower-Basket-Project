using Microsoft.AspNetCore.Mvc;

namespace FlowerBasketProject.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
