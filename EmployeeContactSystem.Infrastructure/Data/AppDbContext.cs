using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeContactSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicit table name mapping (optional)
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Company>().ToTable("Companies");

            // Unique constraints
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(c => c.Domain).IsUnique();

            // Relationships
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

