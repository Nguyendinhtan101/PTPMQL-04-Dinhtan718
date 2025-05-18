using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models;
//[Table("Persons")]

public class Employee 
{
    
     [Key]
    public int EmployeeId { get; set; }
    [Required]
    public string firstName { get; set; }
    [Required]

    public string LastName { get; set; }
    public string Address { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public  DateTime DateOfBirth  { get; set; }
    [Required]

    
    public string Position  { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [DataType(DataType.Date)]
    public  DateTime HireDate { get; set; }
                   
}
