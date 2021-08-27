using StartCalculation.Domain.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace StartCalculation.Services.ViewModels
{
    public class CalculationInsertDto
    {
        [Required]
        public double Input1 { get; set; }

        [Required]
        public OperationType Operator { get; set; }

        [Required]
        public double Input2 { get; set; }
    }
}
