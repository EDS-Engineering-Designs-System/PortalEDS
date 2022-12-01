using Microsoft.AspNetCore.Mvc;

namespace bim360issues.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
