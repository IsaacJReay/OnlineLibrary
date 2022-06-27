using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    public async Task<IActionResult> booklist()
    {
        List<Book> BookObj = await context.Books.ToListAsync();
        return Json(BookObj);
    }
    public async Task<IActionResult> bookByID(string BookID)
    {
        return Json(await Task.Run(() => context.Books.Single(book => book.BookID == BookID)));
    }
    public async Task<IActionResult> bookByFaculty(string Faculty)
    {
        Enum.TryParse(Faculty, out Enums.Faculties FacultyEnum);
        return Json(await Task.Run(() => context.Books.Single(book => book.BookFaculty == FacultyEnum)));
    }
}