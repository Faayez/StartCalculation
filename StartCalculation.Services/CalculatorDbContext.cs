using Microsoft.EntityFrameworkCore;
using StartCalculation.Domain.Domain.Entities;
using StartCalculation.Services.Configurations;

namespace StartCalculation.Services
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CalculationConfiguration());
        }

        #region DbSets

        public virtual DbSet<Calculation> Calculations { get; set; }

        #endregion
    }
}

