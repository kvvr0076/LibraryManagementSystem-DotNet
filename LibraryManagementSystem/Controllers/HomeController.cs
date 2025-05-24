using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryManagementSystem.Models;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.TotalBooks = _context.Books.Count();
            ViewBag.TotalMembers = _context.Members.Count();
            ViewBag.TotalBorrowed = _context.BorrowRecords.Count(br => br.ReturnDate == null);
            ViewBag.TotalOverdue = _context.BorrowRecords.Count(br => br.ReturnDate == null && br.DueDate < DateTime.Today);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
