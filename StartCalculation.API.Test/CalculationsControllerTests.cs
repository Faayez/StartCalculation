using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StartCalculation.API.Controllers;
using StartCalculation.Domain.Domain.Entities;
using StartCalculation.Services;
using StartCalculation.Services.Repositories;
using StartCalculation.Services.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StartCalculation.Test
{
    public class CalculationsControllerTests
    {
        private readonly Mock<ICalculationRepository> _mockRepository;
        private readonly Mock<ILogger<CalculationsController>> _mockLogger;
        private readonly Mock<CalculatorDbContext> _mockDbContext;
        private readonly CalculationsController _calculationsController;
        private readonly DbContextOptions _options;

        public CalculationsControllerTests()
        {
            _options = new DbContextOptions<CalculatorDbContext>();
            _mockDbContext = new Mock<CalculatorDbContext>(_options);
            _mockRepository = new Mock<ICalculationRepository>();
            _mockLogger = new Mock<ILogger<CalculationsController>>();
            _calculationsController = new CalculationsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetByIdReturnsResultWithCalculation()
        {
            //var testId = Guid.NewGuid();
            //var calculation = GetTestCalculations().FirstOrDefault(c => c.GuId == testId);

            //_mockRepository.Setup(rep => rep.FindByIdAsync(testId).Result).Returns(_mockMapper.Object.Map<Calculation>(calculation));
            //var result = _calculationsController.GetById(testId);

            //var actionResult = Assert.IsType<Task<ActionResult<CalculationStatusDto>>>(result);
            //var model = Assert.IsType<Task<Calculation>>(actionResult.Result.Value);
            //Assert.Equal(testId, model.Result.Id);
            //Assert.Equal("3 + 5", model.Result.Expression);
            //Assert.Equal(8, model.Result.Result);
        }

        [Fact]
        public void GetByIdReturnsBadRequestResultWhenIdIsNull()
        {
            var result = _calculationsController.GetById(null).Result;
            Assert.IsType<ActionResult<CalculationStatusDto>>(result);
        }

        [Fact]
        public void GetByIdReturnsNotFoundResultWhenCalculationNotFound()
        {
            Guid testId = Guid.NewGuid();
            _mockRepository.Setup(rep => rep.FindByIdAsync(testId)).Returns(null as Task<CalculationStatusDto>);

            var result = _calculationsController.GetById(testId);

            Assert.IsType<Task<ActionResult<CalculationStatusDto>>>(result);
        }

        [Fact]
        public void CreateReturnsCreatedAtActionAndAddsCalculation()
        {
            var newCalculation = new CalculationInsertDto()
            {
                Input1 = 88,
                Input2 = 11,
                Operator = OperationType.Plus,
            };

            var result = _calculationsController.Create(newCalculation).Result;

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Null(createdAtActionResult.ControllerName);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            _mockRepository.Verify(r => r.InsertAsync(newCalculation));
        }

        [Fact]
        public void CreateReturnsResultWithInvalidCalculationModel()
        {
            _calculationsController.ModelState.AddModelError("Expression", "Required");
            var newCalculation = new CalculationInsertDto();

            var result = _calculationsController.Create(newCalculation).Result;

            Assert.IsType<UnprocessableEntityResult>(result);
        }
    }
}