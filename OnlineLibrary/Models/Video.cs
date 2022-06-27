using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models;

public class Video {
    [Key]
    public string? VideoID { get; set; } = default!;
    public string? PathToVideoThumbnail { get; set; } = default!;
    public string? VideoTitle { get; set; } = default!;
    public Enums.Faculties VideoFaculty { get; set; } = default!;
    [ForeignKey("Teacher")]
    public string? TeacherID { get; set; } = default!;
    public virtual Teacher Teacher { get; set; } = default!;
    public DateTime VideoDate { get; set; } = default!;
}