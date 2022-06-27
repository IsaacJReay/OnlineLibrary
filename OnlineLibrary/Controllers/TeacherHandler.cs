using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    public async Task<IActionResult> teacherlist()
    {
        List<Teacher> TeachersObj = await context.Teachers.ToListAsync();
        return Json(TeachersObj);
    }
    public async Task<IActionResult> teacherByID(string UserID)
    {
        return Json(await Task.Run(() => context.Teachers.Single(teacher => teacher.UserID == UserID)));
    }
}