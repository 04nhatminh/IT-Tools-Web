using System;
using System.Diagnostics;
using System.Net;
using IT_Tools_Web.Data;
using IT_Tools_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Tools_Web.Controllers
{
    public class HomeController : Controller
    {
        // Test database

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult TestDb()
        {
            var account = new Account
            {
                Username = "tester",
                Email = "tester@example.com",
                Password = "123456",
                Type = "user"
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Content("Data inserted successfully!");
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
        public IActionResult ColorConverter()
        {
            return View();
        }
        public IActionResult CaseConverter()
        {
            return View();
        }
        public IActionResult TextToNATOAlphabet()
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
        public IActionResult StringObfuscator()
        {
            return View();
        }
        public IActionResult NumeronymGenerator()
        {
            return View();
        }
        public IActionResult IPv4Converter()
        {
            return View();
        }
        public IActionResult IPv4RangeExpander()
        {
            return View();
        }

        public IActionResult EmojiPicker()
        {
            return View();
        }
        public IActionResult PhoneParserAndFormatter()
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
