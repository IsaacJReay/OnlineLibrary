using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OnlineLibrary.Models;

public class User {
    public Enums.Roles UserRole { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public Enums.Genders UserGender { get; set; } = default!;
    public Enums.Faculties UserFaculty { get; set; } = default!;
    public string? UserAddress { get; set; } = default!;
    public DateTime UserDateofBirth { get; set; } = default!;
    public string? UserTel { get; set; } = default!;
    public string? UserEmail { get; set; } = default!;
    public string PathToUserPhoto { get; set; } = default!;

    [Key]
    public string UserID { get; set; } = default!;

    [JsonIgnore]
    public string UserPasswordHash { get; set; } = default!;
}