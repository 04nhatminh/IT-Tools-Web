using System.Diagnostics;
using IT_Tools_Web.Business.Services;
using IT_Tools_Web.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
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

        _accountService.AddAccount(account);

        return Content("Data inserted successfully!");
    }

    [HttpGet]
    public IActionResult Signup()
    {
        ViewBag.HideLayout = true;
        return View();
    }

    [HttpPost]
    public IActionResult Signup(Account account)
    {
        if (ModelState.IsValid)
        {
            var isRegistered = _accountService.RegisterAccount(account);
            if (isRegistered)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "The username or email is already in use. Please try again.");
            }
        }
        else
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Debug.WriteLine(error.ErrorMessage);
            }
        }

        ViewBag.HideLayout = true;
        return View(account);
    }

    [HttpGet]
    public IActionResult Login()
    {
        ViewBag.HideLayout = true;
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Username and Password are required.");
            ViewBag.HideLayout = true;
            return View();
        }

        var account = _accountService.Authenticate(username, password);
        if (account != null)
        {
            // Lưu thông tin đăng nhập vào session hoặc cookie
            HttpContext.Session.SetString("Username", account.Username);
            HttpContext.Session.SetString("UserType", account.Type);
            HttpContext.Session.SetInt32("UserId", account.Id); 

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Invalid username or password.");
        }

        ViewBag.HideLayout = true;
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Xóa tất cả session
        return RedirectToAction("Login");
    }
}
