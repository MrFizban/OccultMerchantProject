using Microsoft.AspNetCore.Mvc;

namespace Esperimenti
{
    public class Controlller : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}