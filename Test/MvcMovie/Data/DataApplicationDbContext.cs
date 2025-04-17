using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie
{
    public class DataApplicationDbContext : DbContext
    {
        public DataApplicationDbContext (DbContextOptions<DataApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Daily> Daily { get; set; } = default!;
        public DbSet<MvcMovie.Models.Hethongphanphoi> Hethongphanphoi { get; set; } = default!;
    }
}
