using StartCalculation.Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace StartCalculation.Services.Repositories
{
    public interface ICalculationResultRepository
    {
        Task UpdateResultAsync(Guid id, CalculationInsertDto calculation, int processEstimate);
    }
}