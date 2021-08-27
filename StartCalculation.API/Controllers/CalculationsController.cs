using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartCalculation.Services.Repositories;
using StartCalculation.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartCalculation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly ILogger<CalculationsController> _logger;
        private readonly ICalculationRepository _calculationRepository;

        public CalculationsController(ILogger<CalculationsController> logger, ICalculationRepository calculationRepository)
        {
            _logger = logger;
            _calculationRepository = calculationRepository;
        }

        [HttpGet]
        public ActionResult<Task<List<CalculationStatusDto>>> GetAllAsync()
        {
            _logger.LogInformation("History calculations API call.");

            return _calculationRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalculationStatusDto>> GetById(Guid? id)
        {
            _logger.LogInformation($"Get {id} calculation API call.");

            if (!id.HasValue)
            {
                _logger.LogError("Missing id parameter");
                return BadRequest();
            }

            return await _calculationRepository.FindByIdAsync(id.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalculationInsertDto calculationModel)
        {
            _logger.LogInformation($"Create {calculationModel} calculation API call.");

            if (!ModelState.IsValid)
            {
                _logger.LogError("Calculation model is not valid.");
                return UnprocessableEntity();
            }

            var id = await _calculationRepository.InsertAsync(calculationModel);
            return CreatedAtAction(nameof(GetById), new { id = id }, calculationModel);
        }
    }
}
