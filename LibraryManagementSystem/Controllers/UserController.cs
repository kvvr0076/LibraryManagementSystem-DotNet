using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserId") == null || HttpContext.Session.GetString("UserRole") != "admin")
            return RedirectToAction("Login", "Account");

        var users = _context.Users.ToList();
        ViewBag.TotalAdmins = users.Count(u => u.Role == "admin");
        return View(users);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.Find(id);
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        TempData["Message"] = "User updated.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            TempData["Message"] = "User deleted.";
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username))
        {
            ViewBag.Error = "Username already exists.";
            return View(user);
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        TempData["Message"] = "User added.";
        return RedirectToAction("Index");
    }
}
