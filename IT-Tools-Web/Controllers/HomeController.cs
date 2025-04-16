using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
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

        [HttpGet]
        public IActionResult HashText()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HashText(string inputText, string algorithm)
        {
            try
            {
                string hashedResult = ComputeHash(inputText, algorithm);
                ViewBag.Result = hashedResult;
            }
            catch (NotSupportedException ex)
            {
                ViewBag.Result = $"❌ Algorithm '{algorithm}' is not supported.";
            }

            return View();
        }

        private string ComputeHash(string input, string algorithm)
        {
            using HashAlgorithm hasher = algorithm.ToUpper() switch
            {
                "MD5" => MD5.Create(),
                "SHA1" => SHA1.Create(),
                "SHA256" => SHA256.Create(),
                "SHA384" => SHA384.Create(),
                "SHA512" => SHA512.Create(),
                // Những thuật toán chưa được hỗ trợ mặc định
                "SHA224" or "SHA3" or "RIPEMD160" => throw new NotSupportedException(),
                _ => throw new NotSupportedException()
            };

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = hasher.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
