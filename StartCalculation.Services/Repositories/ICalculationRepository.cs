using StartCalculation.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartCalculation.Services.Repositories
{
    public interface ICalculationRepository
    {
        Task<List<CalculationStatusDto>> GetAllAsync();

        Task<CalculationStatusDto> FindByIdAsync(Guid id);

        Task<Guid> InsertAsync(CalculationInsertDto calculation);
    }
}
