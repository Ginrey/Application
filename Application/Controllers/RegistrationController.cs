using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class RegistrationController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}