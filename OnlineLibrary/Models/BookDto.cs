namespace OnlineLibrary.Models;

public class BookDto {
    public string BookID { get; set; } = default!;
    public Enums.FileType BookFileType { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public string TeacherID { get; set; } = default!;
    public DateTime BookDate { get; set; } = default!;
    public Enums.Faculties BookFaculty { get; set; } = default!;
    public IFormFile BookFile { get; set; } = default!;

}