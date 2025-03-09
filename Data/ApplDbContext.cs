using labITP.Models;
using Microsoft.EntityFrameworkCore;

namespace labITP.Data
{
    public class ApplDbContext:DbContext
    {
        public ApplDbContext(DbContextOptions<ApplDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
    }
}
