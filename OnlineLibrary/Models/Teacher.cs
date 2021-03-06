using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Teacher
{
    public string TeacherDegree { get; set; } = default!;
    public string TeacherWorkExperience { get; set; } = default!;

    [Key]
    [ForeignKey("User")]
    public string UserID { get; set; } = default!;
    public virtual User User { get; set; } = default!;

}