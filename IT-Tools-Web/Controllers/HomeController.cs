using System.Diagnostics;
using IT_Tools_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Tools_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            ViewBag.HideLayout = true;
            return View();
        }
        public IActionResult Signup()
        {
            ViewBag.HideLayout = true;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TokenGenerator()
        {
            return View();
        }
        public IActionResult HashText()
        {
            return View();
        }

        public IActionResult Chronometer()
        {
            return View();
        }
        public IActionResult TemperatureConverter()
        {
            return View();
        }
        public IActionResult MathEvaluator()
        {
            return View();
        }
        public IActionResult PercentageCalculator()
        {
            return View();
        }
        public IActionResult LoremIpsumGenerator()
        {
            return View();
        }
        public IActionResult TextStatistics()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
