using Microsoft.EntityFrameworkCore;
namespace TestWithProc.Models
{
    
    public class CustomerContext : DbContext
    {
       public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
          { }
    
    public DbSet<Customer> cust { get; set; }

        
    }
}