using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> addVideo(VideoDto req)
    {
        (string VideoFileName, string? _) = await Task.Run(() => Upload(req.VideoFile, true));
        (string VideoThumbnail, string? _) = await Task.Run(() => Upload(req.VideoThumbnail, true));

        Video currentVideo = new Video 
        {
            VideoID = req.VideoID,
            PathToVideoFile = VideoFileName,
            PathToVideoThumbnail = VideoThumbnail,
            VideoTitle = req.VideoTitle,
            VideoFaculty = req.VideoFaculty,
            TeacherID = req.TeacherID,
            VideoDate = req.VideoDate
        };
        context.Videos.Add(currentVideo);
        await Task.Run(() => context.SaveChanges());
        return Ok("Success!");
    }

    public async Task<IActionResult> videolist()
    {
        List<Video> VideoObj = await context.Videos.ToListAsync();
        return Json(VideoObj);
    }

    public async Task<IActionResult> videoByID(string VideoID)
    {
        Video currentVideo = new Video();

        try
        {
            currentVideo = await Task.Run(() => context.Videos.Single(video => video.VideoID == VideoID));
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentVideo);
    }

    public async Task<IActionResult> videoByFaculty(string Faculty)
    {
        if (Enum.TryParse(Faculty, out Enums.Faculties FacultyEnum)) 
        {
            return BadRequest("Faculty not found");
        };

        Video currentVideo = new Video();

        try
        {
            currentVideo = await Task.Run(() => context.Videos.Single(video => video.VideoFaculty == FacultyEnum));
        }
        catch
        {
            return BadRequest("Not found");
        }

        return Json(currentVideo);
    }
}
