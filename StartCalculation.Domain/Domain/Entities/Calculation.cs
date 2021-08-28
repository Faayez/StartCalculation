using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartCalculation.Domain.Domain.Entities
{
    public class Calculation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public CalculationStatus Status { get; set; }

        public double Input1 { get; set; }

        public OperationType Operator { get; set; }

        public double Input2 { get; set; }

        public double? Result { get; set; }

        public int ProcessEstimate { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
