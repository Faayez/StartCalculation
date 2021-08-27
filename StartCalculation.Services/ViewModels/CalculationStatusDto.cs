using StartCalculation.Domain.Domain.Entities;
using System;

namespace StartCalculation.Services.ViewModels
{
    public class CalculationStatusDto
    {
        public Guid Id { get; set; }

        public CalculationStatus Status { get; set; }

        public string Expression { get; set; }


        public double? Result;

        public int Progess { get; set; }
    }
}
