using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> adminlist()
    {
        Enum.TryParse("Admin", out Enums.Roles adminRole);
        List<User> AdminObj = await Task.Run(() => context.Users.Where(user => user.UserRole == adminRole).ToList());

        foreach (User Admin in AdminObj)
        {
            Admin.UserPasswordHash = "HIDDEN";
            Admin.UserGender = (Enums.Genders) Admin.UserGender;
            Admin.UserFaculty = (Enums.Faculties) Admin.UserFaculty;
            Admin.UserRole = (Enums.Roles) Admin.UserRole;
        }

        return Json(AdminObj);
    }

    public async Task<IActionResult> adminByID(string UserID)
    {
        User currentAdmin = new User();

        if (await context.Users.FindAsync(UserID) != null)
        {
            currentAdmin = await context.Users.FindAsync(UserID) ?? default!;
            currentAdmin.UserPasswordHash = "HIDDEN";
            currentAdmin.UserFaculty = (Enums.Faculties) currentAdmin.UserFaculty;
            currentAdmin.UserRole = (Enums.Roles) currentAdmin.UserRole;
            currentAdmin.UserGender = (Enums.Genders) currentAdmin.UserGender;
        }
        else
        {
            return BadRequest("Not found");
        }

        return Json(currentAdmin);
    }

    [HttpPost]
    public async Task<IActionResult> registerAdmin(AdminDto req)
    {
        string passwordhash = await CreatePasswordHashAsync(req.UserPassword);
        (string filename, string? _) = await UploadAsync(req.UserPhoto, true);

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
            PathToUserPhoto = filename
        };
        await context.Users.AddAsync(currentUser);
        await context.SaveChangesAsync();

        return Ok("Success!");
    }

    [HttpPut]
    public async Task<IActionResult> editAdmin(AdminDto req)
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
    public async Task<IActionResult> deleteAdmin(string UserID)
    {
        if (await context.Users.FindAsync(UserID) == null)
        {
            return BadRequest("Not found");
        }

        User user = await context.Users.FindAsync(UserID) ?? default!;
        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}