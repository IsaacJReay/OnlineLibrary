using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class LoginDto {
    public string? username { get; set; } = default!;
    public string? password { get; set; } = default!;

}