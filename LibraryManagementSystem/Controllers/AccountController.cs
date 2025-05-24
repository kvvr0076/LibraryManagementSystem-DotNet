using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserRole", user.Role);
            TempData["Message"] = $"Welcome, {user.Name}!";
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid username or password.";
        return View();
    }

    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Signup(User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username))
        {
            ViewBag.Error = "Username already exists.";
            return View();
        }

        user.Role = "librarian"; // All signups are librarians
        _context.Users.Add(user);
        _context.SaveChanges();
        TempData["Message"] = "Signup successful. You can now login.";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
