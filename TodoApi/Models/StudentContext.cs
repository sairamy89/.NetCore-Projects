using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions options) : base(options)
        {}


        public DbSet<Student> Students { get; set; }
        
    }
}