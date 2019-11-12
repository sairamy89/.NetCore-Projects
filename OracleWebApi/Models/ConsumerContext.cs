using Microsoft.EntityFrameworkCore;

namespace OracleWebApi.Models
{
    public class ConsumerContext : DbContext
    {
        public ConsumerContext(DbContextOptions<ConsumerContext> options) : base (options){}

        public DbSet<Consumer> ConsumerItems {get; set;}
    }
}