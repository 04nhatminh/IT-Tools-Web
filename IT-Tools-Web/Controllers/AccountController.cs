using IT_Tools_Web.Business.Services;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Index()
    {
        var accounts = _accountService.GetAllAccounts();
        return View(accounts);
    }
}
