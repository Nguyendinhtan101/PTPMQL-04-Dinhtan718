using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcMovie.Models.Entities;
using Entities = MvcMovie.Models.Entities;
using Models = MvcMovie.Models;




namespace MvcMovie.Data
{
     public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Daily> Daily { get; set; }
        //public DbSet<Employee2> Employee2 { get; set; }
        public DbSet<Hethongphanphoi> Hethongphanphoi { get; set; }
        public DbSet<MemberUnits> MemberUnits { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // TPT: Tách riêng mỗi bảng cho từng entity
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Employee>().ToTable("Employees");
             base.OnModelCreating(modelBuilder);
        //  builder.Entity<ApplicationUser>().ToTable("Users");
        //  builder.Entity<IdentityRole>().ToTable("Roles");
        //  builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        //  builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        //  builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        //  builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        //  builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        //  builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        }
        
    }
}
