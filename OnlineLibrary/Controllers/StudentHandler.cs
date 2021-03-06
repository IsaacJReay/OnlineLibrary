using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerStudent(StudentDto req)
    {
        if (req.UserPassword == null)
        {
            return BadRequest("Password cannot be null");
        }
        
        string passwordhash = await CreatePasswordHashAsync(req.UserPassword);
        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);

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
            PathToUserPhoto = filename
        };

        Student currentStudent = new Student
        {
            UserID = req.UserID,
            StudentFatherName = req.StudentFatherName,
            StudentMotherName = req.StudentMotherName,
            TeacherID = req.TeacherID
        };

        await context.Users.AddAsync(currentUser);
        await context.Students.AddAsync(currentStudent);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }

    public async Task<IActionResult> studentlist()
    {
        List<Student> StudentObj = await context.Students.ToListAsync();
        
        foreach (Student currentStudent in StudentObj) 
        {
            currentStudent.User = await context.Users.FindAsync(currentStudent.UserID) ?? default!;
            currentStudent.Teacher = await context.Teachers.FindAsync(currentStudent.TeacherID) ?? default!;
            currentStudent.Teacher.User = await context.Users.FindAsync(currentStudent.TeacherID) ?? default!;
        }
        return Json(StudentObj);
    }

    public async Task<IActionResult> studentByID(string UserID)
    {
        Student currentStudent = new Student();

        if (await context.Students.FindAsync(UserID) != null)
        {
            currentStudent = await context.Students.FindAsync(UserID) ?? default!;
            currentStudent.User = await context.Users.FindAsync(UserID) ?? default!;
            currentStudent.Teacher = await context.Teachers.FindAsync(currentStudent.TeacherID) ?? default!;
            currentStudent.Teacher.User = await context.Users.FindAsync(currentStudent.TeacherID) ?? default!;
        }
        else
        {
            return BadRequest("Not found");
        }

        return Json(currentStudent);
    }

    [HttpPut]
    public async Task<IActionResult> editStudent(StudentDto req)
    {
        if (await context.Users.FindAsync(req.UserID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);

        Student currentStudent = await context.Students.FindAsync(req.UserID) ?? default!;
        currentStudent.StudentFatherName = req.StudentFatherName;
        currentStudent.StudentMotherName = req.StudentMotherName;
        currentStudent.TeacherID = req.TeacherID;

        User currentUser = await context.Users.FindAsync(req.UserID) ?? default!;
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
    public async Task<IActionResult> deleteStudent(string UserID)
    {
        if (await context.Users.FindAsync(UserID) == null)
        {
            return BadRequest("Not found");
        }

        Student student = await context.Students.FindAsync(UserID) ?? default!;
        context.Students.Remove(student);

        User user = await context.Users.FindAsync(UserID) ?? default!;
        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}