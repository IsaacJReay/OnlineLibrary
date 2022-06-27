using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Student
{
    [Key]
    [ForeignKey("User")]
    public string? UserID { get; set; } = default!;
    public virtual User User { get; set; } = default!;
    public string? StudentFatherName { get; set; } = default!;
    public string? StudentMotherName { get; set; } = default!;
    [ForeignKey("Teacher")]
    public string? TeacherID { get; set; } = default!;
    public virtual Teacher Teacher { get; set; } = default!;
}
