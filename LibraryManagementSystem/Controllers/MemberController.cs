using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class MemberController : Controller
{
    private readonly ApplicationDbContext _context;

    public MemberController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var members = _context.Members.ToList();
        return View(members);
    }

    [HttpGet]
    public IActionResult Add()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        return View();
    }

    [HttpPost]
    public IActionResult Add(Member member)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var existing = _context.Members.FirstOrDefault(m => m.Name == member.Name && m.Email == member.Email);
        if (existing != null)
        {
            TempData["Message"] = "Member already exists.";
            return RedirectToAction("Index");
        }

        _context.Members.Add(member);
        _context.SaveChanges();
        TempData["Message"] = "Member added successfully.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var member = _context.Members.Find(id);
        return View(member);
    }

    [HttpPost]
    public IActionResult Edit(Member member)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        _context.Members.Update(member);
        _context.SaveChanges();
        TempData["Message"] = "Member updated successfully.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var member = _context.Members.Find(id);
        if (member != null)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
            TempData["Message"] = "Member deleted.";
        }

        return RedirectToAction("Index");
    }
}
