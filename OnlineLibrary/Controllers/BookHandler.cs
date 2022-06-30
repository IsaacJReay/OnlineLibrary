using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> addBook(BookDto req)
    {
        (string BookFileName, string extension) = await UploadAsync(req.BookFile, false);

        if (!Enum.TryParse(extension, out Enums.FileType BookExtension))
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
        
        foreach (Book book in BookObj) 
        {
            book.Teacher = await context.Teachers.FindAsync(book.TeacherID) ?? default!;
            book.Teacher.User = await context.Users.FindAsync(book.TeacherID) ?? default!;
            // book.Teacher.User.UserPasswordHash = "HIDDEN";
        }

        return Json(BookObj);
    }

    public async Task<IActionResult> bookByID(string BookID)
    {
        Book currentBook = new Book();

        if (await context.Books.FindAsync(BookID) != null) 
        {
            currentBook = await context.Books.FindAsync(BookID) ?? default!;
            currentBook.Teacher = await context.Teachers.FindAsync(currentBook.TeacherID) ?? default!;
            currentBook.Teacher.User = await context.Users.FindAsync(currentBook.TeacherID) ?? default!;
            // currentBook.Teacher.User.UserPasswordHash = "HIDDEN";
        }
        else
        {
            return BadRequest("Not Found");
        }

        return Json(currentBook);
    }

    public async Task<IActionResult> bookByFaculty(string BookFaculty)
    {
        List<Book> currentBooks = new List<Book>();

        if (!Enum.TryParse(BookFaculty, out Enums.Faculties currentFaculty))
        {
            return BadRequest("Faculty Not found");
        }

        try
        {
            currentBooks = await context.Books.Where(book => book.BookFaculty == currentFaculty).ToListAsync();
        }
        catch
        {
            return BadRequest("Not found");
        }

        foreach (Book book in currentBooks) 
        {
            book.Teacher = await context.Teachers.FindAsync(book.TeacherID) ?? default!;
            book.Teacher.User = await context.Users.FindAsync(book.TeacherID) ?? default!;
            book.Teacher.User.UserPasswordHash = "HIDDEN";
        }

        return Json(currentBooks);
    }

    [HttpPut]
    public async Task<IActionResult> editBook(BookDto req)
    {
        if (req.BookID == null)
        {
            return BadRequest("Missing ID");
        }

        if (await context.Books.FindAsync(req.BookID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string extension) = await UploadAsync(req.BookFile, false);
        Enum.TryParse(extension, out Enums.FileType ftype);
        
        Book currentBook = await context.Books.FindAsync(req.BookID) ?? default!;
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
    public async Task<IActionResult> deleteBook(string BookID)
    {

        if (context.Books.Find(BookID) == null)
        {
            return BadRequest("Not found");
        }

        Book book = await context.Books.FindAsync(BookID) ?? default!;
        context.Books.Remove(book);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}