namespace OnlineLibrary.Models;

public class TeacherDto
{
    public string UserID { get; set; } = default!;
    public Enums.Roles UserRole { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public Enums.Genders UserGender { get; set; } = default!;
    public Enums.Faculties UserFaculty { get; set; } = default!;
    public string? UserAddress { get; set; } = default!;
    public DateTime UserDateofBirth { get; set; } = default!;
    public string? UserTel { get; set; } = default!;
    public string? UserEmail { get; set; } = default!;
    public string UserPassword { get; set; } = default!;
    public string TeacherDegree { get; set; } = default!;
    public string TeacherWorkExperience { get; set; } = default!;
    public IFormFile UserPhoto { get; set; } = default!;
}