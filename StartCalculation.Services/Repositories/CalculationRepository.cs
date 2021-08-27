using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
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

            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Id",
                            SqlDbType =  System.Data.SqlDbType.UniqueIdentifier,
                            Direction = System.Data.ParameterDirection.Output,
                        },
                        new SqlParameter() {
                            ParameterName = "@Status",
                            SqlDbType =  System.Data.SqlDbType.TinyInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Status
                        },
                        new SqlParameter() {
                            ParameterName = "@Input1",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Input1
                        },
                        new SqlParameter() {
                            ParameterName = "@Operator",
                            SqlDbType =  System.Data.SqlDbType.TinyInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Operator
                        },
                        new SqlParameter() {
                            ParameterName = "@Input2",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Input2
                        },
                        new SqlParameter() {
                            ParameterName = "@Result",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Result
                        },
                        new SqlParameter() {
                            ParameterName = "@CreatedOn",
                            SqlDbType =  System.Data.SqlDbType.DateTime2,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.CreatedOn
                        },
                        new SqlParameter() {
                            ParameterName = "@FinishedOn",
                            SqlDbType =  System.Data.SqlDbType.DateTime2,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.FinishedOn
                        }};

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC InsertCalculations @Id OUT, @Status, @Input1, @Operator, @Input2, @Result, @CreatedOn, @FinishedOn", param);
            return new Guid(param[0].Value.ToString());
        }
    }
}