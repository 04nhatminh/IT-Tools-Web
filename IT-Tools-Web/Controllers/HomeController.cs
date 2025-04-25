using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using IT_Tools_Web.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace IT_Tools_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult TokenGenerator()
        {
            return View();
        }

        public IActionResult RomanNumberConvert()
        {
            return View();
        }

        public IActionResult Bcrypt()
        {
            return View();
        }
        public IActionResult HashText()
        {
            return View();
        }

        public IActionResult ULIDGenerator()
        {
            return View();
        }

        public IActionResult IntegerBaseConverter()
        {
            return View();
        }

        public IActionResult RomanNumeralConverter()
        {
            return View();
        }

        public IActionResult EncodeDecodeURL()
        {
            return View();
        }
        public IActionResult EscapeHTML()
        {
            return View();
        }

        public IActionResult DeviceInformation()
        {
            return View();
        }

        public IActionResult BasicAuthGenerator()
        {
            return View();
        }
        



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

