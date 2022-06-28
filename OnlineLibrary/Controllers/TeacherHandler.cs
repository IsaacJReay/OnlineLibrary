using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerTeacher(TeacherDto req)
    {
        (byte[] passwordhash, byte[] passwordSalt) = await Task.Run(() => CreatePasswordHash(req.UserPassword));
        (string filename, string? _) = await Task.Run(() => Upload(req.UserPhoto, true));

        User currentUser = new User
        {
            UserID = req.UserID,
            UserName = req.UserName,
            UserRole = Enums.Roles.Teacher,
            UserGender = req.UserGender,
            UserFaculty = req.UserFaculty,
            UserAddress = req.UserAddress,
            UserDateofBirth = req.UserDateofBirth,
            UserTel = req.UserTel,
            UserEmail = req.UserEmail,
            UserPasswordHash = passwordhash,
            UserPasswordSalt = passwordSalt,
            PathToUserPhoto = filename
        };

        Teacher currentTeacher = new Teacher
        {
            UserID = req.UserID,
            TeacherDegree = req.TeacherDegree,
            TeacherWorkExperience = req.TeacherWorkExperience
        };

        context.Users.Add(currentUser);
        context.Teachers.Add(currentTeacher);
        await Task.Run(() => context.SaveChanges());

        return Ok("Success!");
    }

    public async Task<IActionResult> teacherlist()
    {
        List<Teacher> TeachersObj = await context.Teachers.ToListAsync();
        return Json(TeachersObj);
    }

    public async Task<IActionResult> teacherByID(string UserID)
    {
        Teacher currentTeacher = new Teacher();

        try
        {
            currentTeacher = await Task.Run(() => context.Teachers.Single(teacher => teacher.UserID == UserID));
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentTeacher);
    }
}