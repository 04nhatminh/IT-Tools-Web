using IT_Tools_Web.Business.Services;
using IT_Tools_Web.DataAccess;
using IT_Tools_Web.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace IT_Tools_Web.Controllers
{
    public class ToolController : Controller
    {
        private readonly ToolService _toolService;
        private readonly ToolLoaderService _toolLoaderService;

        public ToolController(ToolService toolService, ToolLoaderService toolLoaderService)
        {
            _toolService = toolService;
            _toolLoaderService = toolLoaderService;
        }

        [HttpGet]
        public IActionResult AddTool()
        {
            // Chỉ cho phép admin truy cập
            if (HttpContext.Session.GetString("UserType") != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddTool(IFormFile file)
        {
            // Chỉ cho phép admin truy cập
            if (HttpContext.Session.GetString("UserType") != "admin")
            {
                return RedirectToAction("Login", "Account");
            }

            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a valid .cshtml file.");
                return View();
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".cshtml")
            {
                ModelState.AddModelError("", "Only .cshtml files are allowed.");
                return View();
            }

            // Lưu file vào thư mục Tools/NewTools
            var toolsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "NewTools");
            if (!Directory.Exists(toolsDirectory))
            {
                Directory.CreateDirectory(toolsDirectory);
            }

            var filePath = Path.Combine(toolsDirectory, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Gọi lại ToolLoaderService để cập nhật tools
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Tools");
            _toolLoaderService.LoadToolsFromDirectory(baseDirectory);

            return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chính hoặc danh sách tools
        }

        public class AddToFavoriteRequest
        {
            public int ToolId { get; set; }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult AddToFavorite([FromBody] AddToFavoriteRequest request)
        {
            int toolId = request.ToolId;
            Debug.WriteLine($"ToolID:{toolId}");
            // Logic xử lý
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized("You must be logged in to add a tool to favorites.");
            }

            var tool = _toolService.GetToolById(toolId);
            if (tool == null)
            {
                return NotFound("Tool not found.");
            }

            var isAlreadyFavorite = _toolService.IsToolFavorite((int)userId, toolId);
            if (isAlreadyFavorite)
            {
                return BadRequest("Tool is already in your favorites.");
            }

            _toolService.AddToFavorite((int)userId, toolId);
            return Ok("Tool added to favorites successfully.");
        }
    }
}
