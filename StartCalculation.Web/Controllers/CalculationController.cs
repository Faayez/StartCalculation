using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartCalculation.Services.ViewModels;

namespace StartCalculation.Web.Controllers
{
    public class CalculationController : Controller
    {
        private readonly ILogger<CalculationController> _logger;

        public CalculationController(ILogger<CalculationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CalculationInsertDto calculation)
        {
            return View();
        }
    }
}
