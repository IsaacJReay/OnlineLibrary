using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    protected readonly OnlineLibraryDbContext context = default!;
    protected readonly IConfiguration configuration = default!;

    public apiController(OnlineLibraryDbContext context, IConfiguration configuration)
    {
        this.context = context;
        this.configuration = configuration;
    }

    protected (byte[], byte[]) CreatePasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }

    }

    protected bool VerifyPasswordHash(string UserPassword, byte[] UserpasswordHash, byte[] UserpasswordSalt)
    {
        using (var hmac = new HMACSHA512(UserpasswordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(UserPassword));
            return computeHash.SequenceEqual(UserpasswordHash);
        }
    }

    protected string? CreateToken(User User)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, User.UserID ?? default!, User.UserName)
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt; 
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
