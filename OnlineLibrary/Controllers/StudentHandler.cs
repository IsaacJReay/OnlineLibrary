using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerStudent(StudentDto req)
    {
        (byte[] passwordhash, byte[] passwordSalt) = await Task.Run(() => CreatePasswordHash(req.UserPassword));
        (string filename, string? _) = await Task.Run(() => Upload(req.UserPhoto, true));

        User currentUser = new User
        {
            UserID = req.UserID,
            UserName = req.UserName,
            UserRole = Enums.Roles.Student,
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

        Student currentStudent = new Student 
        {
            UserID = req.UserID,
            StudentFatherName = req.StudentFatherName,
            StudentMotherName = req.StudentMotherName,
            TeacherID = req.TeacherID
        };
        context.Users.Add(currentUser);
        context.Students.Add(currentStudent); 
        await Task.Run(() => context.SaveChanges());

        return Ok("Success!");
    }

    public async Task<IActionResult> studentlist()
    {
        List<Student> StudentObj = await context.Students.ToListAsync();
        return Json(StudentObj);
    }

    public async Task<IActionResult> studentByID(string UserID)
    {
        Student currentStudent = new Student();

        try
        {
            currentStudent = await Task.Run(() => context.Students.Single(student => student.UserID == UserID));
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentStudent);
    }
}