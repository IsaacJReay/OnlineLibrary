using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class LoginDto {
    public string UserID { get; set; } = default!;
    public string Password { get; set; } = default!;

}