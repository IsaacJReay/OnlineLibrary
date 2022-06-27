using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Data
{
    public class OnlineLibraryDbContext: DbContext
    {
        public OnlineLibraryDbContext(DbContextOptions<OnlineLibraryDbContext> options): base (options)
        {
            
        }

        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Teacher> Teachers { get; set; } = default!;
        public DbSet<Video> Videos { get; set; } = default!;
    }
}