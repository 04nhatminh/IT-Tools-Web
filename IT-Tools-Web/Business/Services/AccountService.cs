using IT_Tools_Web.DataAccess;
using IT_Tools_Web.DataAccess.Models;

namespace IT_Tools_Web.Business.Services;

public class AccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public List<Account> GetAllAccounts()
    {
        return _context.Accounts.ToList();
    }

    public Account GetAccountById(int id)
    {
        return _context.Accounts.FirstOrDefault(a => a.Id == id);
    }

    public void AddAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
    }

    public void DeleteAccount(int id)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }

    public bool IsEmailOrUsernameTaken(string email, string username)
    {
        return _context.Accounts.Any(a => a.Email == email || a.Username == username);
    }

    public bool RegisterAccount(Account account)
    {
        if (IsEmailOrUsernameTaken(account.Email, account.Username))
        {
            return false;
        }

        AddAccount(account);
        return true;
    }
}
