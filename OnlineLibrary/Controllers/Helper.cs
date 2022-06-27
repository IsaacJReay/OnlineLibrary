using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data;
using System.Security.Cryptography;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    protected readonly OnlineLibraryDbContext context = default!;
    public apiController(OnlineLibraryDbContext context)
    {
        this.context = context;
    }
    
    protected void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    protected string UploadPhoto(IFormFile currentPhoto)
    {
        var destination = Path.Combine("/home/isaac/project/OnlineLibrary/", "image");
        string? filename = default!;
        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        if (currentPhoto != null)
        {
            var extension = Path.GetExtension(currentPhoto.FileName);
            filename = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + extension;

            var destinationFile = Path.Combine(destination, filename);
            using FileStream fstream = new FileStream(destinationFile, FileMode.Create);
            currentPhoto.CopyToAsync(fstream);

        }
        return filename;
    }
}
