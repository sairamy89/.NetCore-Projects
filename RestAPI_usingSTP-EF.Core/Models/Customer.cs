using System.ComponentModel.DataAnnotations;
namespace TestWithProc.Models
{

    public class Customer
    {
    [Key]
    public int Customer_id { get; set; }

    public int Age { get; set; }

    public string Customer_name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }
     public string DateOfBirth { get; set; }
   // public string message {get; set;}
    }
}
