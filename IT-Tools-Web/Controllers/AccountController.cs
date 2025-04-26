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

    public IActionResult Login()
    {
        ViewBag.HideLayout = true;
        return View();
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
}
