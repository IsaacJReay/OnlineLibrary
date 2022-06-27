using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;
namespace OnlineLibrary.Controllers;
public partial class apiController : Controller
{
    public async Task<IActionResult> videolist()
    {
        List<Video> VideoObj = await context.Videos.ToListAsync();
        return Json(VideoObj);
    }
    public async Task<IActionResult> videoByID(string VideoID)
    {
        return Json(await Task.Run(() => context.Videos.Single(video => video.VideoID == VideoID)));
    }
    public async Task<IActionResult> videoByFaculty(string Faculty)
    {
        Enum.TryParse(Faculty, out Enums.Faculties FacultyEnum);
        return Json(await Task.Run(() => context.Videos.Single(video => video.VideoFaculty == FacultyEnum)));
    }
}