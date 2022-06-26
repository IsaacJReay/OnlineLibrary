using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Teacher
{
    [Key]
    public string? TeacherID { get; set; } = default!;
    public string? TeacherDegree { get; set; } = default!;
    public string? TeacherWorkExperience { get; set; } = default!;
}