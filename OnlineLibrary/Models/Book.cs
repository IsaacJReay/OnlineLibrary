using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Book {
    [Key]
    public string? BookID { get; set; } = default!;
    public Enums.FileType BookFileType { get; set; } = default!;
    public string? BookTitle { get; set; } = default!;
    [ForeignKey("Teacher")]
    public string? TeacherID { get; set; } = default!;
    public virtual Teacher Teacher { get; set; } = default!;
    public DateTime BookDate { get; set; } = default!;
    public Enums.Faculties BookFaculty { get; set; } = default!;

}