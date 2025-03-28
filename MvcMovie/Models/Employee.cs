
namespace MvcMovie.Models
{
    public class Employee : Person
    {
        public string EmployeeId { get; set; }
        public int Age { get; set; }

        public Employee()
        {
            EmployeeId = GenerateEmployeeId();
        }

        private string GenerateEmployeeId()
        {
            return "EMP-" + Guid.NewGuid().ToString("N").Substring(6).ToUpper();
        }
    }
}
