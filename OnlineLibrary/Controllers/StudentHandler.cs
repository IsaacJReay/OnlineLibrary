using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    public async Task<IActionResult> studentlist()
    {
        List<Student> StudentObj = await context.Students.ToListAsync();
        return Json(StudentObj);
    }
    public async Task<IActionResult> student(string UserID)
    {
        return Json(await Task.Run(() => context.Students.Single(student => student.UserID == UserID)));
    }
}