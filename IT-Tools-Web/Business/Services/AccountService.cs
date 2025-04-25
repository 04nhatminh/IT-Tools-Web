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

    // Thêm các hàm xử lý khác như Add, Update, Delete nếu cần
}
