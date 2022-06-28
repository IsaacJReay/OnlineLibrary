using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    [HttpPost]
    public async Task<IActionResult> registerAdmin(AdminDto req)
    {
        (byte[] passwordhash, byte[] passwordSalt) = await Task.Run(() => CreatePasswordHash(req.UserPassword));
        (string filename, string? _) = await Task.Run(() => Upload(req.UserPhoto, true));

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

        return Ok("Success!");
    }
}