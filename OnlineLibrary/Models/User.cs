using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class User {
    [Key]
    public string UserID { get; set; } = default!;
    public Enums.Roles UserRole { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public Enums.Genders UserGender { get; set; } = default!;
    public Enums.Faculties UserFaculty { get; set; } = default!;
    public string? UserAddress { get; set; } = default!;
    public DateTime UserDateofBirth { get; set; } = default!;
    public string? UserTel { get; set; } = default!;
    public string? UserEmail { get; set; } = default!;
    public byte[] UserPasswordHash { get; set; } = default!;
    public byte[] UserPasswordSalt { get; set; } = default!;
    public string PathToUserPhoto { get; set; } = default!;
}