using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> addBook(BookDto req)
    {
        (string BookFileName, string extension) = await Task.Run(() => Upload(req.BookFile, true));

        if (Enum.TryParse(extension, out Enums.FileType BookExtension))
        {
            return BadRequest("Extension not supported");
        }

        Book currentBook = new Book
        {
            BookID = req.BookID,
            BookFileType = BookExtension,
            BookTitle = req.BookTitle,
            TeacherID = req.TeacherID,
            BookDate = req.BookDate,
            BookFaculty = req.BookFaculty,
            PathToBookFile = BookFileName
        };
        context.Books.Add(currentBook);
        await Task.Run(() => context.SaveChanges());

        return Ok("Success!");
    }
    public async Task<IActionResult> booklist()
    {
        List<Book> BookObj = await context.Books.ToListAsync();
        return Json(BookObj);
    }

    public async Task<IActionResult> bookByID(string BookID)
    {
        Book currentBook = new Book();
        
        try
        {
            currentBook = await Task.Run(() => context.Books.Single(book => book.BookID == BookID));
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentBook);
    }

    public async Task<IActionResult> bookByFaculty(string Faculty)
    {
        if (Enum.TryParse(Faculty, out Enums.Faculties FacultyEnum))
        {
            return BadRequest("Faculty Not Found");
        };
        return Json(await Task.Run(() => context.Books.Single(book => book.BookFaculty == FacultyEnum)));
    }
}