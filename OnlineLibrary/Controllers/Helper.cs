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

    protected async Task<(byte[], byte[])> CreatePasswordHashAsync(string password)
    {
        using (HMACSHA512 hmac = new HMACSHA512())
        {
            return (
                hmac.Key,
                hmac.ComputeHash(
                    await Task.Run(
                        () => System.Text.Encoding.UTF8.GetBytes(
                            password
                        )
                    )
                )
            );
        }
    }

    protected bool VerifyPasswordHash(string UserPassword, byte[] UserpasswordHash, byte[] UserpasswordSalt)
    {
        using (HMACSHA512 hmac = new HMACSHA512(UserpasswordSalt))
        {
            byte[] computeHash = hmac.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(
                    UserPassword
                )
            );
            return computeHash.SequenceEqual(UserpasswordHash);
        }
    }

    protected async Task<string> CreateToken(User User)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, User.UserID, User.UserName)
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection(
                    "JwtSettings:SecretKey"
                ).Value
            )
        );
        int TokenExpiration = int.Parse(
            configuration.GetSection(
                "JwtSettings:TokenExpirationSec"
            ).Value
        );
        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken token = await Task.Run(() => new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(TokenExpiration),
            signingCredentials: cred
        ));
        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }

    protected async Task<(string, string)> UploadAsync(IFormFile currentFile, bool isPhoto)
    {
        string destination = isPhoto ? Path.Combine("/home/isaac/project/OnlineLibrary/", "images") : Path.Combine("/home/isaac/project/OnlineLibrary/", "files"); ;

        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        string extension = Path.GetExtension(currentFile.FileName);
        string filename = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + extension;
        string destinationFile = Path.Combine(destination, filename);

        using FileStream fstream = new FileStream(destinationFile, FileMode.Create);
        await currentFile.CopyToAsync(fstream);

        return (filename, extension);
    }
}
