using IT_Tools_Web.DataAccess;
using IT_Tools_Web.DataAccess.Models;
using System.IO;

namespace IT_Tools_Web.Business.Services
{
    public class ToolLoaderService
    {
        private readonly AppDbContext _context;

        public ToolLoaderService(AppDbContext context)
        {
            _context = context;
        }

        public void LoadToolsFromDirectory(string baseDirectory)
        {
            var categories = Directory.GetDirectories(baseDirectory);

            foreach (var categoryPath in categories)
            {
                var categoryName = Path.GetFileName(categoryPath);

                var toolFiles = Directory.GetFiles(categoryPath, "*.cshtml");

                foreach (var toolFile in toolFiles)
                {
                    var toolName = Path.GetFileNameWithoutExtension(toolFile);
                    var relativePath = Path.GetRelativePath(baseDirectory, toolFile);

                    if (!_context.Tools.Any(t => t.Path == relativePath))
                    {
                        var tool = new Tool
                        {
                            Name = toolName,
                            Category = categoryName,
                            Path = relativePath,
                            IsEnabled = true,
                            IsPremium = false
                        };

                        _context.Tools.Add(tool);
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
