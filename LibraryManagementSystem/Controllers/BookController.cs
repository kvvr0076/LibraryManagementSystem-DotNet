using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var books = _context.Books.ToList();
        return View(books);
    }

    [HttpGet]
    public IActionResult Add()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        return View();
    }

    [HttpPost]
    public IActionResult Add(Book book)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var existing = _context.Books.FirstOrDefault(b => b.Title == book.Title && b.Author == book.Author);
        if (existing != null)
        {
            existing.Quantity += book.Quantity;
            TempData["Message"] = "Book already exists. Quantity updated.";
        }
        else
        {
            _context.Books.Add(book);
            TempData["Message"] = "Book added successfully.";
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var book = _context.Books.Find(id);
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(Book book)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        _context.Books.Update(book);
        _context.SaveChanges();
        TempData["Message"] = "Book updated successfully.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var book = _context.Books.Find(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            TempData["Message"] = "Book deleted.";
        }

        return RedirectToAction("Index");
    }
}
