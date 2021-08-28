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
        private readonly ICalculationResultRepository _calculationResult;
        private readonly IMapper _mapper;

        public CalculationRepository(CalculatorDbContext dbContext, ICalculationResultRepository calculationResult, IMapper mapper)
        {
            _dbContext = dbContext;
            _calculations = _dbContext?.Set<Calculation>();
            _calculationResult = calculationResult;
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
            Random rnd = new Random();
            var processEstimate = rnd.Next(20, 60);

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
                            Value = CalculationStatus.Running
                        },
                        new SqlParameter() {
                            ParameterName = "@Input1",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = calculation.Input1
                        },
                        new SqlParameter() {
                            ParameterName = "@Operator",
                            SqlDbType =  System.Data.SqlDbType.TinyInt,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = calculation.Operator
                        },
                        new SqlParameter() {
                            ParameterName = "@Input2",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = calculation.Input2
                        },
                        new SqlParameter() {
                            ParameterName = "@CreatedOn",
                            SqlDbType =  System.Data.SqlDbType.DateTime2,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = DateTime.Now
                        },
                        new SqlParameter() {
                            ParameterName = "@ProcessEstimate",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = processEstimate
                        }};

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC InsertCalculation @Id OUT, @Status, @Input1, @Operator, @Input2, @CreatedOn, @ProcessEstimate", param);
            var id = new Guid(param[0].Value.ToString());

            Task.Run(() => _calculationResult.UpdateResultAsync(id, calculation, processEstimate));
            return id;
        }
    }
}