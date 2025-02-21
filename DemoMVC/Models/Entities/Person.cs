
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class Person
    {
        [Key]
        public String PersonID { get; set;}
        public String HoTen { get;set;}
        public String QueQuan { get; set;}
    }
}
