using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWithWebApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=MyDatabase;Integrated Security=True;MultipleActiveResultSets=true");
        }

        public DbSet<AspNetCoreWithWebApi.Models.EmployeeDetails> EmployeeDetails { get; set; }
    }
}
