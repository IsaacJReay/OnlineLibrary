using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerStudent(StudentDto req)
    {
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
        
        foreach (Student student in StudentObj) 
        {
            student.User = await context.Users.FindAsync(student.UserID) ?? default!;
            student.User.UserFaculty = (Enums.Faculties) student.User.UserFaculty;
            student.User.UserRole = (Enums.Roles) student.User.UserRole;
            student.User.UserGender = (Enums.Genders) student.User.UserGender;
            student.User.UserPasswordHash = "HIDDEN";
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
            currentStudent.User.UserPasswordHash = "HIDDEN";
            currentStudent.User.UserFaculty = (Enums.Faculties) currentStudent.User.UserFaculty;
            currentStudent.User.UserRole = (Enums.Roles) currentStudent.User.UserRole;
            currentStudent.User.UserGender = (Enums.Genders) currentStudent.User.UserGender;
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
        if (req.oldUserID == null)
        {
            return BadRequest("Missing Old ID");
        }

        if (await context.Users.FindAsync(req.oldUserID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);


        Student currentStudent = await context.Students.FindAsync(req.oldUserID) ?? default!;
        currentStudent.UserID = req.UserID;
        currentStudent.StudentFatherName = req.StudentFatherName;
        currentStudent.StudentMotherName = req.StudentMotherName;
        currentStudent.TeacherID = req.TeacherID;

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