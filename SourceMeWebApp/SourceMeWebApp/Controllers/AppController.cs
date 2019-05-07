using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SourceMeWebApp.Controllers
{
    public class AppController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Feed()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View("Categories");
        }
    }
}
