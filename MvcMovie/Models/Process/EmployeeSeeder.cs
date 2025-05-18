using System;
using System.Collections.Generic;
using Bogus;
using MvcMovie.Models; // Hoặc Models.Entities tùy namespace của bạn
using MvcMovie.Data;
namespace MvcMovie.Models.Process
{
    public class EmployeeSeeder
    {
        private readonly ApplicationDbContext _context;
        public EmployeeSeeder(ApplicationDbContext context)
        {
            _context = context;

         }
        public void SeedEmployees(int n)

        {
            var employees = GenerateEmployees (n);


            _context.Employee.AddRange(employees);
            _context.SaveChanges();
         }
        public  List<Employee> GenerateEmployees(int n)
        {
            var faker = new Faker<Employee>()
                .RuleFor(e => e.firstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-20)))
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.firstName, e.LastName))
                .RuleFor(e => e.HireDate, f => f.Date.Past(10));

            return faker.Generate(n);
        }
    }
}
