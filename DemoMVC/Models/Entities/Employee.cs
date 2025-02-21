
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Employee : Person
    {
    public string EmployeeID { get; set; }
    public string Company { get; set; }
    }
}
