using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models;
[Table("Persons")]

public class Employee : Person 
{
    
    
    public string? EmployeeID{ get; set; }
    public string? Age{ get;set; }
    public string? Department { get; set; }
    
    
 
   
}
