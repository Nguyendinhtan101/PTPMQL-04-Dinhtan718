
using System;

namespace MvcMovie.Models
{
    public class Person
    {
        public string PersonId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Person()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            PersonId = GeneratePersonId();
        }

        private string GeneratePersonId()
        {
            return "P-" + Guid.NewGuid().ToString("N").Substring(6).ToUpper();
        }
    }
}
