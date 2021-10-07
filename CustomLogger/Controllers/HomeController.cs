using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomLogger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Route("/readme")]
        public IActionResult Readme() => View();
    }
}
