using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Student
{
    [Key]
    public string? StudentID { get; set; } = default!;
    public string? StudentName { get; set; } = default!;
    public string? StudentFatherName { get; set; } = default!;
    public string? StudentMotherName { get; set; } = default!;
    public static Enums.Genders StudentGender { get; set; } = default!;
    public Enums.Subjects StudentSubject { get; set; } = default!;
    public string? StudentAddress { get; set; } = default!;
    public DateTime StudentDateofBirth { get; set; } = default!;
    public string? StudentTel { get; set; } = default!;
    public string? StudentEmail { get; set; } = default!;
    public string? StudentPassword { get; set; } = default!;
    public string? PathToStudentPhoto { get; set; } = default!;
    [ForeignKey("Teacher")]
    public string? TeacherID { get; set; } = default!;
    public virtual Teacher Teacher { get; set; } = default!;
}
