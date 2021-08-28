using StartCalculation.Domain.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StartCalculation.Services.ViewModels
{
    public class CalculationInsertDto : IValidatableObject
    {
        [Required]
        public double Input1 { get; set; }

        [Required]
        public OperationType Operator { get; set; }

        [Required]
        public double Input2 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Operator == OperationType.Divide && Input2 == 0)
            {
                yield return new ValidationResult($"You can not devide anything with zero.", new[] { nameof(Input2) });
            }
        }
    }
}
