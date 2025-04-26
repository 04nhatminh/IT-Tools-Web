using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using IT_Tools_Web.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;
using IT_Tools_Web.DataAccess;
using IT_Tools_Web.Business.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IT_Tools_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ToolService _toolService;
        public HomeController(ToolService toolService)
        {
            _toolService = toolService;
        }
        public IActionResult Index(string searchQuery = null)
        {
            // Lấy UserType từ session
            var userType = HttpContext.Session.GetString("UserType") ?? "anonymous";

            // Lấy danh sách tools phù hợp với người dùng
            var tools = _toolService.GetToolsForUser(userType);

            // Nếu có từ khóa tìm kiếm, lọc danh sách tools
            if (!string.IsNullOrEmpty(searchQuery))
            {
                tools = tools.Where(t => t.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(tools);
        }

        public IActionResult LoadTool(string path)
        {
            // Chuẩn hóa đường dẫn
            var normalizedPath = path.Replace("\\", "/");

            // Kiểm tra xem tệp có tồn tại không
            var toolsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Tools");
            var fullPath = Path.Combine(toolsDirectory, normalizedPath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound(); // Trả về 404 nếu tệp không tồn tại
            }

            var tool = _toolService.GetToolByPath(path);
            if (tool == null)
            {
                return NotFound(); // Trả về 404 nếu tool không tồn tại
            }

            ViewData["ToolId"] = tool.Id;

            return View($"~/Tools/{normalizedPath}");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

