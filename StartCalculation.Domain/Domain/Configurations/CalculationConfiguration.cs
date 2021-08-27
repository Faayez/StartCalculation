using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartCalculation.Domain.Domain.Entities;

namespace StartCalculation.Services.Configurations
{
    public class CalculationConfiguration : IEntityTypeConfiguration<Calculation>
    {
        public void Configure(EntityTypeBuilder<Calculation> builder)
        {
            builder.Property(calculation => calculation.Input1).IsRequired().HasMaxLength(100);

            builder.Property(calculation => calculation.Input2).IsRequired().HasMaxLength(100);
        }
    }
}
