namespace OnlineLibrary.Models;

public class VideoDto {
    public string? VideoID { get; set; } = default!;
    public IFormFile VideoThumbnail { get; set; } = default!;
    public string? VideoTitle { get; set; } = default!;
    public Enums.Faculties VideoFaculty { get; set; } = default!;
    public string? TeacherID { get; set; } = default!;
    public DateTime VideoDate { get; set; } = default!;
}