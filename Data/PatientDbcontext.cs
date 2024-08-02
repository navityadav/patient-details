using Microsoft.EntityFrameworkCore;
using patient.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace patient.Data
{
  
    public class PatientDbContext : DbContext
    {
      public DbSet<Patient> Patients { get; set; }

      public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
      {
      Database.EnsureCreated();
      }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlite("Data Source=:memory:");
      }
    }
  }
}
