using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers;

public partial class apiController : Controller
{
    protected readonly OnlineLibraryDbContext context = default!;
    public apiController(OnlineLibraryDbContext context)
    {
        this.context = context;
    }
    
}
