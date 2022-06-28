using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerTeacher(TeacherDto req)
    {
        (byte[] passwordhash, byte[] passwordSalt) = await CreatePasswordHashAsync(req.UserPassword);
        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);

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

        await context.Users.AddAsync(currentUser);
        await context.Teachers.AddAsync(currentTeacher);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }

    public async Task<IActionResult> teacherlist()
    {
        List<Teacher> TeachersObj = await context.Teachers.ToListAsync();
        return Json(TeachersObj);
    }

    public async Task<IActionResult> teacherByID(QueryDto req)
    {
        if (req.ID == null)
        {
            return BadRequest("Missing ID");
        }

        Teacher currentTeacher = new Teacher();

        try
        {
            currentTeacher = await context.Teachers.SingleAsync(teacher => teacher.UserID == req.ID);
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentTeacher);
    }

    [HttpPut]
    public async Task<IActionResult> editTeacher(TeacherDto req)
    {
        if (req.oldUserID == null)
        {
            return BadRequest("Missing Old ID");
        }

        if (await context.Users.FindAsync(req.oldUserID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);


        Teacher currentTeacher = await context.Teachers.FindAsync(req.oldUserID) ?? default!;
        currentTeacher.UserID = req.UserID;
        currentTeacher.TeacherDegree = req.TeacherDegree;
        currentTeacher.TeacherWorkExperience = req.TeacherWorkExperience;

        User currentUser = await context.Users.FindAsync(req.oldUserID) ?? default!;
        currentUser.UserID = req.UserID;
        currentUser.UserName = req.UserName;
        currentUser.UserGender = req.UserGender;
        currentUser.UserFaculty = req.UserFaculty;
        currentUser.UserAddress = req.UserAddress;
        currentUser.UserDateofBirth = req.UserDateofBirth;
        currentUser.UserTel = req.UserTel;
        currentUser.UserEmail = req.UserEmail;
        currentUser.PathToUserPhoto = filename;

        await context.SaveChangesAsync();

        return Ok("Success!");
    }

    [HttpDelete]
    public async Task<IActionResult> deleteTeacher(QueryDto req)
    {
        if (req.ID == null)
        {
            return BadRequest("Missing ID");
        }

        if (await context.Users.FindAsync(req.ID) == null)
        {
            return BadRequest("Not found");
        }

        Teacher Teacher = await context.Teachers.FindAsync(req.ID) ?? default!;
        context.Teachers.Remove(Teacher);

        User user = await context.Users.FindAsync(req.ID) ?? default!;
        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}