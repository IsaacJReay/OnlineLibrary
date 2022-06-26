using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Teacher
{
    [Key]
    public string? TeacherID { get; set; } = default!;
    public string? TeacherName { get; set; } = default!;
    public Enums.Genders TeacherGender { get; set; } = default!;
    public Enums.Subjects TeacherSubject { get; set; } = default!;
    public string? TeacherDegree { get; set; } = default!;
    public string? TeacherWorkExperience { get; set; } = default!;
    public string? TeacherAddress { get; set; } = default!;
    public DateTime TeacherDateofBirth { get; set; } = default!;
    public string? TeacherTel { get; set; } = default!;
    public string? TeacherEmail { get; set; } = default!;
    public string? TeacherPassword { get; set; } = default!;
    public string? PathToTeacherPhoto { get; set; } = default!;
}