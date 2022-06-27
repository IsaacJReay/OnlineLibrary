using Microsoft.AspNetCore.Mvc;
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

        if (req.Password == null) {
            return BadRequest("Null");
        }

        if (!VerifyPasswordHash(req.Password, CurrentUser.UserPasswordHash, CurrentUser.UserPasswordSalt) )
        {
            return BadRequest("Not Found");
        }

        string token = CreateToken(CurrentUser) ?? default!;
        return Ok(token);
    }
}