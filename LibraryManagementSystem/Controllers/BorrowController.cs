using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BorrowController : Controller
{
    private readonly ApplicationDbContext _context;

    public BorrowController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var records = _context.BorrowRecords
            .Include(br => br.Book)
            .Include(br => br.Member)
            .ToList();

        ViewBag.Today = DateTime.Today;
        return View(records);
    }

    [HttpGet]
    public IActionResult Lend()
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        ViewBag.Books = _context.Books.Where(b => b.Quantity > 0).ToList();
        ViewBag.Members = _context.Members.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Lend(int bookId, int memberId)
    {
        if (HttpContext.Session.GetString("UserId") == null)
            return RedirectToAction("Login", "Account");

        var book = _context.Books.Find(bookId);
        if (book == null || book.Quantity <= 0)
        {
            TempData["Message"] = "Book not available.";
            return RedirectToAction("Lend");
        }

        var borrow = new BorrowRecord
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(7)
        };

        _context.BorrowRecords.Add(borrow);
        book.Quantity -= 1;
        _context.SaveChanges();

        TempData["Message"] = "Book issued successfully.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Return(int id)
    {
        var record = _context.BorrowRecords
            .Include(r => r.Book)
            .FirstOrDefault(r => r.Id == id);

        if (record != null && record.ReturnDate == null)
        {
            record.ReturnDate = DateTime.Today;
            if (record.Book != null)
                record.Book.Quantity += 1;

            _context.SaveChanges();
            TempData["Message"] = "Book returned.";
        }

        return RedirectToAction("Index");
    }
}
