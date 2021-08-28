using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartCalculation.Domain.Domain.Entities;
using StartCalculation.Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace StartCalculation.Services.Repositories
{
    public class CalculationResultRepository : ICalculationResultRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CalculationResultRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task UpdateResultAsync(Guid id, CalculationInsertDto calculation, int processEstimate)
        {
            await Task.Delay(processEstimate * 1000);

            var result = Calculator.Calculate(calculation.Input1, calculation.Input2, calculation.Operator);
            var status = result.HasValue ? CalculationStatus.Completed : CalculationStatus.Failed;

            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.UniqueIdentifier,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = id
                        },
                        new SqlParameter() {
                            ParameterName = "@Result",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = result
                        },
                        new SqlParameter() {
                            ParameterName = "@Status",
                            SqlDbType =  System.Data.SqlDbType.TinyInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = status
                        }};
            try
            {
                using var serviceScope = _serviceScopeFactory.CreateScope();
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<CalculatorDbContext>();
                dbContext.Database.ExecuteSqlRaw("EXEC UpdateCalculationResult @Id, @Result, @Status", param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}