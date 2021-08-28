using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartCalculation.Services.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StartCalculation.Web.Controllers
{
    public class CalculationController : Controller
    {
        private const string ApiUrl = "https://localhost:44350/Calculations";
        private readonly ILogger<CalculationController> _logger;

        public CalculationController(ILogger<CalculationController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{ApiUrl}?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<CalculationStatusDto>(result);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CalculationInsertDto calculation)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                var response = await client.PostAsync(ApiUrl, new StringContent(JsonSerializer.Serialize(calculation), Encoding.UTF8, "application/json"));
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return View(calculation);
            }
        }
    }
}
