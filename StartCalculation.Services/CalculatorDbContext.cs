using Microsoft.EntityFrameworkCore;
using StartCalculation.Domain.Domain.Entities;

namespace StartCalculation.Services
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options) : base(options)
        { }

        #region DbSets

        public virtual DbSet<Calculation> Calculations { get; set; }

        #endregion
    }
}

