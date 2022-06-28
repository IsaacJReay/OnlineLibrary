using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerAdmin(AdminDto req)
    {
        (byte[] passwordhash, byte[] passwordSalt) = await CreatePasswordHashAsync(req.UserPassword);
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
            UserPasswordSalt = passwordSalt,
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
    public async Task<IActionResult> deleteAdmin(QueryDto req)
    {
        if (req.ID == null)
        {
            return BadRequest("Missing ID");
        }

        if (await context.Users.FindAsync(req.ID) == null)
        {
            return BadRequest("Not found");
        }

        User user = await context.Users.FindAsync(req.ID) ?? default!;
        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}