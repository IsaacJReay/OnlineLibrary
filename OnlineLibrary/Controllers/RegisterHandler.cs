using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using System.Security.Cryptography;


namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    [HttpPost("registerStudent")]
    public async Task<IActionResult> RegisterStudent(StudentDto req)
    {

        if (req.UserPassword == null) {
            return BadRequest("Null");
        }

        CreatePasswordHash(req.UserPassword, out byte[] passwordhash, out byte[] passwordSalt);
        string filename = await Task.Run(() => UploadPhoto(req.UserPhoto));

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

        return Ok(currentUser);
    }

    [HttpPost("registerTeacher")]
    public async Task<IActionResult> RegisterTeacher(TeacherDto req)
    {

        if (req.UserPassword == null) {
            return BadRequest("Null");
        }

        CreatePasswordHash(req.UserPassword, out byte[] passwordhash, out byte[] passwordSalt);
        string filename = await Task.Run(() => UploadPhoto(req.UserPhoto));

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

        return Ok(currentUser);
    }

    [HttpPost("registerAdmin")]
    public async Task<IActionResult> RegisterAdmin(AdminDto req)
    {

        if (req.UserPassword == null) {
            return BadRequest("Null");
        }

        CreatePasswordHash(req.UserPassword, out byte[] passwordhash, out byte[] passwordSalt);
        string filename = await Task.Run(() => UploadPhoto(req.UserPhoto));

        User currentUser = new User
        {
            UserID = req.UserID,
            UserName = req.UserName,
            UserRole = Enums.Roles.Admin,
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
        context.Users.Add(currentUser);
        await Task.Run(() => context.SaveChanges());

        return Ok(currentUser);
    }
}