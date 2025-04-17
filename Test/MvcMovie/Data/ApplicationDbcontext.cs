using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Daily> Daily { get; set; }
        public DbSet<Hethongphanphoi> Hethongphanphoi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPT: Tách riêng mỗi bảng cho từng entity
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
    }
}
