using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StartCalculation.Domain.Domain.Entities;
using StartCalculation.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartCalculation.Services.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly CalculatorDbContext _dbContext;
        private readonly DbSet<Calculation> _calculations;
        private readonly IMapper _mapper;

        public CalculationRepository(CalculatorDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _calculations = _dbContext?.Set<Calculation>();
            _mapper = mapper;
        }

        public Task<List<CalculationStatusDto>> GetAllAsync()
        {
            return _calculations.OrderByDescending(c => c.CreatedOn).ProjectTo<CalculationStatusDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<CalculationStatusDto> FindByIdAsync(Guid id)
        {
            return _calculations.Where(p => p.Id == id).ProjectTo<CalculationStatusDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<Guid> InsertAsync(CalculationInsertDto calculation)
        {
            var entity = _mapper.Map<Calculation>(calculation);
            entity.Result = Calculator.Calculate(entity.Input1, entity.Input2, entity.Operator);
            _dbContext.Calculations.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}