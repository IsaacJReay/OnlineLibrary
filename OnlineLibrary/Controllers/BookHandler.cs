using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> addBook(BookDto req)
    {
        (string BookFileName, string extension) = await UploadAsync(req.BookFile, false);

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
        await context.Books.AddAsync(currentBook);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }
    public async Task<IActionResult> booklist()
    {
        List<Book> BookObj = await context.Books.ToListAsync();
        return Json(BookObj);
    }

    public async Task<IActionResult> bookByID(QueryDto req)
    {
        if (req.ID == null)
        {
            return BadRequest("Missing ID");
        }

        Book currentBook = new Book();

        try
        {
            currentBook = await context.Books.SingleAsync(book => book.BookID == req.ID);
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentBook);
    }

    public async Task<IActionResult> bookByFaculty(QueryDto req)
    {
        List<Book> currentBooks = new List<Book>();

        try
        {
            currentBooks = await context.Books.Where(book => book.BookFaculty == req.Faculty).ToListAsync();
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentBooks);
    }

    [HttpPut]
    public async Task<IActionResult> editBook(BookDto req)
    {
        if (req.oldBookID == null)
        {
            return BadRequest("Missing ID");
        }

        if (await context.Books.FindAsync(req.oldBookID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string extension) = await UploadAsync(req.BookFile, false);
        Enum.TryParse(extension, out Enums.FileType ftype);
        
        Book currentBook = await context.Books.FindAsync(req.oldBookID) ?? default!;
        currentBook.BookID = req.BookID;
        currentBook.BookFileType = ftype;
        currentBook.BookDate = req.BookDate;
        currentBook.BookTitle = req.BookTitle;
        currentBook.TeacherID = req.TeacherID;
        currentBook.BookFaculty = req.BookFaculty;
        currentBook.PathToBookFile = filename;

        await context.SaveChangesAsync();


        return Ok("Success!");
    }

    [HttpDelete]
    public async Task<IActionResult> deleteBook(QueryDto req)
    {
        if (req.ID == null)
        {
            return BadRequest("Missing ID");
        }

        if (context.Books.Find(req.ID) == null)
        {
            return BadRequest("Not found");
        }

        Book book = await context.Books.FindAsync(req.ID) ?? default!;
        context.Books.Remove(book);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}