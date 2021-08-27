﻿using System;

namespace StartCalculation.Domain.Domain.Entities
{
    public class Calculation
    {
        public Calculation()
        {
            Random rnd = new Random();
            CreatedOn = DateTime.Now;
            FinishedOn = CreatedOn.AddSeconds(rnd.Next(20, 60));
        }

        public Guid Id { get; set; }

        public CalculationStatus Status { get; set; }

        public double Input1 { get; set; }

        public OperationType Operator { get; set; }

        public double Input2 { get; set; }

        public double? Result;

        public int Progess => (DateTime.Now - CreatedOn).Seconds / (FinishedOn - CreatedOn).Seconds * 100;

        public DateTime CreatedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }
    }
}
