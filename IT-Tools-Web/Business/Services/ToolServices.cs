using IT_Tools_Web.DataAccess;
using IT_Tools_Web.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

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

    public List<Tool> GetToolsForUser(string userType, string searchQuery = null)
    {
        var query = _context.Tools.AsQueryable();

        if (userType != "admin" && userType != "premium")
        {
            query = query.Where(t => !t.IsPremium); // Lọc tools không phải premium
        }

        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(t => t.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
        }

        return query.Where(t => t.IsEnabled).ToList();
    }

    public Tool GetToolById(int toolId)
    {
        return _context.Tools.FirstOrDefault(t => t.Id == toolId);
    }

    public Tool GetToolByPath(string path)
    {
        return _context.Tools.FirstOrDefault(t => t.Path == path);
    }

    public bool IsToolFavorite(int userId, int toolId)
    {
        return _context.Favorites.Any(f => f.AccountId == userId && f.ToolId == toolId);
    }

    public void AddToFavorite(int userId, int toolId)
    {
        var favorite = new Favorite
        {
            AccountId = userId,
            ToolId = toolId
        };

        _context.Favorites.Add(favorite);
        _context.SaveChanges();
    }

    public List<Tool> GetFavoriteToolsForUser(int userId)
    {
        return _context.Favorites
            .Where(f => f.AccountId == userId)
            .Select(f => f.Tool)
            .Where(t => t.IsEnabled)
            .ToList();
    }
}
