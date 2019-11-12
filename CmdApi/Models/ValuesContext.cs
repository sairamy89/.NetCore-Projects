using Microsoft.EntityFrameworkCore;

namespace CmdApi.Models
{
    public class ValuesContext : DbContext
    {
        public ValuesContext(DbContextOptions<ValuesContext> options) : base(options) 
        {}

        public DbSet<Values> GetValues {get; set;}
    }
}