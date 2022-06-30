using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    public async Task<IActionResult> addVideo(VideoDto req)
    {
        (string VideoFileName, string? _) = await UploadAsync(req.VideoFile, false);
        (string VideoThumbnail, string? _) = await UploadAsync(req.VideoThumbnail, true);

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
        
        await context.Videos.AddAsync(currentVideo);
        await context.SaveChangesAsync();
        return Ok("Success!");
    }

    public async Task<IActionResult> videolist()
    {
        List<Video> VideoObj = await context.Videos.ToListAsync();

        foreach (Video video in VideoObj) 
        {
            video.Teacher = await context.Teachers.FindAsync(video.TeacherID) ?? default!;
            video.Teacher.User = await context.Users.FindAsync(video.TeacherID) ?? default!;
            // video.Teacher.User.UserPasswordHash = "HIDDEN";
        }
        return Json(VideoObj);
    }

    public async Task<IActionResult> videoByID(string VideoID)
    {
        Video currentVideo = new Video();

        if (await context.Videos.FindAsync(VideoID) != null)
        {
            currentVideo = await context.Videos.FindAsync(VideoID) ?? default!;
            currentVideo.Teacher = await context.Teachers.FindAsync(currentVideo.TeacherID) ?? default!;
            currentVideo.Teacher.User = await context.Users.FindAsync(currentVideo.TeacherID) ?? default!;
            // currentVideo.Teacher.User.UserPasswordHash = "HIDDEN";
        }
        else
        {
            return BadRequest("Not found");
        }

        return Json(currentVideo);
    }

    public async Task<IActionResult> videoByFaculty(string faculty)
    {
        List<Video> currentVideos = new List<Video>();

        if (Enum.TryParse(faculty, out Enums.Faculties currentFaculty))
        {
            return BadRequest("Not Found");
        }

        try
        {
            currentVideos = await context.Videos.Where(video => video.VideoFaculty == currentFaculty).ToListAsync();
        }
        catch
        {
            return BadRequest("Not found");
        }

        foreach (Video video in currentVideos) 
        {
            video.Teacher = await context.Teachers.FindAsync(video.TeacherID) ?? default!;
            video.Teacher.User = await context.Users.FindAsync(video.TeacherID) ?? default!;
            // video.Teacher.User.UserPasswordHash = "HIDDEN";
        }

        return Json(currentVideos);
    }

    [HttpPut]
    public async Task<IActionResult> editVideo(VideoDto req)
    {
        if (req.oldVideoID == null)
        {
            return BadRequest("Missing ID");
        }

        if (await context.Videos.FindAsync(req.oldVideoID) == null)
        {
            return BadRequest("Not found");
        }

        (string filename, string? _) = await UploadAsync(req.VideoFile, false);
        (string thumbnail, string? _) = await UploadAsync(req.VideoThumbnail, true);
        
        Video currentVideo = await context.Videos.FindAsync(req.oldVideoID) ?? default!;
        currentVideo.VideoID = req.VideoID;
        currentVideo.PathToVideoThumbnail = thumbnail;
        currentVideo.VideoDate = req.VideoDate;
        currentVideo.VideoTitle = req.VideoTitle;
        currentVideo.TeacherID = req.TeacherID;
        currentVideo.VideoFaculty = req.VideoFaculty;
        currentVideo.PathToVideoFile = filename;
        currentVideo.VideoFaculty = req.VideoFaculty;

        await context.SaveChangesAsync();

        return Ok("Success!");
    }

    [HttpDelete]
    public async Task<IActionResult> deleteVideo(string VideoID)
    {
        if (await context.Videos.FindAsync(VideoID) == null)
        {
            return BadRequest("Not found");
        }

        Video Video = await context.Videos.FindAsync(VideoID) ?? default!;
        context.Videos.Remove(Video);

        await context.SaveChangesAsync();

        return Ok("Success!");
    }
}
