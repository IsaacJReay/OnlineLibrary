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
        try
        {
            CurrentUser = await Task.Run(() => context.Users.Single(User => User.UserID == req.UserID));
        }
        catch
        {
            return BadRequest("Not Found");
        }

        if (!VerifyPasswordHash(req.Password, CurrentUser.UserPasswordHash, CurrentUser.UserPasswordSalt))
        {
            return BadRequest("Not Found");
        }

        string token = CreateToken(CurrentUser) ?? default!;
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