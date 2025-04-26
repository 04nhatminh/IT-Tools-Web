using IT_Tools_Web.DataAccess;
using IT_Tools_Web.DataAccess.Models;

namespace IT_Tools_Web.Business.Services;

public class ToolService
{
    private readonly AppDbContext _context;

    public ToolService(AppDbContext context)
    {
        _context = context;
    }

    public List<Tool> GetAllTools()
    {
        return _context.Tools.ToList();
    }

    public List<Tool> GetEnabledTools()
    {
        return _context.Tools.Where(t => t.IsEnabled).ToList();
    }
}
