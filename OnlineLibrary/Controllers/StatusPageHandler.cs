using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    public async Task<IActionResult> status()
    {
        List<Teacher> TeachersObj = await context.Teachers.ToListAsync();
        List<Student> StudentObj = await context.Students.ToListAsync();
        return Json(
            new {
                TeacherCount = TeachersObj.Count(),
                StudentCount = StudentObj.Count(),
                FacultiesCount = Enum.GetNames(typeof(Enums.Faculties)).Length
            }
        );
    }
}