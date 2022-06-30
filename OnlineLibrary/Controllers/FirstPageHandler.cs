using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> login(LoginDto req)
    {
        User CurrentUser = new User();

        if (await context.Users.FindAsync(req.UserID) != null)
        {
            CurrentUser = await context.Users.FindAsync(req.UserID) ?? default!;
        }
        else
        {
            return BadRequest("Not Found in DB");
        }

        if (!VerifyPasswordHash(req.Password, CurrentUser.UserPasswordHash))
        {
            return BadRequest("Incorrect Password");
        }

        string token = await CreateToken(CurrentUser);
        return Ok(token);
    }

    public async Task<IActionResult> status()
    {
        List<Teacher> TeachersObj = await context.Teachers.ToListAsync();
        List<Student> StudentObj = await context.Students.ToListAsync();
        return Json(
            new
            {
                TeacherCount = TeachersObj.Count(),
                StudentCount = StudentObj.Count(),
                FacultiesCount = Enum.GetNames(typeof(Enums.Faculties)).Length
            }
        );
    }
}